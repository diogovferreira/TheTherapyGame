using System.Collections;
using System.Collections.Generic; // lets us use lists
using System.Xml;					//basic xml attributes
using System.Xml.Serialization;		//access xmlserializer
using System.IO;					//file management
using UnityEngine;	
using UnityEngine.UI;

public class XMLManager : MonoBehaviour {


	public static XMLManager ins;

//	public static string pathStepHorizontal = "C:\\Users\\Diogo\\Desktop\\GAMESSTEPHORIZONTAL\\GAMESEVALDATA\\";
	private string pathStepHorizontal;
	private string pathStepVertical;
	private string pathInfinite;
	private string pathLeaning;
	private string pathHit;
	public static int counter = 1;


	private string gameName;
	private string gameNameScreenshoot;

	public game2GestureListener gestureLeaningListener;
	public LeaningDataEntry l;



	void Awake(){
		ins = this;



	}

	void Start(){
		pathStepHorizontal = Application.dataPath + "\\Users\\" + PlayerPrefs.GetString ("User") + "\\STEP_HORIZONTAL_GAME_DATA\\GAMES_EVAL_DATA\\";
		pathStepVertical = Application.dataPath + "\\Users\\" + PlayerPrefs.GetString ("User") + "\\STEP_VERTICAL_GAME_DATA\\GAMES_EVAL_DATA\\";
		pathInfinite = Application.dataPath + "\\Users\\" + PlayerPrefs.GetString ("User") + "\\INFINITE_RUNNER_GAME_DATA\\GAMES_EVAL_DATA\\";
		pathLeaning = Application.dataPath + "\\Users\\" + PlayerPrefs.GetString ("User") + "\\LEANING_GAME_DATA\\GAMES_EVAL_DATA\\";
		pathHit = Application.dataPath + "\\Users\\" + PlayerPrefs.GetString ("User") + "\\HIT_WALL_GAME_DATA\\GAMES_EVAL_DATA\\";
		//
	}

	//list of movements
	public MovementDatabase MovementDB;
	public ObjectDatabase ObjectDB;
	public LeaningDatabase LeaningDB;
	public HitWallDatabase HitDB;



	public void SaveItems(){

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
					}}}}

		print ("TAMANHO: " + filesInDirectory.Count);

		if (filesInDirectory.Count.Equals (0)) {
			//opens a new xml file
			XmlSerializer serializer = new XmlSerializer (typeof(MovementDatabase));
			StreamWriter stream = new StreamWriter (pathStepHorizontal + "game_1_movementsh.xml", false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, MovementDB);
			stream.Close ();
		} else {
			for (int j = 1; j <= filesInDirectory.Count; j++) {
				if (filesInDirectory.Contains ("game_" + j + "_movementsh.xml")) {

				} else {
					gameName = "game_" + j + "_Movementsh.xml";

				}
			}
			filesInDirectory.Clear ();


			//opens a new xml file
			XmlSerializer serializer = new XmlSerializer (typeof(MovementDatabase));
			StreamWriter stream = new StreamWriter (pathStepHorizontal + gameName, false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, MovementDB);
			stream.Close ();
		}



	}


	public void SaveMovementsVertical(){


		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathStepVertical);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {
				if (s != "" || s != "GAMES_EVAL_DATA") {
					if (filesInDirectory.Contains (s)) {

					} else {
						filesInDirectory.Add (s);
					}}}}

		print ("TAMANHO: " + filesInDirectory.Count);

		if (filesInDirectory.Count.Equals (0)) {
			XmlSerializer serializer = new XmlSerializer (typeof(MovementDatabase));
			StreamWriter stream = new StreamWriter (pathStepVertical + "game_1_movementsv.xml", false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, MovementDB);
			stream.Close ();
		} else {
			for (int j = 1; j <= filesInDirectory.Count; j++) {
				if (filesInDirectory.Contains ("game_" + j + "_movementsv.xml")) {

				} else {
					gameName = "game_" + j + "_movementsv.xml";

				}
			}
			filesInDirectory.Clear ();
			//opens a new xml file
			XmlSerializer serializer = new XmlSerializer (typeof(MovementDatabase));
			StreamWriter stream = new StreamWriter (pathStepVertical + gameName, false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, MovementDB);
			stream.Close ();
		}



	}


	public void SaveItemsInfinite(){

		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathInfinite);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {
				if (s != "" || s != "GAMES_EVAL_DATA") {
					if (filesInDirectory.Contains (s)) {

					} else {
						filesInDirectory.Add (s);
					}}}}

		print ("TAMANHO: " + filesInDirectory.Count);

		if (filesInDirectory.Count.Equals (0)) {
			XmlSerializer serializer = new XmlSerializer (typeof(ObjectDatabase));
			StreamWriter stream = new StreamWriter (pathInfinite + "game_1_objs_caught.xml", false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, ObjectDB);
			stream.Close ();

		} else {
			for (int j = 1; j <= filesInDirectory.Count; j++) {
				if (filesInDirectory.Contains ("game_" + j + "_objs_caught.xml")) {

				} else {
					gameName = "game_" + j + "_objs_caught.xml";

				}
			}
			filesInDirectory.Clear ();

			XmlSerializer serializer = new XmlSerializer (typeof(ObjectDatabase));
			StreamWriter stream = new StreamWriter (pathInfinite + gameName, false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, ObjectDB);
			stream.Close ();
		}



	}

	public void SaveLeaningData(){

		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathLeaning);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {
				if (s != "" || s != "GAMES_EVAL_DATA") {
					if (filesInDirectory.Contains (s)) {

					} else {
						filesInDirectory.Add (s);
					}}}}
						
		print ("TAMANHO: " + filesInDirectory.Count);

		if (filesInDirectory.Count.Equals (0)) {
			XmlSerializer serializer = new XmlSerializer (typeof(LeaningDatabase));
			StreamWriter stream = new StreamWriter (pathLeaning +"game_1_leaningdata.xml", false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, LeaningDB);
			stream.Close ();
		}else{
			for (int j = 1; j <= filesInDirectory.Count; j++) {
				if (filesInDirectory.Contains ("game_" + j + "_leaningdata.xml")) {

				} else {
					gameName = "game_" + j + "_leaningdata.xml";

				}
			}
			filesInDirectory.Clear ();


			/////////////////////////////////Create new Game File//////////////////////////////

			XmlSerializer serializer = new XmlSerializer (typeof(LeaningDatabase));
			StreamWriter stream = new StreamWriter (pathLeaning + gameName, false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, LeaningDB);
			stream.Close ();

		}


	}

	public void SaveHitWallData(){


		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathHit);
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

		print ("TAMANHO: " + filesInDirectory.Count);

		if (filesInDirectory.Count.Equals (0)) {
			XmlSerializer serializer = new XmlSerializer (typeof(HitWallDatabase));
			StreamWriter stream = new StreamWriter (pathHit +"game_1_hitwalldata.xml", false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, HitDB);
			stream.Close ();

			Application.CaptureScreenshot (pathHit +"\\IMAGES\\"+ "game_1_hitwall_image.png");
		}else{
			for (int j = 1; j <= filesInDirectory.Count -1; j++) {
				if (filesInDirectory.Contains ("game_" + j + "_hitwalldata.xml")) {

				} else {
					gameName = "game_" + j + "_hitwalldata.xml";
					gameNameScreenshoot = "game_" + j + "_hitwall_image.png";
				}
			}
			filesInDirectory.Clear ();


			/////////////////////////////////Create new Game File//////////////////////////////

			XmlSerializer serializer = new XmlSerializer (typeof(HitWallDatabase));
			StreamWriter stream = new StreamWriter (pathHit + gameName, false ,System.Text.Encoding.GetEncoding("utf-8"));
			serializer.Serialize (stream, HitDB);
			stream.Close ();

			Application.CaptureScreenshot (pathHit +"\\IMAGES\\" + gameNameScreenshoot );

		}

	}





}
[System.Serializable]//to be able to see this in the inspector
public class MovementEntry {
	public int movementNumber;
	public int angle;//degrees
	public float movementDuration;//seconds
	public int movementMediumVelocity; //centimeters/seconds
	public int distance;//centimeters
	public string footUsed;

	}

[System.Serializable]
public class InfObjectEntry{
	public int objectNumber;
	public int angle;
	public string handUsed;

}

[System.Serializable]
public class LeaningDataEntry{
	public int leaningAngle;
	public int leaningTime;
	public string leaningSide;


}


[System.Serializable]
public class HitWallDataEntry{
	public float mediumVel;
	public float distance;
	public int time;

}


[System.Serializable]
public class MovementDatabase{

	public List<MovementEntry> list = new List<MovementEntry>();

}


[System.Serializable]
public class ObjectDatabase{

	public List<InfObjectEntry> list = new List<InfObjectEntry>();

}

[System.Serializable]
public class LeaningDatabase{

	public List<LeaningDataEntry> list = new List<LeaningDataEntry>();

}

[System.Serializable]
public class HitWallDatabase{

	public List<HitWallDataEntry> list = new List<HitWallDataEntry>();

}



