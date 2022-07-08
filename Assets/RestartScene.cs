using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour {



	public void ExitPrefs(){
		if (SceneManager.GetActiveScene ().name.Equals ("Game2-level 1")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Game4-Level1")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("ForestSurfer")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Level1")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Level2")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Level3")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Level4")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Level5")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("CustomL")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("CustomLevel")) {
			PlayerPrefs.SetString ("Game", "Leaning");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("CharactererSelection")) {
			PlayerPrefs.SetString ("Game", "Menu");
		}

	}


	public void passRestartPrefs(){
		if (SceneManager.GetActiveScene ().name.Equals ("Game2-level 1")) {
			PlayerPrefs.SetString ("Game", "Leaning");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Game4-Level1")) {
			PlayerPrefs.SetString ("Game", "Step");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("ForestSurfer")) {
			PlayerPrefs.SetString ("Game", "Runner");
		}
		if (SceneManager.GetActiveScene ().name.Equals ("CustomL")) {
			PlayerPrefs.SetString ("Game", "Hit");
		}

	}
}
