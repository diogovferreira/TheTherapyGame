using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrennshot : MonoBehaviour {
	private Texture2D screenCap;
	private Texture2D border;
	bool shot = false;

	// Use this for initialization
	void Start () {
		screenCap = new Texture2D (300, 200, TextureFormat.RGB24, false);
		border = new Texture2D (2, 2, TextureFormat.ARGB32, false);
		border.Apply ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			StartCoroutine ("Capture");
		}
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect (200, 100, 300, 2), border, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (200, 300, 300, 2), border, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (200, 100, 2, 200), border, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (500, 100, 2, 200), border, ScaleMode.StretchToFill);

		if (shot) {
			GUI.DrawTexture (new Rect (10, 10, 60, 40), screenCap, ScaleMode.StretchToFill);
		}

	}


	IEnumerator Capture(){
		yield return new WaitForEndOfFrame ();
		screenCap.ReadPixels (new Rect (198, 98, 298, 198), 0, 0);
		screenCap.Apply ();
	}
}
