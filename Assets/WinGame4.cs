using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame4 : MonoBehaviour {

	public Transform Wincanvas;
	public Text winscoreDisplay;
	public Text currentScore;

	private int Totalscore;
	private bool Won;



	void levelWon(int s){
		
		s = Totalscore;
		if (Totalscore == 10) {
			Time.timeScale = 0;//Pause Time
			Won = true;
			if (Wincanvas.gameObject.activeInHierarchy == false) {
				Wincanvas.gameObject.SetActive (true);
				winscoreDisplay.text = Totalscore.ToString ();

			}
		}
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		levelWon (Totalscore);
	}
}
