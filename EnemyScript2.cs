using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript2 : MonoBehaviour {

	public Transform target;
	Transform mytransform;
	public float EnemySpeed;
    public int health;
    private float knockback;
    private int kb_timer;
	private int Lock;
	
	public Rigidbody2D rb;

	 void Start (){
		
		mytransform = transform;
        health = health * 2;
		Lock = 0;
	}

	void Update(){
		
		EnemyMove ();
        kb_timer--;
	}

	void EnemyMove(){

        if (kb_timer < 1)
        {
            if (target.position.x > transform.position.x)
            {
                //face right
                transform.localScale = new Vector3(4, 4, 1);
				Vector3 oldPosition = (mytransform.position);
				
                Vector3 directionTowardsAI = (target.position - mytransform.position).normalized;
				
				
				if(Lock == 0)
				{
					mytransform.Translate(directionTowardsAI * EnemySpeed * Time.deltaTime);
				}
				
				if(Lock == 1)
				{
					//mytransform.Translate(oldPosition* EnemySpeed * Time.deltaTime);
				}
            }
            else if (target.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-4, 4, 1);
				Vector3 oldPosition = (mytransform.position);
                Vector3 directionTowardsAI = (target.position - mytransform.position).normalized;
				
				if(Lock == 0)
				{
					mytransform.Translate(directionTowardsAI * EnemySpeed * Time.deltaTime);
				}
				
				if(Lock == 1)
				{
					//mytransform.Translate(oldPosition* EnemySpeed * Time.deltaTime);
				}
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
                Destroy(gameObject);
            }
            kb_timer = 6;
            transform.Translate(other.transform.right * knockback*Time.deltaTime);
        }
		
		if((other.gameObject.tag == "Wall") || (other.gameObject.tag == "Enemy") || (other.gameObject.tag == "Player"))
		{
			Lock = 1;
		
		}

    }
	
	void OnTriggerExit2D(Collider2D other)
	{
			Lock = 0;	
	}
/*	
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
	}*/
}
		
