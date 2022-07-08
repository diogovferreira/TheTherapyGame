using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLevel : MonoBehaviour {


	public Transform WinCanvas;

	private int score;
	private bool Won;
	public Text WinScoreDisplay;
	public Text currentScore;
	private KinectManager kinect;


	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		levelWon (score);
	}


	void levelWon(int s){//Alternativamente para a frente criar um switch/case consoante o nivel(maior a pontuação)
		s = score;
		if (score == 4) {
			Time.timeScale = 0;
			Won = true;
			if (WinCanvas.gameObject.activeInHierarchy == false) {
				WinCanvas.gameObject.SetActive (true);
				WinScoreDisplay.text = currentScore.text;
			}
		}
	}
}
