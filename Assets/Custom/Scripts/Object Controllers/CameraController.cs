using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//			Vector3
	// private
	private Vector3 direction;
	private Vector3 playerPostion;

	//			float
	// public
	public float defaultSpeed;

	// private 
	private float distance;
	private float speed;


	// Use this for initialization
	void Awake () {
		direction = new Vector3 (1, 0, 0);
		speed = defaultSpeed;
	}

	void Start(){
		UpdatePlayerDistance ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePlayerDistance ();
		UpdatePlayerPosition ();
	}


	void FindSpeed(){
		if (distance > 10.0f) {
			speed = defaultSpeed;
		}

		if (distance <= 10.0f && distance > 5.0f) {
			speed = defaultSpeed * (2 / 3);
		}

		if (distance <= 5.0f && distance > 0) {
			speed = defaultSpeed * (1 / 3);
		}


	}

	void UpdatePlayerDistance(){
		distance = Vector3.Distance (transform.position, PlayerController.instance.position);
	}

	void UpdatePlayerPosition(){
		//playerPostion = new Vector3 (PlayerController.instance.position.x, transform.position.y, transform.position.z);
		playerPostion = new Vector3 (transform.position.x + 10, transform.position.y, transform.position.z);
	}
}
