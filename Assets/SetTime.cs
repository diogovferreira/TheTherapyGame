using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SetTime : MonoBehaviour {

	private static float playerSpeed = 0;//1 by default
	private static float time = 0;
	private static int scoreObjective;
	private static float range;
	private static float fireRate;
	private static int lifes;
	private static float hight;

	//Inf runner
	private static int bombs;
	private static int diamonds;
	private static int poison;
	private static int medicPack;


	public static Toggle LeftLeg;
	public static Toggle Righleg;
	public static Toggle Horizontal;
	public static Toggle Vertical;
	public static Toggle FrontMovement;
	public static Toggle BackMovement;


	private static SetTime instance = null;

	public Dropdown goodTiles;
	public Dropdown badTiles;

	public List<string> colours = new List<string> ();


	private Button b;

	// Use this for initialization
	void Start () {
		b = GetComponent<Button> ();
		colours.Add ("black");
		colours.Add ("yellow");
		colours.Add ("red");
		colours.Add ("blue");
		colours.Add ("orange");
		colours.Add ("dark yellow");
		colours.Add ("dark blue");
		colours.Add ("grey");


		goodTiles.AddOptions (colours);
		badTiles.AddOptions (colours);
	}


	public void selectColourGood(){
		PlayerPrefs.SetString ("goodTileColour", goodTiles.options [goodTiles.value].text);
	}

	public void selectColourBad(){
		PlayerPrefs.SetString ("badTileColour", badTiles.options [badTiles.value].text);
	}



	public static SetTime Instance {
		get {
			return instance;
		}
	}


	public void WriteForLeaning(string path, string otherData){
		string data;
		string time = "Time: " + getTime ().ToString ();
		string playerSpeed = "Player Speed: " + getSpeed ().ToString ();
		string pointsRequired = "Score Objective: " + getScore ().ToString ();
		string ObjectSpawnRange = "Object SpawnRange: " + getRange ().ToString ();
		string FireRate = "Fire Rate: " + getFireRate ().ToString ();

		data = time + playerSpeed + pointsRequired + ObjectSpawnRange + FireRate + "\n" + otherData;

		File.WriteAllText (path, data);

	}


	//LEANING GAME VARIABLES
	public void setSpeed (string newspeed){
		playerSpeed = (float)int.Parse (newspeed);
	}


	public void setTime (string newTime){
		time = (float)int.Parse (newTime);

	}

	public void setScore(string newScore){
		scoreObjective = int.Parse (newScore);
	}

	public void setRange(string newRange){
		range = (float)int.Parse (newRange);
	}

	public void SetFireRate(string newFireRate){
		fireRate = (float)int.Parse (newFireRate);
	}

	public void SetLifes(string newLifes){
		lifes = int.Parse (newLifes);
	}

	public void SetBombs(string newBombs){
		bombs = int.Parse (newBombs);
	}

	public void SetPoison(string newPoison){
		poison = int.Parse (newPoison);
	}

	public void SetDiamonds(string newDiamonds){
		diamonds = int.Parse (newDiamonds);
	}

	public void SetMedic(string newMedic){
		medicPack = int.Parse (newMedic);
	}


	public int getLifes(){
		return lifes;
	}

	public float getFireRate(){
		return fireRate;
	}

	public float getRange(){
		return range;
	}

	public int getScore(){
		return scoreObjective;
	}

	public float getSpeed(){
		return playerSpeed;
	}
		
	public float getTime(){
		return time;
	}


	public int getBombs(){
		return bombs;
	}

	public int getDiamonds(){
		return diamonds;
	}

	public int getMedicPack(){
		return medicPack;
	}

	public int getPoison(){
		return poison;
	}

	//STEP ON THE TILE VARIABLES

	public Toggle getLeftToggle(){
		return LeftLeg;
	}

	public Toggle getRightToggle(){
		return Righleg;
	}

	public Toggle getHorizontalToggle(){
		return Horizontal;
	}

	public Toggle getVerticalToggle(){
		return Vertical;
	}

	//Se o botão a ser carregado tiver de nome(30,60,90,12
		


}
