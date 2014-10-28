using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	// 			PlayerController
	// public
	public static PlayerController instance;
	
	//			float
	// public
	public float jumpForce;
	public float defaultSpeed;

	// private 
	private float distance;
	private float speed;

	//			SpriteRenderer
	// public
	public SpriteRenderer spriteRenderer;

	//			Vector3
	// public
	public Vector3 position;

	//			Vector2
	// private
	private Vector2 jumpVector;
	private Vector2 tempLocalPosition;
	private Vector2 middlePosition;

	//			State
	// public
	public State currentState;

	public enum State{
		sameColor, differentColor, noColor
	}

	// 			GameObject
	// private
	private List<GameObject> sameColorObject;


	//			Color
	// private
	private Color currentColor;

	//			Script
	// private
	private EdgePiece tempEdgeScript;
	

	void Awake(){
		instance = this;

		currentColor = spriteRenderer.color;

		currentState = State.noColor;
	}

	void OnEnable(){
		Initialization ();
	}

	/// <summary>
	/// Initializes the player.
	/// </summary>
	void Initialization () {
		jumpVector = new Vector2 (0.0f, jumpForce);
		sameColorObject = new List<GameObject>();

		spriteRenderer.color = Global.instance.defaultColor;

		middlePosition = new Vector2 (transform.localPosition.x, 0.0f);

		tempLocalPosition = new Vector2 (transform.localPosition.x, 0.0f);
		
		distance = Vector2.Distance (transform.localPosition, middlePosition);

	}
	
	// Update is called once per frame
	void Update () {


		if (Global.instance.currentState == Global.GameState.gamePlay) {
			UpdateColor ();
			
			UpdatePosition ();
			
			UpdatePlayerDistance ();
			
			//UpdateJumpVector ();

			StateMachine();
		}

	}

	void StateMachine(){
		switch(currentState){
			case State.sameColor:
				
				Global.instance.energyGathered += 1f;
				break;
				
		}
	}

	void FixedUpdate(){
		if (Global.instance.currentState == Global.GameState.gamePlay) {
			if (currentState != State.noColor) {
				rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		if (collider != null && SpawnPoolController.instance.edgeScripts.ContainsKey(collider.transform.GetInstanceID())) {

			//SpriteRenderer collidingSpriteRenderer = collider.gameObject.GetComponent<SpriteRenderer>();

			//print ("collider.transform.name: " + collider.transform.name);

			//print ("collider.transform.GetInstanceID(): " + collider.transform.GetInstanceID());


			tempEdgeScript = SpawnPoolController.instance.edgeScripts[collider.transform.GetInstanceID()];


			//print ("edge color: " + tempEdgeScript.GetColor());

			//print ("currentColor: " + currentColor);

			if(tempEdgeScript.GetColor() == currentColor){

				// set the current state
				currentState = State.sameColor;

				Global.instance.energyGathered += 1f;

				// add the object to list of objects that the player is currently touching (if not already apart of the list)
				if(!sameColorObject.Contains(collider.gameObject)){
					sameColorObject.Add(collider.gameObject);

					//Global.instance.energyGathered += 10f;
				}
			}
			else{
				currentState = State.differentColor;
			}
		}
		
	}


	void OnCollisionExit2D(Collision2D collider){

		if(sameColorObject.Contains(collider.gameObject)){
			sameColorObject.Remove(collider.gameObject);
		}

		if (sameColorObject.Count < 1) {
			currentState = State.noColor;		
		}
	}
	
	void UpdateColor(){
		currentColor = spriteRenderer.color;
	}

	void UpdatePosition(){
		position = transform.position;
	}

	void UpdateJumpVector(){
		if (distance > 40.0f) {
			speed = defaultSpeed;
		}
		
		if (distance <= 40.0f && distance > 30.0f) {
			speed = defaultSpeed * (5F / 6F);

		}
		
		if (distance <= 30.0f && distance > 20.0f) {
			speed = defaultSpeed * (4F / 6F);

		}

		if (distance <= 20.0f && distance > 10.0f) {
			speed = defaultSpeed * (3F / 6F);

		}

		if (distance <= 10.0f && distance > 5.0f) {
			speed = defaultSpeed * (2F / 6F);

		}

		if (distance <= 5.0f) {
			speed = defaultSpeed * (1F / 6F);

		}

		if(distance > 0){
			jumpVector = new Vector2 (speed, jumpForce);
			//print ("DISTANCE: " + distance);
			//print ("JUMP VECTOR: " + jumpVector);
			//print ("SPEED: " + speed);

		}
		else{
			jumpVector = new Vector2 (0.0f, jumpForce);
		}

	}

	void UpdatePlayerDistance(){
		distance = transform.localPosition.x * -1f;

	}

	/// <summary>
	/// Jump this player.
	/// </summary>
	public void Jump(){
		// only jump if the player is touching a side
		if (currentState != State.noColor) {
			//print (distance);
			//print (jumpVector);
			rigidbody2D.AddForce(jumpVector * Global.instance.gravityDirection);
		}

	}

}
