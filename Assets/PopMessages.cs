using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopMessages : MonoBehaviour {



	private string[] MiddleGameGoodLeaningMessageList;
	private string[] BadLeaningMessageList;




	public string getGoodMessages(){
		MiddleGameGoodLeaningMessageList = new string[]{"There you go!", "Nice job!", "Excellent!", "Keep Going!"};

		int randomMessageindex = Random.Range (0, MiddleGameGoodLeaningMessageList.Length);
		string message = MiddleGameGoodLeaningMessageList [randomMessageindex].ToString ();

		return message;

	}

	public string getBadMessages(){
		BadLeaningMessageList = new string[]{"Ops!", "Try to avoid that!" ,"Better watch out!", "Common you can do it"};

		int randomMessageindex = Random.Range (0, BadLeaningMessageList.Length);
		string message = BadLeaningMessageList [randomMessageindex].ToString ();

		return message;
	}


}
