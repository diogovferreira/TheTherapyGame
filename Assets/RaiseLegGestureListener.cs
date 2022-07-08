using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;


public class RaiseLegGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {



	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 1;

	[Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
	public GUIText gestureInfo;



	public static bool leftLegRaised = false;
	public static bool rightLegRaised = false;


	private DetectPos p;
	private static RaiseLegGestureListener instance = null;
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;


	void Start () {
		p = DetectPos.Instance;
		print ("Entro aqui!");
	}

	public static RaiseLegGestureListener Instance {
		get {
			return instance;
		}
	}

	public void UserDetected(long userId, int userIndex)
	{
		if (userIndex != playerIndex)
			return;

		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		//gestures to detect
		manager.DetectGesture (userId, KinectGestures.Gestures.RaiseLeftLeg);
		manager.DetectGesture (userId, KinectGestures.Gestures.RaiseRightLeg);
	


		if(gestureInfo != null)
		{
			gestureInfo.text = "Start the game";
		}
	}


	//USER LOST
	public void UserLost(long userId, int userIndex)
	{
		if (userIndex != playerIndex)
			return;

		if(gestureInfo != null)
		{
			gestureInfo.text = string.Empty;
		}
	}


	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos){


		if (userIndex != playerIndex)
			return;

		//RAISE RIGHT LEG/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.RaiseRightLeg) {

			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;

			if ((progress * 100) >97.0 && rightLegRaised == false) {
				rightLegRaised = true;
			}

		}

		//RAISE LEFT LEG/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.RaiseLeftLeg) {


			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;

			if ((progress * 100) >97.0 && leftLegRaised == false) {
				leftLegRaised = true;
			}
		}

	}


	public bool righLegRaisedCompleted(){
		return rightLegRaised;
	}

	public bool leftLegRaisedCompleted(){
		return leftLegRaised;
	}

	public void setInitialPosRraised(bool s){
		rightLegRaised = s;
	}

	public void setInitialPosLraised(bool s){
		leftLegRaised = s;
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



	public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint){

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
		if(progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 3f))
		{
			progressDisplayed = false;

			if(gestureInfo != null)
			{
				gestureInfo.text = String.Empty;
			}

			Debug.Log("Forced progress to end.");
		}
	}

}
