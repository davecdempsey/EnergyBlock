using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	// 			Global
	// public
	public static Global instance;

	//			GameObject
	// public
	public GameObject topContainer;
	public GameObject bottomContainer;
	public GameObject firstTop;
	public GameObject firstBottom;
	public GameObject gravityArrow;
	

	//			float
	// public
	public float gravity;
	
	public float smallWidth = 2f;
	public float medWidth = 3f;
	public float largeWidth = 5f;

	public float regularHeight = 1f;
	public float smallHeight = 2f;
	public float mediumHeight = 3f;
	public float largeHeight = 4f;

	public float gravityDirection;

	public float energyNeeded;
	public float energyGathered;

	// 			Vector3
	// public
	public Vector3 defatultScale;

	//			Color
	// public
	public Color defaultColor;
	public Color orange;

	public Color small;
	public Color medium;

	//			GameState
	// public
	public GameState currentState;
	public enum GameState{
		loading, main, gamePlay, pause, dead, tutorial
	}


	void Awake(){
		instance = this;

		defaultColor = new Color ((84f / 255f), (129f / 255f), (230f / 255f), 1f);
		orange = new Color ((247f / 255f), (108f / 255f), (60f / 255f), 1f);
			
		currentState = GameState.loading;

		defatultScale = new Vector3 (5, 1, 1);

		gravityDirection = 1f;

		energyGathered = 0.0f;
	
		energyNeeded = 100.0f;
	}
}
