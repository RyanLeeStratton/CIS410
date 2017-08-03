using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Punch_Behavior : MonoBehaviour {
    public float force;
    public float lifetime;

    private Rigidbody2D rb;
    private AudioSource aClip;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
    }
	

	void Update () {
        if (lifetime > 0)
        {
            lifetime--;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
