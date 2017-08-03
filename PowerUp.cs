using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject punchGun;
    public GameObject shotGun;

    private GameObject currentGun;
    private int powerup;
    // Use this for initialization
    void Start()
    {
        currentGun = punchGun;
        currentGun.SetActive(true);
        punchGun.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            activatePowerup(powerup);
        }
    }

    void switchGun(int id)
    {
        switch (id)
        {
            case 1:
                if (currentGun != punchGun)
                {
                    currentGun.SetActive(false);
                    currentGun = punchGun;
                    currentGun.SetActive(true);
                }
                break;
            case 2:
                if (currentGun != shotGun)
                {
                    currentGun.SetActive(false);
                    currentGun = shotGun;
                    currentGun.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

  

    private void activatePowerup(int type)
    {
        switch (type)
        {
            default:
                break;
            case 1:
                GetComponent<playerScript>().UpgradeHealth(5);
                break;
        }
    }

    public void setPowerup(GameObject other)
    {
        powerup = other.GetComponent<GetType>().getPowerupType();
    }

}
