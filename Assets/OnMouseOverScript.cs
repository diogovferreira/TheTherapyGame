using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverScript : MonoBehaviour {




	public Image gameImage;
	public Image gameImage2;


	public void showLeaning(){
		gameImage.gameObject.SetActive (true);
	}

	public void hideLeaning(){
		gameImage.gameObject.SetActive (false);
	}


	public void hideStep(){
		gameImage.gameObject.SetActive (false);
		gameImage2.gameObject.SetActive (false);
	}

	public void showStep(){
		gameImage.gameObject.SetActive (true);
		gameImage2.gameObject.SetActive (true);
	}


	public void showWall(){
		gameImage.gameObject.SetActive (true);
	}

	public void hideWall(){
		gameImage.gameObject.SetActive (false);
	}


	public void showInfinite(){
		gameImage.gameObject.SetActive (true);
	}

	public void hideInfinite(){
		gameImage.gameObject.SetActive (false);
	}


}
