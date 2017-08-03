using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenSlide : MonoBehaviour {


    GameObject Cam;
    public GameObject MovingCamTo;
    public float camSize;
    public bool isExit;
    public Transform PlayerMark;
    GameObject Player;

    // Use this for initialization
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerMark = this.gameObject.transform.GetChild(0);
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Moving Screen");
            Cam.GetComponent<CameraPosition>().MoveCamTo = MovingCamTo;
            Cam.GetComponent<CameraPosition>().camSize = camSize;

            Player.GetComponent<playerScript>().movePoint = PlayerMark;
            Player.GetComponent<playerScript>().screenMove = true;
        }

    }



}
