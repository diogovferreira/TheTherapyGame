using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class SideFootMovesGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {

	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
	public GUIText gestureInfo;



	public static bool rightLegSideMove = false;
	public static bool leftLegSideMove = false;


	private DetectPos p;

	private static SideFootMovesGestureListener instance = null;
	// Use this for initialization

	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;




	void Start(){
		p = DetectPos.Instance;
	}


	public static SideFootMovesGestureListener Instance {
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
		//manager.DetectGesture (userId, KinectGestures.Gestures.SideLeftLegMove);
		manager.DetectGesture (userId, KinectGestures.Gestures.SideRighLegMove);


		if(gestureInfo != null)
		{
			gestureInfo.text = "Start the game";
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


	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos){


		if (userIndex != playerIndex)
			return;


		//Side Left Move/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.SideLeftLegMove) {

			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;
			print ("PROGRESSO->" + (progress * 100));
			if ((progress * 100) > 97.0 && leftLegSideMove == false) {
				leftLegSideMove = true;
			}
		}


		//Side Right Move/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.SideRighLegMove) {

			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;
			print ("PROGRESSO->" + (progress * 100));
			if ((progress * 100) > 97.0 && rightLegSideMove == false) {
				rightLegSideMove = true;
			}
		}
	}


	public bool leftsideMoveCompleted(){
		return leftLegSideMove;
	}

	public bool rightsideMoveCompleted(){
		return rightLegSideMove;
	}

	public void setInitialPosRSideMoved(bool s){
		rightLegSideMove = s;
	}

	public void setInitialPosLSideMoved(bool s){
		leftLegSideMove = s;
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
}
