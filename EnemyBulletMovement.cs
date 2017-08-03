using UnityEngine;
using System.Collections;

public class EnemyBulletMovement : MonoBehaviour
{

	public Vector3 velocity;
	public float step = 2f;
	private Transform player;
	private float lifeTimer = 3f;

	void Start()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		player = go.transform;
		//Debug.Log("Player:" + "" + player);
		//Debug.Log(this.transform.position);
		//Debug.Log("player's position" + player.position);
	}


	void Update()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, step * Time.deltaTime);
		Debug.Log("this.transform:" + "" + this.transform.position);
		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0)
			Destroy (gameObject);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
