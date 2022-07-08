using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class XMLGameManager : MonoBehaviour {

	public static XMLGameManager ins;
	private string pathStepHorizontal;
	private string pathInfinite;
	private string pathStepVertical; 
	private string pathLeaning;
	private string pathHitWall;


	private string pathHitMaps;
	private string pathHitMapsImages;

	public LoadSceneOnClick2 HorV;


	public Text Time;
	public Text Score;
	public Text ScoreObjective;
	public Text FireRate;
	public Text TilesSpeed;
	public Text StepsLeft;
	public Text StepsRight;
	public Text BadObjs;
	public Text GoodObjs;
	public Text Result;


	public Text PlayerSpeed;
	public Text SpawnRange;
	public Text Poison;
	public Text Star;
	public Text Bombs;
	public Text Wrench;


	public Text Lives;
	public Text MedicalKit;
	public Text Diamonds;



	public Text handUsed;


	public LoadGames lg;

	private string gameName;

	public static int gameCounter;


	public GamesDatabase gamesDB;
	public MapDatabase mapDB;

	private string mapName;
	private string mapImageName;

	void Awake(){
		ins = this;
	}


	void Start(){
		
		setPahts ();


	}

	public void setPahts(){

		pathHitMaps = Application.dataPath + "\\StreamingAssets\\HITMAPSc\\";
		pathHitMapsImages = Application.dataPath + "\\StreamingAssets\\HITMAPSc\\IMAGES\\";


	}
		


	public void SaveGame(){

		if (HorV.getHorizontal () == true) {
			

			List<string> filesInDirectory = new List<string> ();

			//SET THE NEW FILE NAME
			DirectoryInfo dir = new DirectoryInfo (pathStepHorizontal);
			FileInfo[] info = dir.GetFiles ("*.*");
			foreach (FileInfo f in info) {

				//subtring to remove the extension

				string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);

				foreach (string s in RealName) {
					if (s != "" || s != "GAMES_EVAL_DATA") {
						if (filesInDirectory.Contains (s)) {

						} else {
							filesInDirectory.Add (s);
						}
					}
				}
			}


			for (int j = 1; j <= filesInDirectory.Count; j++) {
				if (filesInDirectory.Contains ("game_" + j + ".xml")) {

				} else {
					gameName = "game_" + (j - 1) + ".xml";

				}
			}
			filesInDirectory.Clear ();

			//opens a new xml file
			XmlSerializer serializer = new XmlSerializer (typeof(GamesDatabase));
			StreamWriter stream = new StreamWriter (pathStepHorizontal + gameName, false, System.Text.Encoding.GetEncoding ("utf-8"));
			serializer.Serialize (stream, gamesDB);
			stream.Close ();
		}
//		} else {
//
//			List<string> filesInDirectory = new List<string> ();
//
//			//SET THE NEW FILE NAME
//			DirectoryInfo dir = new DirectoryInfo (pathStepVertical);
//			FileInfo[] info = dir.GetFiles ("*.*");
//			foreach (FileInfo f in info) {
//
//				//subtring to remove the extension
//
//				string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);
//
//				foreach (string s in RealName) {
//					if (s != "" || s != "GAMES_EVAL_DATA") {
//						if (filesInDirectory.Contains (s)) {
//
//						} else {
//							filesInDirectory.Add (s);
//						}}}}
//
//
//			for (int j = 1; j <= filesInDirectory.Count; j++) {
//				if (filesInDirectory.Contains ("game_" + j + ".xml")) {
//
//				} else {
//					gameName = "game_" + (j-1) + ".xml";
//
//				}
//			}
//			filesInDirectory.Clear ();
//
//
//			XmlSerializer serializer = new XmlSerializer (typeof(GamesDatabase));
//			StreamWriter stream = new StreamWriter (pathStepVertical + gameName ,false ,System.Text.Encoding.GetEncoding("utf-8"));
//			serializer.Serialize (stream, gamesDB);
//			stream.Close ();
//		}
		//Mudar a base de danos apenas para jogo em vez de DB
	
	}
		

	public void SaveHitMap(MapDatabase mapdb){

		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathHitMaps);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {
				if (s != "" || s != "IMAGES") {
					if (filesInDirectory.Contains (s)) {

					} else {
						filesInDirectory.Add (s);
					}}}}


		for (int j = 1; j <= filesInDirectory.Count; j++) {
			if (filesInDirectory.Contains ("map_" + j + ".xml")) {

			} else {
				mapName = "map_" + (j-1) + ".xml";
				mapImageName = "map_" + (j - 1) + "_image.png";
			}
		}
		filesInDirectory.Clear ();

		Application.CaptureScreenshot (Path.Combine (pathHitMapsImages, mapImageName));


		//opens a new xml file
		XmlSerializer serializer = new XmlSerializer (typeof(MapDatabase));
		StreamWriter stream = new StreamWriter ((Path.Combine(pathHitMaps,mapName)) ,false ,System.Text.Encoding.GetEncoding("utf-8"));
		serializer.Serialize (stream, mapdb);
		stream.Close ();


	}








}
[System.Serializable]//to be able to see this in the inspector
public class GameEntryData{
	
	public int gameNumber;
	public int gameTime;
	public int score;
	public int scoreObjective;
	public int fireRate;
	public int tileSpeed;
	public int stepsLeft;
	public int stepsRight;
	public int badObjs;
	public int goodObjs;
	public string gameResult;

	//InfiniteRunner
	public int poison;
	public int bombs;
	public int medicalKit;
	public int diamonds;
	public int playerSpeed;
	public int lives;

	//Leaning game
	public int stars;
	public int wrench;
	public int spawnRange;

	//Hit Wall
	public string handUsed;
	public int timeLeft;



}


[System.Serializable]//to be able to see this in the inspector
public class GamesDatabase{
	
	public List<GameEntryData> gameList = new List<GameEntryData>();
}


[System.Serializable]
public class MapEntryData{
	public List<Vector3> WallPos;
	public List<Quaternion> WallRot;
//	public Vector3 PlayerPos;
//	public Vector3 CheckPointPos;
}

[System.Serializable]
public class MapDatabase{
	public List<MapEntryData> mapList = new List<MapEntryData>();
}
