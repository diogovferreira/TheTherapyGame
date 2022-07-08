using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {

//	public Transform[] possibleObstaclePositions;
//	public GameObject[] obstacles;
//	public GameObject[] obstaclesclones;
	private List<Vector3> possiblePositions = new List<Vector3> ();

	public GameObject diamondPrefab;
	public GameObject poisonPrefab;
	public GameObject bombPrefab;
	public GameObject mediPackPrefab;

	public Vector3 center;
	public Vector3 size;


	//variables to be defined by the user

	//diamonds(+1 points)
	private int numberOfDiamonds;

	//bombs(-1 life)
	private int numberOfBombs;


	//hearts(+1 life)
	private int numberOfMedicPack;

	//poison(-1 points)
	private int numberOfPoison;


	public SetTime gameVariables;

	private GameObject LeftArea;
	private GameObject RightArea;




	// Use this for initialization
	void Start () {

		numberOfBombs = gameVariables.getBombs ();
		numberOfDiamonds = gameVariables.getDiamonds ();
		numberOfPoison = gameVariables.getPoison();
		numberOfMedicPack = gameVariables.getMedicPack ();

//		numberOfBombs = 5;
//		numberOfDiamonds = 2;
//		numberOfMedicPack = 20;
//		numberOfPoison = 20;


		defineNumberofObjects (numberOfDiamonds,numberOfBombs,numberOfMedicPack,numberOfPoison);


	}


	void spwanDiamonds(){
		if (this.name == "LeftArea" || this.name == "RightArea" || this.name == "TopArea") {
			Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), Random.Range (-size.y / 2, size.y / 2), Random.Range (-size.z / 2, size.z / 2));
			Instantiate (diamondPrefab, pos, Quaternion.identity);
		}
	}


	void SpawnPoison(){
		if (this.name == "LeftArea" || this.name == "RightArea" || this.name == "TopArea") {
			Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), Random.Range (-size.y / 2, size.y / 2), Random.Range (-size.z / 2, size.z / 2));
			Instantiate (poisonPrefab, pos, Quaternion.identity);
		}
	}


	void SpawnBombs(){
		if (this.name == "BottomArea") {
			Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), Random.Range (-size.y / 2, size.y / 2), Random.Range (-size.z / 2, size.z / 2));
			Instantiate (bombPrefab, pos, Quaternion.identity);
		}
	}


	void SpawnMedicPack(){
		if (this.name == "BottomArea") {
			Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), Random.Range (-size.y / 2, size.y / 2), Random.Range (-size.z / 2, size.z / 2));
			Instantiate (mediPackPrefab, pos, Quaternion.identity);

		}
	}



	void OnDraw(){
		Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawCube (transform.localPosition + center, size);
	}




	void defineNumberofObjects(int Diamonds,int bombs,int medic,int poison){

		for(int i = 0; i < bombs ;i++){
			SpawnBombs ();
		}

		for(int i = 0; i < medic ;i++){
			SpawnMedicPack ();
		}

		for(int i = 0; i < poison ;i++){
			SpawnPoison ();
		}

		for(int i = 0; i < Diamonds ;i++){
			spwanDiamonds ();
		}


	}



}
