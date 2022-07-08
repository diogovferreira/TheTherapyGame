using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class variablesCollected : MonoBehaviour {


	//premade Levels
	List<string> levels = new List<string>() {"NoLevel" ,"Level 1", "Level 2", "Level 3","Level 4", "Level 5","CustomLevel"};
	// Use this for initialization
	
	public Dropdown dropdown;
	public Text selectLevel;
	private static string gameLevelName;


	List<string> MapList = new List<string> ();
	List<string> customMapList = new List<string> ();


	public Dropdown customDropdown;
	public RawImage MapImage;
	private Texture2D thisTex;

	private string pathHitMaps;
	private string pathHitMapsImages;

	private string pathHitMapsCustom;
	private string pathHitMapsImagesCustom;

	public MapDatabase mapDB;



	void Start () {
		
		pathHitMaps = Application.dataPath + "\\StreamingAssets\\HITMAPS\\";
		pathHitMapsImages = Application.dataPath + "\\StreamingAssets\\HITMAPS\\IMAGES\\";


		pathHitMapsCustom = Application.dataPath + "\\StreamingAssets\\HITMAPSc\\";
		pathHitMapsImagesCustom = Application.dataPath + "\\StreamingAssets\\HITMAPSc\\IMAGES\\";


		PopulateList ();
		populateCustomList ();

		thisTex = new Texture2D (100, 100);
	}


	public void Dropdown_Index(int index){

		//Se o toogle 1 for ativado o game name fica recolhido na player prefs(Jogos predefinidos)
		if(PlayerPrefs.GetString("List").Equals("1")){
			print ("Escolhi lista 1");
			gameLevelName = dropdown.options [dropdown.value].text;
			PlayerPrefs.SetString ("LevelName", gameLevelName);

		}

		if(PlayerPrefs.GetString("List").Equals("2")){
			print ("Escolhi lista 2");
			gameLevelName = customDropdown.options [customDropdown.value].text;
			PlayerPrefs.SetString ("LevelName", gameLevelName);

		}


	}




	void PopulateList(){
		DirectoryInfo dir = new DirectoryInfo (pathHitMaps);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {
			//subtring to remove the extension
			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);
			foreach (string s in RealName) {
				if (s != "" && s != "IMAGES") {
					if (MapList.Contains (s)) {
						
					} else {
						MapList.Add (s);
					}
				}}}
		//now its still missing an exception, not to add with the extension xml.meta
		dropdown.AddOptions (MapList);
		MapList.Clear ();


	}


	private void populateCustomList(){

		//NAME OF THE LEVEL
		print (dropdown.options[dropdown.value].text);

		DirectoryInfo dir = new DirectoryInfo (pathHitMapsCustom);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {
			print ("E aqui");
			//subtring to remove the extension
			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta" }, System.StringSplitOptions.None);
			foreach (string s in RealName) {
				if (s != "" && s != "IMAGES") {
					if (customMapList.Contains (s)) {
						
					} else {
						customMapList.Add (s);
					}
				}}}
		//now its still missing an exception, not to add with the extension xml.meta
		customDropdown.AddOptions (customMapList);
		customMapList.Clear ();
	}







	void Update(){
		//se mapa selecionada display map image
		displayCustomMap();
	}
		


	void getMapImage(string mapName){
		byte[] fileData;

		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathHitMapsImages);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta", ".png", "_image" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {

				if (mapName.Equals (s)) {

					fileData = File.ReadAllBytes (pathHitMapsImages + s + ".png");
					thisTex.LoadImage (fileData);
					MapImage.texture = thisTex;

				}
					
			}
		}
	}


	void getMapImagec(string mapName){
		byte[] fileData;

		List<string> filesInDirectory = new List<string> ();

		//SET THE NEW FILE NAME
		DirectoryInfo dir = new DirectoryInfo (pathHitMapsImagesCustom);
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {

			//subtring to remove the extension

			string[] RealName = f.Name.Split (new string[] { ".xml", ".meta", ".png", "_image" }, System.StringSplitOptions.None);

			foreach (string s in RealName) {

				if (mapName.Equals (s)) {

					fileData = File.ReadAllBytes (pathHitMapsImagesCustom + s + "_image.png");
					thisTex.LoadImage (fileData);
					MapImage.texture = thisTex;

				}

			}
		}
	}




	private void displayCustomMap(){
		string List = PlayerPrefs.GetString ("List");


		//Escolhe a lista, set the mapName to load the xml file related and set the image to the rawImage
		if (List.Equals ("1")) {
			if (dropdown.options [dropdown.value].text.Equals ("Level1")) {
				PlayerPrefs.SetString ("MapName", dropdown.options [dropdown.value].text);
				getMapImage (dropdown.options [dropdown.value].text);
			}
			if (dropdown.options [dropdown.value].text.Equals ("Level2")) {
				PlayerPrefs.SetString ("MapName", dropdown.options [dropdown.value].text);
				getMapImage (dropdown.options [dropdown.value].text);
			}
			if (dropdown.options [dropdown.value].text.Equals ("Level3")) {
				PlayerPrefs.SetString ("MapName", dropdown.options [dropdown.value].text);
				getMapImage (dropdown.options [dropdown.value].text);
			}
			if (dropdown.options [dropdown.value].text.Equals ("Level4")) {
				PlayerPrefs.SetString ("MapName", dropdown.options [dropdown.value].text);
				getMapImage (dropdown.options [dropdown.value].text);
			}
			if (dropdown.options [dropdown.value].text.Equals ("Level5")) {
				PlayerPrefs.SetString ("MapName", dropdown.options [dropdown.value].text);
				getMapImage (dropdown.options [dropdown.value].text);
			}
		}





		else if(List.Equals("2")) {
			PlayerPrefs.SetString ("MapName", customDropdown.options [customDropdown.value].text);

			getMapImagec (customDropdown.options [customDropdown.value].text);
		}
	}


	public string getLevelName(){
		return gameLevelName;
	}


	}



		

