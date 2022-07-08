using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {

	private GameObject c;


	// Use this for initialization
	void Start () {

//		c = GetComponent<Canvas> ();
		c.SetActive (false);


		SceneManager.LoadScene (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
