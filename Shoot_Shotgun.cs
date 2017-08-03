using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Shotgun : MonoBehaviour
{

    public GameObject bullet_shotgun;
    public float fireRate;
    public float offset;
    public int shots;
    public float knockback;

    private GameObject camObj;
    private Camera camera;
    private Vector3 mousePosition;
    private float cooldown;
    private GameObject player;


    private void Start()
    {
        camObj = GameObject.FindGameObjectWithTag("MainCamera");
        camera = camObj.GetComponent<Camera>();
        cooldown = Time.time;
        player = transform.parent.gameObject;
    }

    void Update()
    {

        mouseAim();
        if (Input.GetMouseButtonDown(0) && Time.time > cooldown)
        {
            for (int i = 0; i < shots; i++)
            {
                Instantiate(bullet_shotgun, transform.position, randomOffset(transform.rotation, offset));
            }
            cooldown = Time.time + fireRate;
            player_knockback();
        }
        if (Input.GetMouseButtonDown(1) && Time.time > cooldown)
        {
            for (int i = 0; i < shots; i++)
            {
                Instantiate(bullet_shotgun, transform.position, randomOffset(transform.rotation, offset*2));
            }
            cooldown = Time.time + fireRate;
            player_knockback();
        }
    }

    void mouseAim()
    {
        //Grab the current mouse position on the screen
        mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - camera.transform.position.z));

        //Rotates toward the mouse
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
    }

    Quaternion randomOffset(Quaternion initial, float offset_range)
    {
        float offset = Random.Range(-offset_range, offset_range);
        Quaternion offQuat = Quaternion.Euler(0, 0, offset);
        return initial * offQuat;
    }

    void player_knockback()
    {
        player.transform.Translate(-transform.right * knockback * Time.deltaTime);
    }
}
