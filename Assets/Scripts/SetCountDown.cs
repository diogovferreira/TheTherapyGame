using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDown : MonoBehaviour {

	private CountDownScript cd;


	public void CountNow(){

		if (GameObject.Find ("GameController").GetComponent<CountDownScript> () != null) {
			cd = GameObject.Find ("GameController").GetComponent<CountDownScript> ();
			cd.countDowndone = true;
		} 
			
	}

}
