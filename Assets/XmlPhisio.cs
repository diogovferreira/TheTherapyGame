using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;					//basic xml attributes
using System.Xml.Serialization;	
using UnityEngine.UI;
using System.IO;//access xmlserializer
using QRCodeReaderAndGenerator;

public class XmlPhisio : MonoBehaviour {


	public PhisioDataBase PhisioDB;

	public InputField Username;
	public InputField Password;
	public InputField Name;
	public InputField ccN;

	public RawImage image;

	private string[] directories;

	private string InsertPhisioURL = "193.136.221.122/therapy_game/InsertPhisio.php";
	private string qrCodePath;





	void Start(){
		qrCodePath = Application.dataPath + "\\StreamingAssets\\TherapistsQR\\";
	}

	//Generate QRCode
	public void GenerateQrCode(){
		if (Username && image && Password) {
			image.texture= QRCodeManager.Instance.GenerateQRCode (Username.text +"."+ Password.text);

		}
	}


	public void GenerateImageQrCode(RawImage qr2D){
		Texture2D text = (Texture2D)qr2D.texture;
		byte[] bytes = text.EncodeToPNG ();
		FileStream file = new FileStream (qrCodePath + Username.text +  ".png" , FileMode.Create);
		BinaryWriter b= new BinaryWriter (file);
		b.Write (bytes);
		file.Close ();

	}


	public void SavePhisioDatabase(){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", Username.text);
		form.AddField ("passwordPost", Password.text);
		form.AddField ("namePost", Name.text);

		form.AddField ("userTypePost", "Therapist");
		form.AddField ("ccPost", ccN.text);


		WWW www = new WWW (InsertPhisioURL,form);

	}



	public void SavePhisioUserData(){


		///////////////////////User Directories/////////////////
		directories = Directory.GetDirectories (Application.dataPath + "\\PhisioXML\\");



		string pathToPhisioUsersXML = Application.dataPath + "\\PhisioXML\\phisio_user_";

		int lastindexDirectory = directories.Length;
	
		int newDirectoryIndex = lastindexDirectory + 1;

		if (lastindexDirectory.Equals (0)) {



			XmlSerializer serializer = new XmlSerializer (typeof(PhisioDataBase));
			FileStream stream = new FileStream (pathToPhisioUsersXML + "1.xml", FileMode.Create);
			serializer.Serialize (stream, PhisioDB);
			stream.Close ();


			//Generate qr code for user 1
			GenerateImageQrCode (image);

		} else {
			string lastDirectory = directories [lastindexDirectory -1].ToString ();
		


			////////////////////USER XML FILE////////////////////////////////////
			XmlSerializer serializer = new XmlSerializer (typeof(PhisioDataBase));
			StreamWriter stream = new StreamWriter (pathToPhisioUsersXML + newDirectoryIndex.ToString() + ".xml",false, System.Text.Encoding.GetEncoding ("utf-8"));
			serializer.Serialize (stream, PhisioDB);
			stream.Close ();


			//generate code for other users
			GenerateImageQrCode (image);


		}

	}

	public void populateDataBase(){
		PhisioEntry User = new PhisioEntry ();

		//		print ("InputFields");
		//		print ("Name: " + Name.text + " Age: " + Age.text + "   Height: " + Height.text + "  Weight: " + Weight.text + "   Comments: " + Comments.text);
		User.username = Username.text;
		User.password = Password.text;
		User.name = Name.text;
		User.ccNumber = ccN.text;


		PhisioDB.list.Add (User);
		SavePhisioUserData ();
		SavePhisioDatabase ();


	}







}



[System.Serializable]
public class PhisioEntry{
	public string username;
	public string password;
	public string name;
	public string ccNumber;
}

[System.Serializable]
public class PhisioDataBase{

	public List<PhisioEntry> list = new List<PhisioEntry>();
}
