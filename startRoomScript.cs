using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startRoomScript : MonoBehaviour {

    public float timer;
    public GameObject portal;
	public Text timerText;
	public int timerInt;
    private Camera camera;
    private bool isOnce;

    // Use this for initialization
    void Start () {
        camera = Camera.main;
        isOnce = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (timer >= 0)
        {
			
            timer -= Time.deltaTime;
			timerInt = (int)timer;
			if (timerInt > 0) {
				timerText.text = timerInt.ToString ();
			}
			else {
				timerText.fontSize = 60;
				timerText.text = "GO";
			}
        }
        else {
			if (timerText.IsActive()){
				timerText.transform.gameObject.SetActive(false);
			}
            if (!portal.activeSelf&&isOnce) {
                isOnce = false;
                portal.SetActive(true);
                camera.GetComponent<CameraPosition>().setShake(.4f);
            }
        }
	}
}
