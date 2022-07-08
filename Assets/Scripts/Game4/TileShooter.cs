using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TileShooter : MonoBehaviour {


	private float fireRate;//
	public GameObject [] tiles;
	private float speed;
	private float tileRange;

	private float seconds;
	private Rigidbody rb;
	private KinectManager k;
	private Transform t;
	private List <Vector3> possiblePositions = new List<Vector3> ();
	public SetTime SetTimeScript;



	public Material yellow;
	public Material black;
	public Material red;
	public Material blue;
	public Material orange;
	public Material darkYellow;
	public Material darkBlue;
	public Material grey;





	// Use this for initialization
	void Start () {
		t = GetComponent<Transform> ();
		k = KinectManager.Instance;

		string colourGood = PlayerPrefs.GetString ("goodTileColour");
		string colourBad = PlayerPrefs.GetString ("badTileColour");

		setTilesColour (colourGood);
		setBadTilesColour (colourBad);

		speed = SetTimeScript.getSpeed ();
		fireRate = SetTimeScript.getFireRate ();

		//posições possiveis para spam dos tiles
		tileRange = SetTimeScript.getRange();
		setPossibleTilePositions(tileRange);

	}
		

	void setTilesColour(string goodColour){

		if(goodColour.Equals("black")){
			tiles [0].GetComponent<Renderer>().material = black;
		}
		if(goodColour.Equals("yellow")){
			tiles [0].GetComponent<Renderer>().material = yellow;
		}
		if(goodColour.Equals("red")){
			tiles [0].GetComponent<Renderer>().material = red;
		}
		if(goodColour.Equals("blue")){
			tiles [0].GetComponent<Renderer>().material = blue;
		}
		if(goodColour.Equals("orange")){
			tiles [0].GetComponent<Renderer>().material = orange;
		}
		if(goodColour.Equals("dark yellow")){
			tiles [0].GetComponent<Renderer>().material = darkYellow;
		}
		if(goodColour.Equals("dark blue")){
			tiles [0].GetComponent<Renderer>().material = darkBlue;
		}
		if(goodColour.Equals("grey")){
			tiles [0].GetComponent<Renderer>().material = grey;
		}
			
	}

	void setBadTilesColour(string badColour){

		if(badColour.Equals("black")){
			tiles [1].GetComponent<Renderer>().material = black;
		}
		if(badColour.Equals("yellow")){
			tiles [1].GetComponent<Renderer>().material = yellow;
		}
		if(badColour.Equals("red")){
			tiles [1].GetComponent<Renderer>().material = red;
		}
		if(badColour.Equals("blue")){
			tiles [1].GetComponent<Renderer>().material = blue;
		}
		if(badColour.Equals("orange")){
			tiles [1].GetComponent<Renderer>().material = orange;
		}
		if(badColour.Equals("dark yellow")){
			tiles [1].GetComponent<Renderer>().material = darkYellow;
		}
		if(badColour.Equals("dark blue")){
			tiles [1].GetComponent<Renderer>().material = darkBlue;
		}
		if(badColour.Equals("grey")){
			tiles [1].GetComponent<Renderer>().material = grey;
		}

	}

	void setPossibleTilePositions(float range){

		if (range > 0.7f && range < 1) {
			possiblePositions.Add (new Vector3 (t.position.x + range, t.position.y, t.position.z));
			possiblePositions.Add (new Vector3 (t.position.x - range, t.position.y, t.position.z));
		} else {
			range = 0.7f;
			possiblePositions.Add (new Vector3 (t.position.x + range, t.position.y, t.position.z));
			possiblePositions.Add (new Vector3 (t.position.x - range, t.position.y, t.position.z));
		}

	}


	// Update is called once per frame
	void Update () {
//		StartCoroutine (waitSpwaner ());
		if (k.IsUserDetected()) {
			spawnTiles (fireRate);
		}
	}

	//fireRate variable decides how many seconds it wiil wait till the next tile is spawn
	void spawnTiles(float fireRate){
		seconds += Time.deltaTime;

		if (fireRate == 0) {
			return;
		} else if (seconds > fireRate) {
			int randomVec = Random.Range (0, possiblePositions.Count);
			Vector3 position = possiblePositions [randomVec];

			int randomObj = Random.Range (0, tiles.Length);
			GameObject bullet = Instantiate (tiles [randomObj], position, t.rotation) as GameObject;

			//Obter o rigidbody do missile/bala
			Rigidbody temp = bullet.GetComponent<Rigidbody> ();
			//set the shoot direction
			temp.AddForce (bullet.transform.forward);
			//setting the shoot velocity
			temp.velocity = new Vector3 (0, 0, -speed);


			Destroy (bullet, 4f);

			seconds = 0;
		}

	}




}
