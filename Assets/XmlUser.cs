using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;					//basic xml attributes
using System.Xml.Serialization;	
using UnityEngine.UI;
using System.IO;//access xmlserializer
using QRCodeReaderAndGenerator;

public class XmlUser : MonoBehaviour {




	public UserDataBase UserDB;
	string createUserURL = "193.136.221.122/therapy_game/InsertUser.php";

	public InputField Username;
	public InputField Password;
	public InputField Name;
	public InputField Age;
	public InputField Height;
	public InputField Weight;
	public Toggle Male;
	public Toggle Female;
	public InputField Comments;
	public Toggle Superior;
	public Toggle Inferior;

	private int userCounter = 1;
	private string[] directories;
	private static bool Exists = false;


	public RawImage image;
	UserEntry User = new UserEntry ();
	private string qrcodePath;


	void Start(){
		qrcodePath = Application.dataPath + "\\StreamingAssets\\UsersQR\\";

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
		FileStream file = new FileStream (qrcodePath + Username.text +".png", FileMode.Create);
		BinaryWriter b= new BinaryWriter (file);
		b.Write (bytes);
		file.Close ();
		
	}

	public void SaveUserData(){

			SaveUserToDatabase ();
			
	}
		

	public void SaveUserToDatabase(){
		WWWForm form = new WWWForm ();

		form.AddField ("usernamePost", Username.text);
		form.AddField ("passwordPost", Password.text);
		form.AddField ("namePost", Name.text);
		form.AddField ("agePost", Age.text);
		form.AddField ("heightPost", Height.text);
		form.AddField ("weightPost", Weight.text);
		if (Male.isOn) {
			form.AddField ("genderPost", "Male");
		}

		if (Female.isOn) {
			form.AddField ("genderPost", "Female");
		}

		if (Superior.isOn) {
			form.AddField ("rehab_typePost", "Superior");
		}
		if (Inferior.isOn) {
			form.AddField("rehab_typePost" , "Inferior");
			}
		if(Inferior.isOn && Superior.isOn){
			form.AddField("rehab_typePost" , "Both");
		}

		int phisioid = int.Parse(PlayerPrefs.GetString ("PhisioId"));

		form.AddField ("commentsPost", Comments.text);
			print ("IDAQUI:" + phisioid);
		form.AddField ("phisioIdPost", phisioid);
		form.AddField ("userTypePost", "Patient");


		WWW www = new WWW (createUserURL,form);

	}



}



[System.Serializable]
public class UserEntry{
	public int id;
	public string username;
	public string password;
	public string name;
	public string age;
	public string height;
	public string weight;
	public string gender;
	public string comments;
	public string rehabType;
}

[System.Serializable]
public class UserDataBase{

	public List<UserEntry> list = new List<UserEntry>();
}