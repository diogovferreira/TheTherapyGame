using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class game2GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {



	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
	public GUIText gestureInfo;

	// singleton instance of the class
	private static game2GestureListener instance = null;

	//if the gesture was detected or not
	private bool leanLeft;
	private bool leanRight;
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;
	public Text time;


	private static List<string> LeaningLeftList = new List <string> ();
	private static List<string> LeaningRightList = new List<string> ();
	private static Dictionary<float,float> AngTemp = new Dictionary<float,float> ();

	//Não estão a servir para nada
	private static List<int> angulos = new List<int> ();
	private static List<int> tempo = new List<int> ();
	private static List<string> side = new List<string> ();

	private Dictionary<string,float> dici = new Dictionary<string,float>();

	public gameLogic gL;
	public XMLManager xmlManager;


	/// <summary>
	/// T///////////////////////////	/// </summary>
	private Dictionary<string,float> dados;




	public static game2GestureListener Instance
	{
		get
		{
			return instance;
		}
	}
		

	public void UserDetected(long userId, int userIndex)
	{
		if (userIndex != playerIndex)
			return;

		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.LeanLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.LeanRight);
			


		if(gestureInfo != null)
		{
			gestureInfo.text = " Lean for each side to catch the balls";
		}
	}

	public void UserLost(long userId, int userIndex)
	{
		if (userIndex != playerIndex)
			return;

		if(gestureInfo != null)
		{
			gestureInfo.text = string.Empty;
		}
	}






	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
		float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		if (userIndex != playerIndex)
			return;

		if ((gesture == KinectGestures.Gestures.LeanLeft && progress > 0.5f)) {

			leanLeft = true;



			if (gestureInfo != null) {
				string sGestureText = string.Format ("{0} : {1:F0}", gesture, screenPos.z);
				gestureInfo.text = sGestureText;
				progressGestureTime = Time.realtimeSinceStartup;

			
				progressDisplayed = true;


				angulos.Add ((int)screenPos.z);
				tempo.Add (gL.getCurrentTime ());
				side.Add ("Left");
	

			} 
		
		}
		if ((gesture == KinectGestures.Gestures.LeanRight && progress > 0.5f)) {

			leanRight = true;
			if (gestureInfo != null) {
				string sGestureText = string.Format ("{0} - {1:F0} ", gesture, screenPos.z);
				gestureInfo.text = sGestureText;
				progressGestureTime = Time.realtimeSinceStartup;

				progressDisplayed = true;


					angulos.Add ((int)screenPos.z);
					tempo.Add (gL.getCurrentTime());
					side.Add ("Right");


			}
		} 
		
	
	}



	public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectInterop.JointType joint, Vector3 screenPos)
	{
		if (userIndex != playerIndex)
			return false;

		if(progressDisplayed)
			return true;

		string sGestureText = gesture + " detected";
		if(gestureInfo != null)
		{
			gestureInfo.text = sGestureText;
		}

		return true;
	}

	public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectInterop.JointType joint)
	{

		if (gesture == KinectGestures.Gestures.LeanLeft) {
			leanLeft = false;
		}

		if (userIndex != playerIndex)
			return false;

		if(progressDisplayed)
		{
			progressDisplayed = false;

			if(gestureInfo != null)
			{
				gestureInfo.text = String.Empty;
			}
		}

		return true;
	}

	void Awake()
	{
		instance = this;
	}


	public void Update()
	{
		if(progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
		{
			progressDisplayed = false;

			if(gestureInfo != null)
			{
				gestureInfo.text = String.Empty;
			}

			Debug.Log("Forced progress to end.");
		}
	}


	public bool getLeanLeft(){
		return leanLeft;
	}

	public bool getLeanRiht(){
		return leanRight;
	}

	public List<string> getLeaningLeftList(){
		return LeaningLeftList;
	}

	public List<string> getLeaningRightList(){
		return LeaningRightList;
	}

	public List<int> getAngulos(){
		return angulos;
	}

	public List<int> getTimeStamps(){
		return tempo;
	}

	public List<string> getLeaningSide(){
		return side;
	}

	public Dictionary<float,float> angulosTempos(){
		return AngTemp;
	}

	public void saveList(){
		
	}
}
