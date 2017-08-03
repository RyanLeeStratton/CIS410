using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public Transform target;
	Transform mytransform;
    public GameObject explosion;
	public float EnemySpeed;
    public int health;
    private float knockback;
    private int kb_timer;
	
	public bool topCollision; // top collision is detected
	public bool bottomCollision; // bottom collision is detected
	public bool rightCollision; // right collision is detected
	public bool leftCollision; // left collision is detected

    public bool speedingUp;
	
	private float currentTopPos;
	private float currentBottomPos;
	private float currentLeftPos;
	private float currentRightPos;
    private float timer;
    private SpriteRenderer thisSprite;

	//public float bulletSpeed;
	public float fireRate;
	public float nextFire = 5;
	public GameObject bullet;
	public Transform BulletSpawnPoint;
	public bool CanShoot;
	public AudioClip DeathSound;
	AudioSource audio;

	 void Start (){
		
		mytransform = transform;
        health = health * 2;
		topCollision = false;
		bottomCollision = false;
		rightCollision = false;
		leftCollision = false;
        thisSprite = gameObject.GetComponent<SpriteRenderer>();

		target = GameObject.FindGameObjectWithTag ("Player").transform;
		BulletSpawnPoint = mytransform;
		audio = gameObject.GetComponent<AudioSource>();
		
		
	}

	void Update(){
		
		EnemyMove ();
        kb_timer--;

		if (target && CanShoot)
		{
			if (Time.time >= nextFire)
			{   
				nextFire = Time.time + fireRate;
				CreateBullet();
			}
		}

			
	}


	void CreateBullet()
	{
		// Creates a new instance of the bullet
		Instantiate(bullet, BulletSpawnPoint.position + (target.position - BulletSpawnPoint.position).normalized, BulletSpawnPoint.rotation);       
	}

	void EnemyMove(){

        if (kb_timer < 1)
        {
            if (target.position.x > transform.position.x)
            {
                //face right
                transform.localScale = new Vector3(4, 4, 1);
				
				currentTopPos = mytransform.position.y;
				
                Vector3 directionTowardsAI = (target.position - mytransform.position).normalized;
				
				if(topCollision == true)
				{
					if(currentTopPos <= directionTowardsAI.y)
					{
						directionTowardsAI.y = 0;
					}
				}
				
				if(bottomCollision == true)
				{
					if(currentBottomPos >= directionTowardsAI.y)
					{
						directionTowardsAI.y = 0;
					}
				}
				
				if((leftCollision == true) || (rightCollision == true))
				{
					directionTowardsAI.x = 0;
				}
				
                mytransform.Translate(directionTowardsAI * EnemySpeed * Time.deltaTime);

            }
            if (target.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-4, 4, 1);
                Vector3 directionTowardsAI = (target.position - mytransform.position).normalized;
				
				if((topCollision == true) || (bottomCollision == true))
				{
					directionTowardsAI.y = 0;
				}
				
				if((leftCollision == true) || (rightCollision == true))
				{
					directionTowardsAI.x = 0;
				}
				
                mytransform.Translate(directionTowardsAI * EnemySpeed * Time.deltaTime);
            }
            if (speedingUp)
            {
                EnemySpeed += Time.deltaTime*3;
            }
        }
	}
		


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            knockback = other.GetComponent<Bullet_Variables>().getKnockback(); 
            Destroy(other.gameObject);
            health -= other.GetComponent<Bullet_Variables>().getDamage();
            if (health <= 1)
            {
				 GetComponent<SpriteRenderer>().enabled = false;
				 GetComponent<Collider2D>().enabled = false;
				 audio.PlayOneShot(DeathSound, 0.7F);
				 var boom = Instantiate (explosion, transform.position, transform.rotation);
                 Destroy(gameObject, .7F);
            }
            kb_timer = 6;
            transform.Translate(other.transform.right * knockback*Time.deltaTime);
            StartCoroutine(colorFlash());
        }
    }

    IEnumerator colorFlash() {
        thisSprite.color = Color.red;
        yield return new WaitForSeconds(.5f);
        thisSprite.color = Color.white;
    }

	//disables being able to move up
	public void setTopCollision()
	{
		topCollision = true;
	}
	
	//enable being able to move up
	public void setTopCollisionOff()
	{
		topCollision = false;
	}
	
	//disables being able to move down
	public void setBottomCollision()
	{
		bottomCollision = true;
	}
	
	//enable being able to move down
	public void setBottomCollisionOff()
	{
		bottomCollision = false;
	}
	
	//disables being able to move left
	public void setLeftCollision()
	{
		leftCollision = true;
	}
	
	//enable being able to move left
	public void setLeftCollisionOff()
	{
		leftCollision = false;
	}
	
	//disables being able to move right
	public void setRightCollision()
	{
		rightCollision = true;
	}
	
	//enable being able to move right
	public void setRightCollisionOff()
	{
		rightCollision = false;
	}

    public void setTarget(GameObject toSet)
    {
        target = toSet.transform;
    }
}
		
