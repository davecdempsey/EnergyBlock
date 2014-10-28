using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//			Camera
	// public
	public Camera mainCamera;

	// 			GameController
	// public
	public static GameController instance;

	//			GameObject
	// public
	public GameObject gameScreenMiddle;

	//			Vector3
	// private
	private Vector3 middleGameScreenPosition;

	//			float
	// private
	private float energy;
	

	void Awake(){
		instance = this;

	}

	void OnEnable(){
		// setting gravity
		Physics2D.gravity = new Vector3(0, 0, 0);

		middleGameScreenPosition = mainCamera.WorldToScreenPoint(gameScreenMiddle.transform.position);
	}

	void Update(){
		energy = Global.instance.energyGathered / Global.instance.energyNeeded;
		CheckForJump ();
	}

	void CheckForJump(){

		// if the mouse was clicked
		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if (Input.mousePosition.x < middleGameScreenPosition.x){
				PlayerController.instance.Jump();
			}

			else{

				// if the player has enough energy to reverse gravity
				if(energy > 1){
					HudController.instance.ReverseGravity();

					// reset the energy gathered
					Global.instance.energyGathered = 0.0f;
				}

			}

		}

	}


	public void StartGame(){
		print ("START GAME");
		// setting gravity
		Physics2D.gravity = new Vector3(0, Global.instance.gravity, 0);
		Global.instance.currentState = Global.GameState.gamePlay;
	}


}
