using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public GameObject[] players;

	public Vector3 offset;



	// Use this for initialization
	void Start () {

		foreach (GameObject go in players) {
			offset = transform.position - go.transform.position;
		}
			
	}
	
	// Update is called once per frameg
	void Update () {
		foreach (GameObject go in players) {
			transform.position = go.transform.position + offset;
		}

	}
}
