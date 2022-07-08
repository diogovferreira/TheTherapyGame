using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarts : MonoBehaviour {

	public GameObject Login;
	public GameObject LeaningPanel;
	public GameObject HitWallPanel;
	public GameObject StepTilePanel;
	public GameObject InfiniteRunnerPanel;
	public GameObject Menu;
	public GameObject ChooseType;
	public GameObject RegisterPhisio;
	public GameObject usersDropDown;
	string sceneName = null;
	public static int LoginCounter;



	void Start(){
		LoginCounter = 1;

	}

	void Awake(){
		string prefs = PlayerPrefs.GetString ("Game");

		if (LoginCounter == 1) {
			if (prefs.Equals ("Leaning")) {
				Login.SetActive (false);
				LeaningPanel.SetActive (true);
				RegisterPhisio.SetActive (false);

				usersDropDown.SetActive (false);

			}

			if (prefs.Equals ("Step")) {
				Login.SetActive (false);
				StepTilePanel.SetActive (true);
				RegisterPhisio.SetActive (false);

				usersDropDown.SetActive (false);


			}
			if (prefs.Equals ("Runner")) {
				Login.SetActive (false);
				InfiniteRunnerPanel.SetActive (true);
				RegisterPhisio.SetActive (false);

				usersDropDown.SetActive (false);

			}
			if (prefs.Equals ("Hit")) {
				Login.SetActive (false);
				HitWallPanel.SetActive (true);
				RegisterPhisio.SetActive (false);

				usersDropDown.SetActive (false);

			}
			if (prefs.Equals ("Menu")) {
				Login.SetActive (false);
				Menu.SetActive (false);
				ChooseType.SetActive (true);
				RegisterPhisio.SetActive (false);
			
				usersDropDown.SetActive (false);


			}if (prefs.Equals ("Login")) {
				Login.SetActive (true);
				Menu.SetActive (false);
				ChooseType.SetActive (false);
				RegisterPhisio.SetActive (false);
			
				usersDropDown.SetActive (false);
			}
		}

	}


	public void QuitGame(){
		Application.Quit ();

	}


	// Update is called once per frame
	void Update () {
		
	}
}
