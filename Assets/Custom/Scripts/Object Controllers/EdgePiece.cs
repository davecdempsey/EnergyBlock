using UnityEngine;
using System.Collections;

public class EdgePiece : MonoBehaviour {

	//			GameObject
	// public
	public GameObject rightPiece;
	public GameObject container;


	//			Vector3
	// public
	public Vector3 position;
	public Vector3 currentScale;

	// private
	private Vector3 direction;
	private Vector3 screenPosition;

	//			Vector2
	// public
	public Vector2 worldScale;

	//			float
	// public
	public float speed;

	//			int
	// public
	public int id;
	public int height;
	public int width;

	//			SpriteRenderer
	// public
	public SpriteRenderer spriteRenderer;

	//			Place
	// public
	public Place currentPlace;
	public enum Place{
		first, other
	}

	//			Width
	// public
	public Width currentWidth;

	public enum Width{
		small, medium, large
	}

	//			Height
	// public
	public Height currentHeight;
	
	public enum Height{
		regular, small, medium, large
	}

	//			Container
	// public
	public Container currentContainer;
	
	public enum Container{
		top, bottom
	}

	void Awake(){
		id = transform.GetInstanceID ();

		worldScale = new Vector2 (spriteRenderer.renderer.bounds.size.x, spriteRenderer.renderer.bounds.size.y);

	}
	
	void OnEnable () {
		Initialization ();
	}
	void OnDisable(){
		Reset();
	}

	/// <summary>
	/// Initializes the player.
	/// </summary>
	void Initialization () {
		spriteRenderer.color = Global.instance.defaultColor;
		UpdateScale ();
		direction = new Vector3 (-1, 0, 0);
		UpdateWorldScale();

	}

	void Reset(){
		Initialization ();
		rightPiece = null;
		currentWidth = Width.medium;
		currentHeight = Height.regular;
		this.transform.localScale = Global.instance.defatultScale;
		UpdateScale ();
	}


	// Update is called once per frame
	void Update () {

		// setting the position of the object
		position = transform.position;

		// check the edge piece to see if it off screen
		CheckEdge ();

		// checks the color of the piece to make sure it is the right color
		CheckColor ();

		CheckContainer ();

		CheckAlpha ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (currentPlace != Place.first) {
			if (Global.instance.currentState == Global.GameState.gamePlay) {
				rigidbody2D.velocity = direction.normalized * speed;
			}
		}

	}

	public void UpdateWorldScale(){
		worldScale = new Vector2 (spriteRenderer.renderer.bounds.size.x, spriteRenderer.renderer.bounds.size.y);
	}

	public Color GetColor(){
		return spriteRenderer.color;
	}

	void CheckContainer(){
		if (container == Global.instance.topContainer) {
			currentContainer = Container.top;
		}
		else{
			currentContainer = Container.bottom;
		}
	}

	/// <summary>
	/// Checks the edge to see if it is off screen.
	/// </summary>
	void CheckEdge(){

		if (currentPlace != Place.first) {
			// getting the screen position of the game object
			screenPosition = Camera.main.WorldToScreenPoint (transform.position);
			
			// if the gameobject is FULLY off screen
			if (screenPosition.x < -100.0f ) {

				if(currentContainer == Container.bottom){
					ProceduralGeneration.instance.RemoveBottomPiece(id, height, width);
				}
				else{
					ProceduralGeneration.instance.RemoveTopPiece(id, height, width);
				}
				
				// remove from the lists
				GameEngine.instance.RemoveFromLists(gameObject, this);
			}	
		}

	}

	void CheckAlpha(){

		if (Global.instance.gravityDirection == 1f) {

			if(currentContainer == Container.bottom){
				if(spriteRenderer.color.a != 1f){
					spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
				}
			}
			else{
				if(spriteRenderer.color.a != 0.5f){
					spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
				}
			}
		}

		if (Global.instance.gravityDirection == -1f) {

			if(currentContainer == Container.bottom){
				if(spriteRenderer.color.a != 0.5f){
					spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
				}
			}
			else{
				if(spriteRenderer.color.a != 1f){
					spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
				}
			}
		}
	}

	public void ChangeHeight(int newHeight){
		height = newHeight + 1;

		switch (newHeight) {
			case 0:
				transform.localScale = new Vector3(currentScale.x, Global.instance.regularHeight, currentScale.z);
				currentHeight = Height.regular;
				break;

			case 1:
				transform.localScale = new Vector3(currentScale.x, Global.instance.smallHeight, currentScale.z);
				currentHeight = Height.small;
				break;

			case 2:
				transform.localScale = new Vector3(currentScale.x, Global.instance.mediumHeight, currentScale.z);
				currentHeight = Height.medium;
				break;

			case 3:
				transform.localScale = new Vector3(currentScale.x, Global.instance.largeHeight, currentScale.z);
				currentHeight = Height.large;
				break;
		}

		if (currentHeight != Height.regular) {
			spriteRenderer.color = Global.instance.orange;
		}

		UpdateScale ();
	}

	public void ChangeWidth(int newWidth){
		switch (newWidth) {
			case 0:
				transform.localScale = new Vector3(Global.instance.smallWidth, currentScale.y, currentScale.z);
				currentWidth = Width.small;
				ChangeColor(Global.instance.small);
				width = 2;
				break;
				
			case 1:
				transform.localScale = new Vector3(Global.instance.medWidth, currentScale.y, currentScale.z);
				currentWidth = Width.medium;
				ChangeColor(Global.instance.medium);
				width = 3;
				break;
				
			case 2:
				transform.localScale = new Vector3(Global.instance.largeWidth, currentScale.y, currentScale.z);
				currentWidth = Width.large;
				ChangeColor(Global.instance.defaultColor);
				width = 5;
				break;

		}
		
		if (currentHeight != Height.regular) {
			//spriteRenderer.color = Global.instance.orange;
		}
		UpdateScale ();
	}

	public void SetContainer(GameObject container){

		this.container = container;

		//print ("container: " + container.transform.GetInstanceID());
		//print ("transform: " + transform.GetInstanceID());

		if (container == Global.instance.topContainer) {
			currentContainer = Container.top;
		}
		else{
			currentContainer = Container.bottom;
		}

		//print ("currentContainer: " + currentContainer);
	}

	public void ChangeColor(Color colorChange){
		spriteRenderer.color = colorChange;
	}

	public void CheckColor(){
		int scale = (int)currentScale.x;
		switch (scale) {
		case 2:
			currentWidth = Width.small;
			ChangeColor(Global.instance.small);
			break;
			
		case 3:
			currentWidth = Width.medium;
			ChangeColor(Global.instance.medium);
			break;
			
		case 5:
			currentWidth = Width.large;
			ChangeColor(Global.instance.defaultColor);
			break;
			
		}
	}
	
	public void UpdateScale(){
		currentScale = this.transform.localScale;
	}

	public void AddToWorld(){
		if(currentContainer == Container.bottom){
			ProceduralGeneration.instance.AddBottomPiece(id, height, width);
		}
		else{
			ProceduralGeneration.instance.AddTopPiece(id, height, width);
		}
	}
}
