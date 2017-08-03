using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour {

    public GameObject[] wallArr;
    public GameObject[] portalArr;
    public int portalCount;
    public bool wallsDown;
    private bool enterOnce;
    public bool bossRoom;
    private GameObject lights;
    private GameObject[] enemies;

    private void Awake()
    {
        //portalCount = portalArr.Length;
        enterOnce = true;
        lights = GameObject.Find("Lights");
    }

    private void Update()
    {

        if (wallsDown && (wallArr[1].activeSelf))
        {
            foreach (GameObject wall in wallArr)
            {
                wall.SetActive(false);
            }
            
            clearEnemies();
            lights.SetActive(true);
        }
        else if (!wallsDown && !enterOnce && !bossRoom)
        {
            foreach (GameObject portal in portalArr)
            {
                if (portal.active)
                    portalCount++;
            }

            if (portalCount <= 0)
            {
                wallsDown = true;
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies) {
                    Destroy(enemy);
                }
            }
            portalCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player")&& enterOnce)
        {
            foreach (GameObject portal in portalArr)
            {
                portal.SetActive(true);
            }
            enterOnce = false;
            lights.SetActive(false);
			GameObject.Find("Main Camera").GetComponent<CameraPosition>().setShake(.4f);
        }
    }

    void clearEnemies() {

    }
}
