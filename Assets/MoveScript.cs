using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	public Rigidbody player;

	public float speed;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody> ();
	}


	void move(){
		player.AddForce ((player.transform.forward));

		player.velocity = new Vector3 (0, 0, speed);
	}
	// Update is called once per frame
	void Update () {
		move ();
	}
}
