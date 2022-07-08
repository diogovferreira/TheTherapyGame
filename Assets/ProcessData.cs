using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ProcessData : MonoBehaviour {

	private float distance;
	private float angle;
	private float mediumVelocity;

	public Transform RFoot;
	public Transform LFoot;
	public Transform Pelvis;

	string path = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\Movements.txt";
	string pathGameInfo = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\GameInfo.txt";

	public LoadSceneOnClick2 getDirection;




	// Use this for initialization
	void Start () {
		
	}
		
	public void writeFile(List<string> movimentos, string gameInfo){

	
		print ("HORIZONTAL: " + getDirection.getHorizontal() + " VERTICAL: " + getDirection.getVertical());

		if (getDirection.getHorizontal () == true) {

			string path = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\Horizontal\\Movements.txt";
			string pathGameInfo = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\Horizontal\\GameInfo.txt";


			foreach (string a in movimentos) {
				print (a.ToString ());
			}
			File.WriteAllText (pathGameInfo, gameInfo);

			File.WriteAllLines (path, movimentos.ToArray());
			

		} else if (getDirection.getVertical () == true) {
			string path = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\Vertical\\Movements.txt";
			string pathGameInfo = "C:\\Users\\Diogo\\Desktop\\StepTheTileHorizontal\\Vertical\\GameInfo.txt";


			foreach (string a in movimentos) {
				print (a.ToString ());
			}
			File.WriteAllText (pathGameInfo, gameInfo);

			File.WriteAllLines (path, movimentos.ToArray());
		}
			
	}



//	public float calculateDistance(){
//		distance = Vector3.Distance (RFoot.position, LFoot.position);
//
//		return distance;
//	}
//
//
//	public float calculateAngle(){
//		Vector3 leftFootPel = LFoot.position - Pelvis.position;
//		Vector3 rightFootPel = RFoot.position - Pelvis.position;
//
//		angle = Vector3.Angle (leftFootPel, rightFootPel);
//
//		return angle;
//
//
//	}
//
//
//	public float mediumVel(){
//	
//		return mediumVelocity;
//	}
		
}
