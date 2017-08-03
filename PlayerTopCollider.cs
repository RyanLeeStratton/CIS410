﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTopCollider : MonoBehaviour {

private playerScript playerAccess;

//get access to the playerScript
void Start ()
	{
		GameObject playerAccessObject = GameObject.FindGameObjectWithTag("Player");

        if (playerAccessObject != null)
        {
            playerAccess = playerAccessObject.GetComponent<playerScript>();
        }
        if (playerAccessObject == null)
        {
            Debug.Log("Cannot find 'playerScript' script");
        }
	}

//detect wall collision
void OnTriggerEnter2D(Collider2D hitWall)
    {
		if ((hitWall.gameObject.tag == "Wall"))
			{
				playerAccess.setTopCollision();
			}
	}
	
//detect no wall contact
void OnTriggerExit2D(Collider2D noWall)
{
	if((noWall.gameObject.tag == "Wall"))
		playerAccess.setTopCollisionOff();
}
}
