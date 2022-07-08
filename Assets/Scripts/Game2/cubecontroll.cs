using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroll : MonoBehaviour {


	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	private float speed;
	public int numero;

	private long userId;
	private Vector3 lefthip;
	private Vector3 righthip;
	private KinectManager manager;
	private Rigidbody rb;
	private float moveHorizontal;
	private game2GestureListener gestureListener;
	public SetTime st;
	public Transform player;
	private float playerRange;


	// Use this for initialization
	void Start () {
		
		gestureListener = game2GestureListener.Instance;
		manager = KinectManager.Instance;



	}


	void Awake(){
		rb = GetComponent<Rigidbody> ();
		speed = st.getSpeed ();
		print ("Speed" + st.getSpeed().ToString());
	}

	//Falta criar movimento do player quando se inclina
	//Falta implementar boundaries 
	void Update () {

		if (!gestureListener) 
			return;

		if (manager == null) {
			manager = KinectManager.Instance;
		}

		if (manager.IsUserDetected (playerIndex)) {
		
			userId = manager.GetUserIdByIndex (playerIndex);
		}

		//Criar movimento na barra de inclinação.
		lefthip = manager.GetJointPosition (userId,manager.GetJointIndex (KinectInterop.JointType.HipLeft));
		righthip = manager.GetJointPosition (userId, manager.GetJointIndex (KinectInterop.JointType.HipRight));

		float angle = Mathf.Atan2 (righthip.y - lefthip.y, lefthip.x - righthip.x) * Mathf.Rad2Deg;

		moveHorizontal = Mathf.Lerp (1.0f, -1.0f, Mathf.InverseLerp (-45.0f, 45.0f, angle));

	
		Vector3 input = new Vector3 (moveHorizontal, 0.0f, 0.0f);

		if (speed != null) {
			rb.velocity = input * speed;

		} 
		else {
			speed = 3;//default speed
		}
			

	}
		

}
