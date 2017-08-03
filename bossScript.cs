using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossScript : MonoBehaviour {

    public bool fire;
    public int shotDensity;
    public GameObject projectile;
    public GameObject projectileParent;
    public float ySpeed;
    public float xSpeed;
    public float shotFreq;
    public float setStageTimer;
    public float flashSpeed;
    public float sweepSpeed;

    public bool shootingPhaseBool;
    public bool dangerZoneLeftBool;
    public bool dangerZoneRightBool;
    public bool dangerSweepUpBool;
    public bool dangerSweepLeftBool;

    public GameObject dangerZoneLeft;
    public GameObject dangerZoneRight;
    public GameObject dangerSweepUp1;
    public GameObject dangerSweepUp2;
    public GameObject dangerSweepLeft1;
    public GameObject dangerSweepLeft2;

    private SpriteRenderer dzLeftSR;
    private SpriteRenderer dzRightSR;
    private SpriteRenderer dsUpSR1;
    private SpriteRenderer dsUpSR2;
    private SpriteRenderer dsLeftSR1;
    private SpriteRenderer dsLeftSR2;

    public int health;
    public Slider healthBar;
    private float shotTimer;
    private float stageTimer;
    private GameObject tempProjectile;
    private rotate rotator;
    public bool boxCollBool;

	// Use this for initialization
	void Start () {

        healthBar.minValue = 0;
		healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.gameObject.SetActive(true);
        shotTimer = shotFreq;
        rotator = GetComponentInChildren<rotate>();
        dzLeftSR = dangerZoneLeft.GetComponent<SpriteRenderer>();
        dzRightSR = dangerZoneRight.GetComponent<SpriteRenderer>();
        dsUpSR1 = dangerSweepUp1.GetComponent<SpriteRenderer>();
        dsUpSR2 = dangerSweepUp2.GetComponent<SpriteRenderer>();
        dsLeftSR1 = dangerSweepLeft1.GetComponent<SpriteRenderer>();
        dsLeftSR2 = dangerSweepLeft2.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update() {
        if (shootingPhaseBool)
        {
            shootingPhase();
        }
        else if (dangerZoneLeftBool) {
            dangerFloor1();
        }
        else if (dangerZoneRightBool)
        {
            dangerFloor2();
        }
        else if (dangerSweepUpBool)
        {
            sweepUp();
        }
        else if (dangerSweepLeftBool)
        {
            sweepLeft();
        }
        else
        {
            int rand = Random.Range(0, 7);
            switch (rand)
            {
                case 0:
                case 1:
                case 2:
                    stageTimer *= 1f;
                    shootingPhaseBool = true;
                    break;
                case 3:
                    dangerZoneLeft.SetActive(true);
                    dangerZoneLeftBool = true;
                    dangerZoneLeft.GetComponent<BoxCollider2D>().enabled = false;
                    boxCollBool = false;
                    break;
                case 4:
                    dangerZoneRight.SetActive(true);
                    dangerZoneRightBool = true;
                    dangerZoneRight.GetComponent<BoxCollider2D>().enabled = false;
                    boxCollBool = false;
                    break;
                case 5:
                    dangerSweepUp1.transform.parent.gameObject.SetActive(true);
                    dangerSweepUpBool = true;
                    dangerSweepUp1.GetComponent<BoxCollider2D>().enabled = false;
                    dangerSweepUp2.GetComponent<BoxCollider2D>().enabled = false;
                    boxCollBool = false;
                    break;
                case 6:
                    dangerSweepLeft1.transform.parent.gameObject.SetActive(true);
                    dangerSweepLeftBool = true;
                    dangerSweepLeft1.GetComponent<BoxCollider2D>().enabled = false;
                    dangerSweepLeft2.GetComponent<BoxCollider2D>().enabled = false;
                    boxCollBool = false;
                    break;
            }

        }
    }

    void shootingPhase() {
        shotTimer -= Time.deltaTime;
        stageTimer -= Time.deltaTime;
        //print("Stage Timer: " + stageTimer);
        if (stageTimer <= 0)
        {
            shootingPhaseBool = false;
            stageTimer = setStageTimer;
            return;
        }

        if (shotTimer <= 0)
            fire = true;
        if (fire)
        {
            int rand = Random.Range(0, shotDensity);
            //print("Random num: " + rand);
            for (int i = 0; i <= shotDensity; i++)
            {
                tempProjectile = Instantiate(projectile, this.transform.position, projectile.transform.rotation, projectileParent.transform);
                tempProjectile.GetComponent<projectileScript>().dirY = ySpeed;
                if (i == rand)
                    i += 5;
                if (i % 2 == 0)
                    tempProjectile.GetComponent<projectileScript>().dirX = i * xSpeed;
                else
                    tempProjectile.GetComponent<projectileScript>().dirX = -i * xSpeed;
            }
            fire = false;
            shotTimer = shotFreq;
        }


    }

    void dangerFloor1() {
        stageTimer -= Time.deltaTime;

        if (stageTimer <= 2)
            boxCollBool = true;
        if (stageTimer <= 0)
        {
            boxCollBool = false;
            dangerZoneLeftBool = false;
            stageTimer = setStageTimer;

            dzLeftSR.color = new Color (1f,1f,1f,0f);


            dangerZoneLeft.SetActive(false);
            return;
        }
        
        //dzLeftSR.color = new Color(255,255,255,Mathf.Lerp(dzLeftSR.color.a, 150f, 1f));
        StartCoroutine(FadeCR(dzLeftSR));

        if (boxCollBool) {
        dangerZoneLeft.GetComponent<BoxCollider2D>().enabled = true;
        dzLeftSR.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
        
        }
    }

    private IEnumerator FadeCR(SpriteRenderer incSprite)
    {
        float duration = 2f; 
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            incSprite.color = new Color(incSprite.color.r, incSprite.color.g, incSprite.color.b, alpha);
            currentTime += Time.deltaTime;
                
            yield return null;
        }
        yield break;
    }
    

    void dangerFloor2() {
        stageTimer -= Time.deltaTime;
        if (stageTimer <= 2)
            boxCollBool = true;
        if (stageTimer <= 0)
        {

            dangerZoneRightBool = false;
            stageTimer = setStageTimer;
            dzRightSR.color = new Color(1f, 1f, 1f, 0);
            boxCollBool = false;
            dangerZoneRight.SetActive(false);

            return;
        }
        
        StartCoroutine(FadeCR(dzRightSR));

        if (boxCollBool)
        {
            dzRightSR.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
            dangerZoneRight.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void sweepUp() {
        stageTimer -= Time.deltaTime;
        if (stageTimer <= 2)
            boxCollBool = true;
        if (stageTimer <= 0)
        {

            dangerSweepUpBool = false;
            stageTimer = setStageTimer;
            dsUpSR1.color = new Color(1f, 1f, 1f, 0);
            dsUpSR2.color = new Color(1f, 1f, 1f, 0);
            dangerSweepUp1.transform.localPosition = new Vector3(0, -5, 0);
            dangerSweepUp2.transform.localPosition = new Vector3(0, 0, 0);
            boxCollBool = false;
            dangerSweepUp1.transform.parent.gameObject.SetActive(false);
            return;
        }
        

        StartCoroutine(FadeCR(dsUpSR1));
        StartCoroutine(FadeCR(dsUpSR2));

        if (boxCollBool)
        {
            dsUpSR1.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
            dsUpSR2.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
            dangerSweepUp1.transform.Translate(sweepSpeed * Time.deltaTime,0, 0);
            dangerSweepUp2.transform.Translate(sweepSpeed * Time.deltaTime,0, 0);
            dangerSweepUp1.GetComponent<BoxCollider2D>().enabled = true;
            dangerSweepUp2.GetComponent<BoxCollider2D>().enabled = true;
        }


    }

    void sweepLeft() {

        stageTimer -= Time.deltaTime;
        if (stageTimer <= 2)
            boxCollBool = true;
        if (stageTimer <= 0)
        {
            
            dangerSweepLeftBool = false;
            stageTimer = setStageTimer;
            dsLeftSR1.color = new Color(1f, 1f, 1f, 0);
            dsLeftSR2.color = new Color(1f, 1f, 1f, 0);
            dangerSweepLeft1.transform.localPosition = new Vector3(0, 2.5f, 0);
            dangerSweepLeft2.transform.localPosition = new Vector3(0, -2.5f, 0);
            boxCollBool = false;
            dangerSweepLeft1.transform.parent.gameObject.SetActive(false);
            return;
        }
        
        StartCoroutine(FadeCR(dsLeftSR1));
        StartCoroutine(FadeCR(dsLeftSR2));


        if (boxCollBool)
        {
            dsLeftSR1.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
            dsLeftSR2.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, flashSpeed));
            dangerSweepLeft1.transform.Translate(sweepSpeed * Time.deltaTime,0, 0);
            dangerSweepLeft2.transform.Translate(sweepSpeed * Time.deltaTime,0, 0);
            dangerSweepLeft1.GetComponent<BoxCollider2D>().enabled = true;
            dangerSweepLeft1.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health -= other.GetComponent<Bullet_Variables>().getDamage();
            healthBar.value = health;
            rotator.rotateSpeed +=  ((health - 30)*2);
            if (health < 1)
            {
                healthBar.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

    }
}
