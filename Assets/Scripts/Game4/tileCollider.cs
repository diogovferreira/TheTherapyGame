using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileCollider : MonoBehaviour {

	//Game canvas and displays
	public Transform WinCanvas;
	public Transform gameOverCanvas;



	public Text scoreText;
	public Text WinScoreDisplay;
	public Text timerText;
	public Text messagesDisplay;


	//Colliders(Left and Right Foot)
	public Collider LCollider;
	public Collider RCollider;

	//Sounds
	public AudioSource GoodTileSound;
	public AudioSource BadTileSound;
	public AudioSource WinSound;
	public AudioSource GameOverSound;

	//count foot ocurrences(Steps,tiles steped)
	private static int countLeft;
	private static int countRight;
	private static int countGood;
	private static int countBad;
	private static float angle;
	private static float distance;
	private static string FootUsed;

	//Booleans 
	private bool LeftFootOn = false;
	private bool RightFootOn = false;


	private float timeCount;
	private int seconds;
	private int stepNumber;
	private bool levelWon;
	private int scoreObjective;
	private int score;
	private float mediumVelocity;
	private float movementDuration;
	private float gameTime;


	public GameEntryData game;
	public XMLGameManager xml;
	public XMLManager xmlManager;
	public PopMessages popM;
	private List<string> Movements = new List<string> ();



	//Scripts
	public SetTime SetTimeScript;

	public TypeOfExercice tp;
	//get distance and angle data
	public ProcessData data;
	//script to get info on platform contact
	public OnPlatformCollider platcoll;
	public CountDownScript CountDownScript;
	private KinectManager kinectManager;
	//To get if the game is horizontal or vertical
	public LoadSceneOnClick2 ld;

	//different joints depending on the character(to calculate angles...)
	public Transform RFoot;
	public Transform LFoot;
	public Transform Pelvis;

	private GameObject foot;

	public bool Won = false;
	private string InsertStepGameDataURL = "193.136.221.122/therapy_game/Stt.php";
	private string InsertStepDataURL = "193.136.221.122/therapy_game/InsertSttData.php";



	void Start(){
		kinectManager = KinectManager.Instance;

//		LCollider.isTrigger.Equals (tp.getLeft ());
//		RCollider.isTrigger.Equals (tp.getRight ());
//		deactivateCollider ();

		gameTime = SetTimeScript.getTime ();
		scoreObjective = SetTimeScript.getScore ();


		stepNumber = 0;
		Time.timeScale = 1;
		}


	//Count number of times each foot collides with a tile(detect when tile is hit by foot)
	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("tile")) {
			//Set the object to false
			messagesDisplay.text = popM.getGoodMessages();
			c.gameObject.SetActive (false);
			//Plays sound
			GoodTileSound.Play();
			//Increase Counter
			countGood = countGood + 1;

			if (this.tag == "Lfoot"){
				countLeft = countLeft + 1;
			}
			if (this.tag == "Rfoot") {
				countRight = countRight + 1;
			}
			score++;
			scoreText.text = score.ToString ();

			//calculate angle and distance when tile is steped
			distance = calculateDistance ();
			angle = calculateAngle ();
			//Foot used to step on the tile
			FootUsed = this.tag;

			print ("entro aqui e não faço nada");

		}

		//BAD TILES DECREASE PONTUATION
		if (c.gameObject.CompareTag ("badTile")) {
			messagesDisplay.text = popM.getBadMessages();
			c.gameObject.SetActive (false);
			BadTileSound.Play ();
			countBad = countBad + 1;
			//calculate angle and distance when tile is steped
			distance = calculateDistance ();
			angle = calculateAngle ();
			//Foot used to step on the tile
			FootUsed = this.tag;

			if (score == 0) {
				score = 0;
				scoreText.text = score.ToString ();
			} else {
				score--;
				scoreText.text = score.ToString ();
			}}
	}


	//If reaches the score Objective triggers
	public void LevelWon (int s){
		s = score;
		if (score == scoreObjective && platcoll.onPlatform() == true) {
			Time.timeScale = 0;
			WinSound.Play ();
			levelWon = true;
			if (WinCanvas.gameObject.activeInHierarchy == false) {
				WinCanvas.gameObject.SetActive (true);
				WinScoreDisplay.text = "TOTAL: " + score.ToString ();

				//DATA TO WRITE WHEN THE GAME IS WON
				Won = true;


				StartCoroutine(	getGameStats ());




				game.gameResult = "WON";




			}}}



	/// <summary>
	/// Se o tempo acabar e não tiver chegado á pontuação para passar de nivel
	/// </summary>
	/// <param name="time">Time.</param>
	void timeIsUp(int time){
		timerText.text = ((int)gameTime).ToString ();
		if (time <= 0) {
			if (gameOverCanvas.gameObject.activeInHierarchy == false) {
				Time.timeScale = 0;
				GameOverSound.Play ();
				gameOverCanvas.gameObject.SetActive (true);

				Won = false;

				StartCoroutine(	getGameStats ());
			


			} else {
				return;
			}
		}
	}
		
	/// <summary>
	/// Deactivates the colliders
	/// </summary>
	//When choosing which leg to use(Customazition menu)
	void deactivateCollider (){

		LCollider.enabled.Equals(LeftFootOn);
		RCollider.enabled.Equals (RightFootOn);

//		if(LeftFootOn.Equals(true) && RightFootOn.Equals(true)){
//			print("Entro no 1");
//			LCollider.enabled = true;
//			RCollider.enabled = true;
//		}
//		if(LeftFootOn.Equals(true) && RightFootOn.Equals(false)){
//			print("Entro no 2");
//			LCollider.enabled = true;
//			RCollider.enabled = false;
//		}
//		if(LeftFootOn.Equals(false) && RightFootOn.Equals(true)){
//			print("Entro no 3");
//			LCollider.enabled = false;
//			RCollider.enabled = true;
//		}
	
	}


	/// <summary>
	/// Gets the game stats.
	/// </summary>
	/// 

	IEnumerator saveStepData(){
		WWWForm form = new WWWForm ();

		foreach(MovementEntry m in xmlManager.MovementDB.list){
			form.AddField("stepNumberPost",m.movementNumber);
			form.AddField ("anglePost", m.angle);
			form.AddField ("movementDurationPost", m.movementDuration.ToString());
			form.AddField ("movementVelocityPost", m.movementMediumVelocity);
			form.AddField ("stepDistancePost", m.distance);
			form.AddField ("footUsedPost", m.footUsed);
		
			WWW www = new WWW (InsertStepDataURL, form);
			yield return www;
		}
	}

	IEnumerator getGameStats(){

		WWWForm form = new WWWForm ();

		form.AddField ("timePost", ((int)SetTimeScript.getTime()));
		form.AddField ("fireRatePost", ((int)SetTimeScript.getFireRate()));
		form.AddField ("tileSpeedPost", ((int)SetTimeScript.getSpeed ()));
		form.AddField ("scoreObjectivePost", scoreObjective);
		form.AddField ("finalScorePost", score);
		form.AddField ("stepsLeftPost", countLeft);
		form.AddField ("stepsRightPost", countRight);
		form.AddField ("goodTilesPost", countGood);
		form.AddField ("badTilesPost", countBad);

	if (Won) {
			form.AddField ("gameResultPost", "WIN");
		} else {
			form.AddField ("gameResultPost", "LOST");
		}



		if (ld.getHorizontal()) {
		form.AddField ("gameTypePost", "Horizontal");

		} 
		if(ld.getVertical()){
		form.AddField ("gameTypePost", "Vertical");
		}



		form.AddField ("userIdPost", PlayerPrefs.GetInt("User_ID"));
		form.AddField ("sessionIdPost", PlayerPrefs.GetInt ("session_id"));


		WWW www = new WWW (InsertStepGameDataURL,form);
		yield return www;


		StartCoroutine (saveStepData ());


	}


	void Update(){
		LevelWon (score);

		if (kinectManager.IsUserDetected () && gameTime != 0 && CountDownScript.IsCountDownDone() == true ) {
			timeIsUp ((int)gameTime);
			gameTime -= Time.deltaTime;
		} else {
			//criar exception caso uma das condiçoes assima não se verificar.
			//			print ("TEMPO DE JOGO: " + gameTime);
		}

	}

	/// <summary>
	/// Calculates the distance.
	/// </summary>
	/// <returns>The distance.</returns>
	public float calculateDistance(){
		distance = Vector3.Distance (RFoot.position, LFoot.position);
		return distance;
	}

	/// <summary>
	/// C/	/// </summary>
	/// <returns>The angle.</returns>
	public float calculateAngle(){
		Vector3 leftFootPel = LFoot.position - Pelvis.position;
		Vector3 rightFootPel = RFoot.position - Pelvis.position;

		angle = Vector3.Angle (leftFootPel, rightFootPel);

		return angle;
	}

	//geters and setters

	void setLeftLeg(bool leg){
		if (leg.Equals (null)) {
			LeftFootOn.Equals (false);
		} else {
			LeftFootOn.Equals (leg);
		}
	}

	void setRightLeg(bool leg){
		if (leg.Equals (null)) {
			RightFootOn.Equals (false);
		} else {
			RightFootOn.Equals (leg);
		}
	}
		

	public float getDistance(){
		return distance;
	}

	public float getAngle(){
		return angle;
	}

	public string FootUsedName(){
		return FootUsed;
	}
		
	public float mediumVel(){
		return mediumVelocity;
	}


}
