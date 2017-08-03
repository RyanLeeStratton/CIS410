using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

    public int health;
    public float spawn_rate;
    public bool isStartRoom;
    public GameObject goOnDeath;

    public GameObject[] enemyArray = null;

    public GameObject enemy;
    public GameObject enemyParent;
    public GameObject player;

    private GameObject newSpawn;
    private SpriteRenderer spriteR;

    private float flashRate;
    private float cooldown;
    private bool isFlashing = false;

    private int rand;

    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    void Awake () {
        health = health * 2;
        cooldown = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyParent = GameObject.Find("Enemy");

        enemyArray = Resources.LoadAll<GameObject>("Enemies");

        rand = Random.Range(0,enemyArray.Length-1);
        enemy = enemyArray[rand];

        switch (rand) {
            case 0:
                spawn_rate = 2f;
                break;
            case 1:
                spawn_rate = 1.5f;
                break;
            case 2:
                spawn_rate = 1.5f;
                break;
            case 3:
                spawn_rate = 2f;
                break;
            case 4:
                spawn_rate = 3f;
                break;
            case 5:
                spawn_rate = 4f;
                break;
            case 6:
                spawn_rate = 1.5f;
                break;
            case 7:
                spawn_rate = 2.5f;
                break;
            default:
                break;
        }
	}


    void Update() {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0) {
            enemySpawn();
            cooldown = spawn_rate;
        }

        if (isFlashing)
            colorFlash();
	}

    void enemySpawn() {

        newSpawn = Instantiate(enemy,this.transform.position,this.transform.rotation,enemyParent.transform);
        newSpawn.GetComponent<EnemyScript>().target = player.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);

            isFlashing = true;
            //colorFlash();


            health -= other.GetComponent<Bullet_Variables>().getDamage();
            if (health <= 1)
            {
                if (isStartRoom)
                    goOnDeath.SetActive(false);
                gameObject.SetActive(false);
				GameObject.Find("Main Camera").GetComponent<CameraPosition>().setShake(.6f);
            }
            else
                GameObject.Find("Main Camera").GetComponent<CameraPosition>().setShake(.2f);
        }

    }

    void colorFlash() {
        flashRate = health * .2f;
        spriteR.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashRate));

    }

}
