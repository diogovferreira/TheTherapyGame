using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour {


	public float throwWith;
	private float seconds;
	public int startWait;
	public float spawnWait;
	public bool stop;
	public GameObject [] missiles;

	public float speed;

	private Rigidbody rb;
	private KinectManager k;
	private Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform> ();
		k = KinectManager.Instance;

			StartCoroutine (waitSpwaner ());
		
	}


	// Update is called once per frame
	void Update () {
		
		}

	IEnumerator waitSpwaner (){
		yield return new WaitForSeconds (startWait);

		//se o user for detectado(falta implementar) começar a spamar
			while (!stop) {

				//With de que o missile posa ser atirado
				float RandomPoint = Random.Range (-throwWith, throwWith);
				Vector3 position = new Vector3 (t.position.x + RandomPoint, t.position.y, t.position.z);

				int randomObj = Random.Range (0, missiles.Length);
				GameObject bullet = Instantiate (missiles [randomObj], position, t.rotation) as GameObject;

				//Obter o rigidbody do missile/bala
				Rigidbody temp = bullet.GetComponent<Rigidbody> ();
				//set the shoot direction
				temp.AddForce (bullet.transform.forward);
				//setting the shoot velocity
				temp.velocity = new Vector3 (0, 0, -speed);

				yield return new WaitForSeconds (2);

				Destroy (bullet, 2f);


			}
		

	}




 void throwObjects(){
		seconds += Time.deltaTime;




			
			







	}
		
	}



