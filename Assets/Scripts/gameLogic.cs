using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class gameLogic : MonoBehaviour {


	private Rigidbody rb;
	private int score;
	private  int scoreO;
	private  float gameTime;
	private bool levelWon;


	private KinectManager kinect;

	public Transform WinCanvas;
	public Transform gameOverCanvas;


	public Transform playerTransform;
	public SetTime st;
	public game2GestureListener gestureListener;
	public CountDownScript CountDownScript;
	public PopMessages popm;


	public Text bombText;//decrease points
	public Text starText;//increase points
	public Text crossedText;//+sizePlayer
	public Text poisonText;//-sizePlayer

	private static int countBomb;
	private static int countStar;
	private static int countCrossed;
	private static int countPoison;


	public GameEntryData game;
	public LeaningDataEntry lDE;
	public XMLGameManager xml;
	public XMLManager xmlManager;


	public Text WinScoreDisplay;//display da pontuação quando ganha.
	public Text scoreDisplay;
	public Text timerText;
	public Text messageDisplay;

	public static bool restartL = false;

	public AudioSource GoodObjsSound;
	public AudioSource BadObjsSound;
	public AudioSource WinSound;
	public AudioSource GameOverSound;


	private bool Lost;
	private bool Won;

	private string InsertGameDataURL = "193.136.221.122/therapy_game/Lgd.php";
	private string InsertDataURL = "193.136.221.122/therapy_game/InsertLgData.php";


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		kinect = KinectManager.Instance;

		gameTime = st.getTime ();
		timerText.text = st.getTime ().ToString ();

	
		scoreO = st.getScore ();

		Time.timeScale = 1;

	}









	
	// Update is called once per frame
	void Update () {

		LevelWon (score);
		if (kinect.IsUserDetected() && gameTime != 0 && CountDownScript.IsCountDownDone() == true ) {
			timeIsUp ((int)gameTime);
			gameTime -= Time.deltaTime;
		} else {
			//criar exception caso uma das condiçoes assima não se verificar.
			//			print ("TEMPO DE JOGO: " + gameTime);
		}

	}
		




	//Trigger cada vez que uma capsule embate no player
	//Desaparece com o objecto e incrementa o score
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("StarPoints")) {//gain Points
			messageDisplay.text = popm.getGoodMessages();
			GoodObjsSound.Play();
			other.gameObject.SetActive (false);
			score++;
			scoreDisplay.text = score.ToString ();
			countStar++;

		} else if (other.gameObject.CompareTag ("Bomb")) {//Lose Points
			messageDisplay.text = popm.getBadMessages();
			BadObjsSound.Play();
			other.gameObject.SetActive (false);
			countBomb++;
			if (score > 0) {
				score--;

			} else {//if the score is alredy 0
				score = 0;
			}
			scoreDisplay.text = score.ToString ();

		} else if (other.gameObject.CompareTag ("SizeToken")) {//increase player size
			messageDisplay.text = popm.getGoodMessages();
			GoodObjsSound.Play();
			other.gameObject.SetActive (false);
			playerTransform.localScale += new Vector3 (0.5F, 0, 0);
			countCrossed++;
		} else if (other.gameObject.CompareTag ("SizeTokenLess")) {//decrease playerSize
			messageDisplay.text = popm.getBadMessages();
			BadObjsSound.Play();
			other.gameObject.SetActive (false);
			playerTransform.localScale -= new Vector3 (0.5F, 0, 0);
			countPoison++;		
		}
	}





	/// <summary>
	/// Check if the player reached the defined score, trigger win action
	/// </summary>
	/// <param name="s">S.</param>
	public void LevelWon (int s){
		s = score;
		if (score == scoreO) {
			Time.timeScale = 0;
			levelWon = true;

			if (WinCanvas.gameObject.activeInHierarchy == false) {
				WinSound.Play ();
				WinCanvas.gameObject.SetActive (true);
				WinScoreDisplay.text = "TOTAL: " + score.ToString ();


				starText.text = countStar.ToString ();
				bombText.text = countBomb.ToString ();
				crossedText.text = countCrossed.ToString ();
				poisonText.text = countPoison.ToString ();

				game.gameResult = "WON";
				Won = true;
				processLeaningData ();


				//PatientsData
				StartCoroutine (SaveGameData());


			}} }



	/// <summary>
	/// T//Function that verifies if the times is up, trigger lose action
	/// </summary>
	/// <param name="time">Time.</param>
	void timeIsUp(int time){
		timerText.text = time.ToString ();
		if (time <= 0) {
			if (gameOverCanvas.gameObject.activeInHierarchy == false) {
				Time.timeScale = 0;
				GameOverSound.Play ();

				gameOverCanvas.gameObject.SetActive (true);

				Lost.Equals (true);
				game.gameResult = "LOST";


				processLeaningData ();

				//PatientsData
				StartCoroutine (SaveGameData());

				} else {
				return;
			}}}


	public void printSides(){
		List<string> sides = gestureListener.getLeaningSide ();
		print (sides.Count);
		for (int i = 0; i < sides.Count; i++) {
			print ("Lado:" + sides [i].ToString ());

		}

	}

	public void processLeaningData(){
		List<int> angles = gestureListener.getAngulos ();
		List<int> timeStamps = gestureListener.getTimeStamps ();
		List<string> LeaningSides = gestureListener.getLeaningSide ();


		for (int i = 0; i < angles.Count; i++) {
			if (xmlManager.LeaningDB.list.Count.Equals (0)) {
				LeaningDataEntry leaningMov = new LeaningDataEntry ();
				leaningMov.leaningAngle = angles [i];
				leaningMov.leaningTime = timeStamps [i];
				leaningMov.leaningSide = LeaningSides [i];

				xmlManager.LeaningDB.list.Add (leaningMov);
			} else {
				int previousTime = timeStamps [i - 1];
				int actualTime = timeStamps [i];
				if (!(actualTime.Equals (previousTime))) {
					LeaningDataEntry leaningMov = new LeaningDataEntry ();
					leaningMov.leaningAngle = angles [i];
					leaningMov.leaningTime = timeStamps [i];
					leaningMov.leaningSide = LeaningSides [i];

					xmlManager.LeaningDB.list.Add (leaningMov);
				}

			}
		}
		print ("TAMANHOACTUAL:" + xmlManager.LeaningDB.list.Count);
		printSides ();
	}


	IEnumerator SaveLeaningData(){
		WWWForm form = new WWWForm ();
		foreach (LeaningDataEntry l in xmlManager.LeaningDB.list) {
			
			form.AddField ("anglePost", l.leaningAngle.ToString());
			form.AddField ("timeStampPost", l.leaningTime.ToString());
			form.AddField ("leaningSidePost", l.leaningSide);


			WWW www = new WWW (InsertDataURL, form);
			yield return www;
		}

	}



	IEnumerator SaveGameData(){


		WWWForm form = new WWWForm ();

		form.AddField ("timePost", ((int)st.getTime()));
		form.AddField ("playerSpeedPost", ((int)st.getSpeed()).ToString());
		form.AddField ("spwanRangePost", ((int)st.getRange()).ToString());
		form.AddField ("fireRatePost", ((int)st.getFireRate()).ToString());
		form.AddField ("scoreObjectivePost", scoreO);
		form.AddField ("finalScorePost", score);
		form.AddField ("bombPost", countBomb);
		form.AddField ("starsPost", countStar);
		form.AddField ("poisonPost", countPoison);
		form.AddField ("wrenchPost", countCrossed);
		print ("flag1");

		if (Won) {
			print ("flag2");
			form.AddField ("gameResultPost", "WIN");

		} else {
			print ("flag3");
			form.AddField ("gameResultPost", "LOST");
		}

		form.AddField ("userIdPost", PlayerPrefs.GetInt("User_ID"));
		form.AddField ("sessionIdPost", PlayerPrefs.GetInt ("session_id"));

		WWW www = new WWW (InsertGameDataURL,form);
		yield return www;


		StartCoroutine (SaveLeaningData ());



	}


	public bool isLevelWon(){
		return levelWon;
	}

	public int getCurrentTime(){
		return (int)gameTime;

	}


		
}
