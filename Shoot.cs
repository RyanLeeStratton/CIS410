using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet_punch;
    public float fireRate;
	public AudioClip sound;
    public float soundVolume;
	
    private Camera camera;
    private Vector3 mousePosition;
    private GameObject soundObj;
    private float cooldown1;
    private float cooldown2;

    public float joyVert;
    public float joyHorz;
    public bool joystickController;

    private void Start()
    {
        camera = Camera.main;
        cooldown1 = Time.time;
        cooldown2 = Time.time;
    }

    void Update() {

        if (!joystickController)
        {
            mouseAim();
            if (Input.GetMouseButton(0) && Time.time > cooldown1)
            {
                //AudioSource.PlayClipAtPoint(sound, camera.gameObject.transform.position, soundVolume);
                PlayClipAt(sound, camera.gameObject.transform.position);
                
                Instantiate(bullet_punch, transform.position, transform.rotation);
                cooldown1 = Time.time + fireRate;
                //camera.GetComponent<CameraPosition>().setShake(.4f);
            }
            else if (Input.GetMouseButton(1) && Time.time > cooldown2)
            {
                Instantiate(bullet_punch, transform.position, transform.rotation);
                cooldown2 = Time.time + fireRate;
            }
        }
        else
        {
            joyVert = Input.GetAxis("FireJoystickVertical");
            joyHorz = Input.GetAxis("FireJoystickHorizontal");

            if (((Math.Abs(joyHorz) > 0.5f) || (Math.Abs(joyVert) > 0.5f)) && Time.time > cooldown1)
            {
                //print("joyHorz" + joyHorz);
                //print("joyVert" + joyVert);
                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((joyVert), (joyHorz)) * Mathf.Rad2Deg);

                //AudioSource.PlayClipAtPoint(sound, camera.gameObject.transform.position, soundVolume);
                PlayClipAt(sound, camera.gameObject.transform.position);

                Instantiate(bullet_punch, transform.position, transform.rotation);
                cooldown1 = Time.time + fireRate;
            }
        }
    }

    AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos; 
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); 
        aSource.clip = clip; 

        aSource.pitch = (UnityEngine.Random.Range(0.6f, 1f));
        aSource.volume = soundVolume;

        aSource.Play(); 
        Destroy(tempGO, clip.length); 
        return aSource;
    }

    void mouseAim()
    {
        //Grab the current mouse position on the screen
        mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - camera.transform.position.z));

        //Rotates toward the mouse
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
    }
}
