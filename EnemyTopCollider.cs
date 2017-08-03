﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyTopCollider : MonoBehaviour {

public EnemyScript EnemyAccessObject;

//get access to the EnemyScript
void Update ()
	{

        if (EnemyAccessObject == null)
        {
			EnemyAccessObject = gameObject.GetComponentInParent<EnemyScript>();
        }
        if (EnemyAccessObject != null)
        {
            //Debug.Log("Cannot find 'EnemyScript' script");
        }
	}

//detect wall collision
void OnTriggerStay2D(Collider2D hitWall)
    {
		if ((hitWall.gameObject.tag == "Wall") || (hitWall.gameObject.tag == "PlayerTopCollider"))
			{
			EnemyAccessObject.setTopCollision();
			}
	}
	
//detect no wall contact
void OnTriggerExit2D(Collider2D noWall)
{
	if((noWall.gameObject.tag == "Wall") || (noWall.gameObject.tag == "PlayerTopCollider")) 
			EnemyAccessObject.setTopCollisionOff();
}
}
