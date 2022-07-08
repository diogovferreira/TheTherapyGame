using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObjects : MonoBehaviour {



	public GameObject[] objects;


	public float seconds;
	public int incoming;

	private float fireRate;
	private  float spawnRange;
	private CountDownScript cd;
	private LevelOptions level;
	private Rigidbody rb;
	private Transform trans;
	private KinectManager manager;
	private gameLogic g;
	public SetTime st;

	// Use this for initialization
	void Start () {
		cd = GameObject.Find ("GameController").GetComponent<CountDownScript> ();
		trans = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
		manager = KinectManager.Instance;

		spawnRange = st.getRange ();
		fireRate = st.getFireRate ();
	}


	void Awake(){
		

	}
	
	// Update is called once per frame
	void Update () {
		
		if (manager.IsUserDetected ()) {
			if (cd.countDowndone == true) {
				SpawnObjects (fireRate, incoming);
			}
		} else {
			//se o utilizador não for detectado ou sair da area de jogo(aparece mensagem de pause)
		}
	}

	void SpawnObjects(float f,int inc){

			//temporizador, adiciona o detlta para conversão em segundos
			seconds += Time.deltaTime;
			if (f == 0) {
				return;
			} else if (seconds > f) {
				float RandomPoint = Random.Range (-spawnRange, spawnRange);
				Vector3 position = new Vector3 (trans.position.x + RandomPoint, trans.position.y, trans.position.z);

			//Spawn an object thats within the list
					int randomObj = Random.Range (0, objects.Length);
					Instantiate (objects [randomObj], position, transform.rotation);
					incoming++;


				// zero out the seconds variable
				seconds = 0;
			}
		}



}
