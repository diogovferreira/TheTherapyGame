using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTokens : MonoBehaviour {

	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag ("Token")){
			other.gameObject.SetActive (false);


		}
	}
}
			