using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class roomSpawner : MonoBehaviour 
{
	public List<GameObject> roomList;

	//public List<GameObject> spawnList;

	public GameObject[] roomListArray;

	public GameObject[] spawnListArray;

	private Transform tempTransform;

	void Start () 
	{
		GenerateSpawnpoints ();
		
		foreach (GameObject sp in spawnListArray) {
			GenerateRoom (sp);
		}
	}

	void GenerateSpawnpoints (){

		spawnListArray = GameObject.FindGameObjectsWithTag ("RoomSpawn");

		/*spawnListArray = Resources.LoadAll<GameObject>("Roomspawnpoints");

		spawnList = spawnListArray.ToList();
		foreach (GameObject sp in spawnListArray) {
			tempTransform = sp.GetComponent<Transform> ();
			GameObject newRoom = Instantiate (sp, tempTransform.position, Quaternion.identity, this.transform) as GameObject;
		}*/
	}

	void GenerateRoom (GameObject sp)
	{
		tempTransform = sp.GetComponent<Transform> ();

		roomListArray = Resources.LoadAll<GameObject>("Rooms");

		roomList = roomListArray.ToList();

		GameObject roomToBuild = roomList [Random.Range (0, roomList.Count)];
		GameObject newRoom = Instantiate (roomToBuild, 
			new Vector3 (tempTransform.position.x+2,tempTransform.position.y,tempTransform.position.z+2), 
			Quaternion.identity, this.transform ) as GameObject;
	}
}