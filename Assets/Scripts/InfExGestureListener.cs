using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class InfExGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {



	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
	public GUIText gestureInfo;


	public ProcessData pd;
	public static bool isSit = false;
	public static bool rightLegAdvanced = false;
	public static bool leftLegAdvanced = false;

	//To detect the size of the pace
	public Transform lankle;
	public Transform rankle;


	private DetectPos p;

	private static InfExGestureListener instance = null;
	KinectManager manager;
	// Use this for initialization

	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;




	void Start(){
		p = DetectPos.Instance;
		manager = KinectManager.Instance;
	}


	public static InfExGestureListener Instance {
		get {
			return instance;
		}
	}


	public void UserDetected(long userId, int userIndex)
	{
		if (userIndex != playerIndex)
			return;

		// as an example - detect these user specific gestures


		//gestures to detect
//		manager.DetectGesture (userId, KinectGestures.Gestures.Sit);
		manager.DetectGesture (userId, KinectGestures.Gestures.AdvanceLeftLeg);
		manager.DetectGesture (userId, KinectGestures.Gestures.AdvanceRightLeg);
//		manager.DetectGesture (userId, KinectGestures.Gestures.SideLeftLegMove);


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


		}
			
		//ADVANCE LEFT LEG/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.AdvanceLeftLeg) {

			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;

		

//			if ((progress * 100) > 98.0 && leftLegAdvanced == false) {
//				leftLegAdvanced = true;
//				print (gesture.ToString() + "  DIstancia do passo: " + pd.calculateDistance ());
//			}
		} 

		//ADVANCE RIGHT LEG/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.AdvanceRightLeg) {

			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;




//			if ((progress * 100) >98.0 && rightLegAdvanced == false) {
//				rightLegAdvanced = true;
//				print (gesture.ToString() + "  DIstancia do passo: " + pd.calculateDistance ());
//			}
		} 
			
			
		//SIT/////////////////////////////////////////////////////////////7
		if (gesture == KinectGestures.Gestures.Sit) {
			string sGestureText = string.Format ("{0} - {1:F0} %", gesture, progress * 100);
			gestureInfo.text = sGestureText;
			print ("PROGRESSO--->" + progress * 100);
			if ((progress * 100) >97.0 && isSit == false) {
				isSit = true;
			}
		
		} 
			
	}




	public bool righLefAdvancedCompleteded(){
		return rightLegAdvanced;
	}

	public bool leftLefAdvancedCompleteded(){
		return leftLegAdvanced;
	}

	public void setInitialPosRAdvanced(bool s){
		rightLegAdvanced = s;
	}

	public void setInitialPosLAdvanced(bool s){
		leftLegAdvanced = s;
	}
		

	public bool isSited(){
		return isSit;
	}

	public void SetInitialSit(bool s){
		isSit = s;
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
