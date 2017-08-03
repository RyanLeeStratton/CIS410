using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    private Rigidbody2D rigi;
    public float bulletSpeed;
    public float rotateSpeed;
    public float dirX;
    public float dirY;
    public float lifeTimer;

	// Use this for initialization
	void Start () {

	}

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * dirX * Time.deltaTime);
        transform.Translate(Vector3.up * dirY * Time.deltaTime);
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
            Destroy(gameObject);
    }


}
