using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownScript : MonoBehaviour {

	public bool countDowndone = false;
	public GameObject CountDownImage;

	void Update(){
		if (countDowndone) {
			CountDownImage.SetActive (false);
		
		}
	}

	public bool IsCountDownDone(){
		return countDowndone;
	}

}
