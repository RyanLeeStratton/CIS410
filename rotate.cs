using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public float rotateSpeed;
    public bool is2D;

    // Update is called once per frame
    void Update() {
        if (!is2D)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
        else {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}
