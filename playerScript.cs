using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour {
	
    //private Rigidbody2D rigi;
    private SpriteRenderer sprite;

    public float playerSpeed ; //speed player moves

	
    public bool screenMove; //are we in transition from one screen to another?
    public Transform movePoint; //where are we moving to?
    public float transTimer; // speed of transition movement
	//public Text health;
	//public Text money;
	public int direction; // which direction is the player going? 1 is up, 2 is down, 3 is right, 4 is left
	
	public bool topCollision; // top collision is detected
	public bool bottomCollision; // bottom collision is detected
	public bool rightCollision; // right collision is detected
	public bool leftCollision; // left collision is detected
	public bool canMove;
	public bool bumpBool;

    public GameObject Coll;
	
	public int maxHealth;
	public int health;
	public Slider healthBar;
	public Text healthText;
	public static int money;
	public Text moneyText;

    public GameObject bossDoor;


    private void Start()
    {
        //rigi = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
		
		healthBar.minValue = 0;
		healthBar.maxValue = maxHealth;
		health = maxHealth;
		healthBar.value = maxHealth;
		money = 0;

		UpdateHealthText();
		AddMoney(0);

		topCollision = false;
		bottomCollision = false;
		rightCollision = false;
		leftCollision = false;
		canMove = true;
		bumpBool = false;
    }

    void Update()
    {
		if (canMove)
        Movement(); // Player Movement

		if (bumpBool)
			Bump();
	
        if (screenMove) // check to see if we are in a transition state
            ScreenTransition(); 



    }

    void Movement()
    {
		//if (topCollision && rightCollision && leftCollision)

		if(topCollision  == false)
		{
			if ((Input.GetAxis("Vertical"))>0)//Press up arrow key to move forward on the Y AXIS
			{
				transform.Translate(0, playerSpeed * Time.deltaTime, 0);
			
			}
		}
		
		if(bottomCollision  == false)
		{
			if ((Input.GetAxis("Vertical")) < 0)//Press up arrow key to move forward on the Y AXIS
			{
				transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
			
			}
		}
		
		if(leftCollision  == false)
		{
			if ((Input.GetAxis("Horizontal")) < 0)//Press up arrow key to move forward on the Y AXIS
			{
				if (!sprite.flipX)
					sprite.flipX = true;

				transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
			
			}
		}
		
		if(rightCollision  == false)
		{
			if ((Input.GetAxis("Horizontal")) > 0)//Press up arrow key to move forward on the Y AXIS
			{
				if (sprite.flipX)
					sprite.flipX = false;

				transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
			
			}
		}


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Slider")
        {
            //screenMove = true;
            //movePoint = coll.GetComponent<screenSlide>().PlayerMark;
            transTimer = 1f;
        }
		if (coll.gameObject.tag == "Enemy") {
			bumpBool = true;
			canMove = false;
			transTimer = 0.5f;
			TakeDamage(1);
			this.GetComponent<Collider2D>().enabled = false;
			DestroyObject (coll.gameObject);
		}
        if (coll.gameObject.tag == "Powerup")
        {
            GetComponent<PowerUp>().setPowerup(coll.gameObject);
            DestroyObject(coll.gameObject);
        }

        if (coll.gameObject.name == "Key") {
            bossDoor.SetActive(false);
            Destroy(coll.gameObject);
        }
		if (coll.gameObject.tag == "EnemyShot") {
			bumpBool = true;
			canMove = false;
			transTimer = 0.5f;
			TakeDamage(4);
			this.GetComponent<Collider2D>().enabled = false;
			DestroyObject (coll.gameObject);
		}
        if (coll.gameObject.tag == "dangerZone") {
            TakeDamage(2);
        }
		
		
    }

	void Bump(){

		transTimer -= Time.deltaTime;
		canMove = true;
		if (transTimer >= 1) {
			/*if (Input.GetAxis ("Horizontal") > 0) {
				transform.position = Vector3.Lerp (transform.position, 
					new Vector3 (transform.position.x - 2, transform.position.y, 3), 0.5f * Time.deltaTime); 
			} else {
				transform.position = Vector3.Lerp (transform.position, 
					new Vector3 (transform.position.x + 2, transform.position.y, 3), 0.5f * Time.deltaTime);
			}*/

			/*if (Input.GetAxis ("Vertical") > 0) {
				transform.position = Vector3.Lerp (transform.position, 
					new Vector3 (transform.position.x, transform.position.y - 2, 3), 1f * Time.deltaTime);
			} else {
				transform.position = Vector3.Lerp (transform.position, 
					new Vector3 (transform.position.x, transform.position.y + 2, 3), 1f * Time.deltaTime);
			}*/
		}else if (transTimer >= 0) {
			canMove = true;
		}else {
			this.GetComponent<Collider2D>().enabled = true;
			bumpBool = false;
		}
	}

    // screen transition function
    void ScreenTransition()
    {
        //print("transitioning");
        this.GetComponent<Collider2D>().enabled = false;
        Coll.SetActive(false);
        //rigi.velocity = Vector2.zero;
        transform.position = Vector3.Lerp(transform.position, movePoint.position, 3f * Time.deltaTime);
        transTimer -= Time.deltaTime;
        if (transTimer < 0)
        {
            this.GetComponent<Collider2D>().enabled = true;
            Coll.SetActive(true);
            screenMove = false;
            
        }


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
	
	
	void TakeDamage(int amt)	/* Decrements player's health by set amount. */
	{
		health = (health - amt);
		healthBar.value = health;
		if (health <= 0){
			Death();
		}
		UpdateHealthText();
	}
	
	void Restore(int amt)	/* Restores player's health by set amount. */
	{
		health = (health + amt);
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		healthBar.value = health;
		UpdateHealthText();
	}
	
	void FullRestore()	/* Completely restores player health. */
	{
		health = maxHealth;
		healthBar.value = maxHealth;
		UpdateHealthText();
	}
	
	void Death()	/* Player's health drops to 0. */
	{
		health = 0;
		healthBar.value = 0;
		/* Add death code here.. */
	}
	
	public void UpgradeHealth(int amt)		/* Upgrades the player's maximum health, used for shop or powerup. */
	{
		maxHealth = (maxHealth + amt);
		healthBar.maxValue = maxHealth;
		FullRestore();
	}
	
	void UpdateHealthText() /* Updates the health text when needed */
	{
		healthText.text = (health.ToString() + " / " + maxHealth.ToString());
	}
	
	void AddMoney(int amt)	/* Adds money, updates the money text when needed */
	{
		money = (money + amt);
		moneyText.text = money.ToString();
	}

}
