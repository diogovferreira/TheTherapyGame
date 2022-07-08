using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Transform gameOverCanvas;
	public Text timerText;
	public SetTime SetTimeScript;
	public CountDownScript CountDownScript;


	private float gameTime;
	private KinectManager m;

	public GameEntryData game;

	protected static GameOver instance = null;






	// Use this for initialization
	void Start () {
		m = KinectManager.Instance;

		//Funcção que vai definir o tempo de jogo
		defineTime(SetTimeScript.getTime());
		game.gameTime = (int)SetTimeScript.getTime ();

	}


	public static GameOver Instance
	{
		get
		{
			return instance;
		}
	}

	void Update(){
		if (m.IsUserDetected () && gameTime != 0 && CountDownScript.IsCountDownDone() == true ) {
			timeIsUp ((int)gameTime);
			gameTime -= Time.deltaTime;
		} else {
			//criar exception caso uma das condiçoes assima não se verificar.
			//			print ("TEMPO DE JOGO: " + gameTime);
		}
	}



	//Se o tempo acabar e não tiver chegado á pontuação para passar de nivel
	void timeIsUp(int time){
		timerText.text = time.ToString ();
		if (time <= 0) {
			if (gameOverCanvas.gameObject.activeInHierarchy == false) {
				Time.timeScale = 0;
				gameOverCanvas.gameObject.SetActive (true);

				game.gameResult = "LOST";

			} else {
				return;
			}
		}
	}


	public void defineTime(float s){
		gameTime = s;
	}

	public float getCurrentGameTime(){
		return gameTime;
	}




}