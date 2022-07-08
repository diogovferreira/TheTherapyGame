using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnPlatformCollider : MonoBehaviour {

	public static bool inPlatform;
	private int count = 0;
	private float startTime;
	private static float movementTime;
	private static float distance;
	private static float velocity;
	private static float angle;
	private static string footUsed;
	public XMLManager xml;

	private string dataString;

	public tileCollider tillecollider;
	public ProcessData dataP;

	public GamesDatabase game;

	private static List<string> movements = new List<string>();
	private static int movementCounter;


	void OnTriggerExit(Collider c){
		if (c.gameObject.CompareTag ("startingArea")) {
			inPlatform = false;
			startTime = Time.time;
			print ("Sai da plataforma");
			//count to avoid the initial situation of alredy beeing on the platform
			count++;
			} 
	}


	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("startingArea") && count>0) {
			inPlatform = true;
			print ("Estou na plataforma");
			//data collected when the movement is completed(step on the tile and come to the platform)
			movementTime = Time.time - startTime;
			distance = (int)(tillecollider.getDistance ()*100);//meters to cm(*100)
			angle = (int)tillecollider.getAngle ();
			footUsed = tillecollider.FootUsedName ();
			velocity = (int)MediumVelocity (distance, movementTime);
			movementCounter = movementCounter + 1;
			string movementData = "MovementNumber: " + movementCounter + " ANGLE: " + angle + " DISTANCE :" + distance + " Movement Duration :" + movementTime + " Velocidade Media :" + velocity + " GAMEOBJECT NAME: " + footUsed; 
		
			MovementEntry item = new MovementEntry ();

			if (this.gameObject.tag.Equals ("Lfoot")) {
				footUsed = "Left";

			}
			if (this.gameObject.tag.Equals ("Rfoot")) {
				footUsed = "Right";
			}

			item.angle = (int)angle;
			item.movementNumber = movementCounter;
			item.distance = (int)distance;
			item.movementMediumVelocity = (int)velocity;
			item.movementDuration = movementTime;
			item.footUsed = footUsed;


			xml.MovementDB.list.Add (item);


			movements.Add (movementData);


		}
	}



	public float MediumVelocity(float distance, float movTime){

		float mediumvelocity = distance / movTime;
	
		return mediumvelocity;

	}



	public float getMovementTime (){
		return movementTime;
	}

	public bool onPlatform(){
		return inPlatform;
	}


	public List<string> getMovementsList(){
		return movements;
	}
}
