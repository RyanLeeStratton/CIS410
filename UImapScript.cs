using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImapScript : MonoBehaviour {

    public Image minimapImg;
    public GameObject goActivate; // yeah, im piggybacking on some code...
    private float timer;
    public bool on;
    private bool isOnce = true;

    private void Update()
    {
        if (on) {
            if (timer >= 1)
            {
                minimapImg.GetComponent<Image>().color = new Color(211, 211, 211);

            }
            else if (timer >= 0)
            {
                minimapImg.GetComponent<Image>().color = Color.green;
            }
            if (timer < 0)
            {
                timer = 2f;
            }
            timer -= Time.deltaTime;
            print("What?");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
    
        if ((collision.gameObject.tag == "Player")) {
            on = true;
            if ((goActivate.activeSelf == false) && (goActivate != null)&&(isOnce))
            {
                goActivate.SetActive(true);
                isOnce = false;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
            minimapImg.GetComponent<Image>().color = new Color(211, 211, 211);
        on = false;
    }

}
