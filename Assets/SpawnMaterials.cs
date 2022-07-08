using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class SpawnMaterials : MonoBehaviour {

	private int countWalls;
	private int StartPoints;
	private int EndPoints;

	public GameObject wall;
	public GameObject player;
	public GameObject finishPoint;

	public GameObject gameElements;
	private static List<GameObject> newMap = new List<GameObject> ();
	private static List<Vector3> newMapP = new List<Vector3>();
	private static List<Quaternion> newMapR = new List<Quaternion>();



	//Provisorio
	public List<GameObject> Map = new List<GameObject> ();
	private static List<Vector3> MapP = new List<Vector3>();
	private static List<Quaternion> MapR = new List<Quaternion>();


	public XMLGameManager xmlGM;
	private string mapName;

	public MapDatabase mapDB;




	public void createWall(){
		wall.transform.position = new Vector3 (0f, 6.30f, 6.24f);
		GameObject NewWall = Instantiate (wall,wall.transform.position, wall.transform.rotation);

		newMap.Add (NewWall);


	}
		
	public void SaveMap(){

		foreach (GameObject g in newMap) {

			newMapP.Add (g.transform.position);
			newMapR.Add (g.transform.rotation);

		}



		MapEntryData map = new MapEntryData ();

		print (newMap.Count);
		print (player.transform.position);
		print (finishPoint.transform.position);

		map.WallPos = newMapP;
		map.WallRot = newMapR;
		//map.PlayerPos = player.transform.position;
		//map.CheckPointPos = finishPoint.transform.position;

		mapDB.mapList.Add (map);


		xmlGM.SaveHitMap (mapDB);
	}




		


}
