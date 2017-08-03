using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public GameObject player;

    public float speed;
    private Rigidbody2D rb;
    private bool cansee;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 target = player.transform.position - transform.position;
        target.Normalize();
        rb.velocity = target * speed;
    }

    
    private void OnTriggerEnter(Collision collision)
    {
        BounceAtPlayer();
    }

    private void BounceAtPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.transform.gameObject.tag == "PlayerCollider")
        {
            Vector2 target = player.transform.position - transform.position;
            target.Normalize();
            rb.velocity = target * speed;
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (hit.transform.gameObject.tag == "PlayerCollider")
        {
            Vector2 target = player.transform.position - transform.position;
            target.Normalize();
            rb.velocity = target * speed;
        }
    }
}
