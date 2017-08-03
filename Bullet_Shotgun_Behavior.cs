using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun_Behavior : MonoBehaviour
{
    public float force;
    public float lifetime;
    public float variance;

    private Rigidbody2D rb;

    void Start()
    {
        force += Random.Range(0, variance);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
    }


    void Update()
    {
        if (lifetime > 0)
        {
            lifetime--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
