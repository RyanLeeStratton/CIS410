using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public GameObject cameraObj;
    public GameObject MoveCamTo;
    public float smoothSpeed;
    public float camSize;
    public float shakeIntensity;
    private float shake;

    private Vector3 random;
    private Vector3 setPoint;
    private playerScript player;

    // Use this for initialization
    void Start()
    {
        //cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        //MoveCamTo = GameObject.Find ("RoomSpawnpoint00");
       transform.position = MoveCamTo.transform.position;
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
		if (MoveCamTo == null) {
			//MoveCamTo = GameObject.Find ("RoomSpawnpoint00");
			//print ("MoveCamTo is still nulll....");
		} else {
			cameraObj.transform.position = Vector3.Lerp (cameraObj.transform.position, MoveCamTo.transform.position, smoothSpeed * Time.deltaTime);
            setPoint = cameraObj.transform.position;
			cameraObj.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cameraObj.GetComponent<Camera> ().orthographicSize, camSize, smoothSpeed * Time.deltaTime);

        }
	}

    void FixedUpdate()
    {
        if (shake > 0.1f)
        {
            //...I'll need to rewrite this later...
            random = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), transform.position.z);
            transform.position = setPoint + random;
            shake = shake * shakeIntensity;
        }
    }

    public void setShake(float newShake)
    {
        shake = newShake;

    }

}
