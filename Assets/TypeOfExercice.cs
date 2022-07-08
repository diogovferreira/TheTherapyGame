using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeOfExercice : MonoBehaviour {

	public Toggle LeftLeg;
	public Toggle RightLeg;
	public Toggle Front;
	public Toggle Back;

	private static bool LL = false;
	private static bool RL = false;
	private static bool front = false;
	private static bool back = false;




	//SOLUCAO TEMPORARIA
	public void chooseLeft(){
		if (LeftLeg.isOn) {
			LL = true;
		} else {
			
			LL = false;
		}
	}


	//SOLUCAO TEMPORARIA
	public void chooseRight(){
		if (RightLeg.isOn) {
			RL = true;
		} else{
			RL = false;
		}
	}
		

	public void ExerciceChoose(){
		if (LeftLeg.isOn = true & RightLeg.isOn == false) {
			LL = true;
			RL = false;
		} 
		else if (RightLeg.isOn = true & LeftLeg.isOn == false) {
			LL = false;
			RL = true;
		} 
		else if (RightLeg.isOn = true & LeftLeg.isOn == true) {
			LL = true;
			RL = true;
		} 
		else if (RightLeg.isOn = false & LeftLeg.isOn == false) {

			//Lançar mensagem para escolher uma perna senão não deixa prosseguir
			LL = false;
			RL = false;
		} 




	}


	public bool getLeft(){
		return LL;
	}

	public bool getRight(){
		return RL;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
