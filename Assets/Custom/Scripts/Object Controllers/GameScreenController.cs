using UnityEngine;
using System.Collections;

public class GameScreenController : MonoBehaviour {

	//			float
	// public
	public float speed;

	//			Vector3
	// private
	private Vector3 direction;


	// Use this for initialization
	void Awake () {
		direction = new Vector3 (-1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.instance.currentState == Global.GameState.gamePlay) {
			//rigidbody2D.velocity = direction.normalized * speed;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Global.instance.currentState == Global.GameState.gamePlay) {
			//rigidbody2D.velocity = direction.normalized * speed;
		}
	}
}
