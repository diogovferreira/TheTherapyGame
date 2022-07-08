using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProcessDataInfRunner : MonoBehaviour {



	private static int gameNumber;



	public void writeFile(string[] data){
		string path = "C:\\Users\\Diogo\\Desktop\\InfRunner\\GAME" + gameNumber + ".txt";
		print ("CHEGO AQUI");


	
			gameNumber = gameNumber + 1;
			File.WriteAllLines (path, data);
	



	}




	

}
