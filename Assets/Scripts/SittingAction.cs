 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SittingAction : MonoBehaviour {

	public int score;
	public Text scoreText;

	private int count;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		score = 0;


	}

	public void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("capsule")) {
			c.gameObject.SetActive (false);
			score++;
			scoreText.text = "SCORE: " + score; 
		}
	}
}
