using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlInfRunner : MonoBehaviour {

	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	private float forwardspeed;
	public SetTime gameVariables;


	private Transform tPlayer;
	private long userId;
	private Rigidbody rb;
	private float moveHorizontal = 0;
	private SimpleGestureListener gestureListener;
	private KinectManager km;

	public GameObject gameOverCanvas;
	public GameObject winCanvas;
	public GameObject pauseCanvas;




	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		km = KinectManager.Instance;
		gestureListener = SimpleGestureListener.Instance;
		tPlayer = GetComponent<Transform> ();

		forwardspeed = gameVariables.getSpeed ();


	}
		

	void Update () {
		if (km.IsUserDetected ()) {
			Time.timeScale = 1;
			if (forwardspeed <= 0) {
				forwardspeed = 2;
			}

			rb.velocity = new Vector3 (moveHorizontal, 0, forwardspeed);


			if (gestureListener.isLeanleft ()) {
				moveHorizontal = -1;
				StartCoroutine (Stop ());

			} else if (gestureListener.isLeanRight ()) {
				moveHorizontal = 1;
				StartCoroutine (Stop ());

			} else {
				moveHorizontal = 0;
			}
			
		} else if(gameOverCanvas.activeInHierarchy || winCanvas.activeInHierarchy || pauseCanvas.activeInHierarchy) {
			Time.timeScale = 0;
		}
	}


	IEnumerator Stop(){
			yield return new WaitForSeconds(.5f);
	
		
	}
}
