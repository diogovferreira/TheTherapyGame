using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseWinGameOverMenuScript : MonoBehaviour {



	public Transform PauseCanvas;

	private Button b;


	void Start(){
		b = GetComponent<Button> ();
	}

	public void buttonClicked(){
		if (b.name == "Resume") {
			Time.timeScale = 1;
			PauseCanvas.gameObject.SetActive (false);
		} 

	}



}
