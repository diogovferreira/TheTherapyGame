using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorColider : MonoBehaviour {

	private Transform t;


	void Start(){
		t = GetComponent<Transform> ();
	}


	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("floor")){
			Debug.Log("Estou a tocar no chão");
			Debug.Log ("O pé que está a tocar é:" + t.gameObject.ToString ());
		}
	}

}
