using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {

	//			GameEngine
	// public
	public static GameEngine instance;

	//			GameObject
	// public 
	public GameObject edgePrefab;

	// private
	private GameObject bottomContainer;
	private GameObject topContainer;
	private GameObject tempGameObject;
	private List<GameObject> bottomEdges;
	private List<GameObject> topEdges;
	
	//			Script
	// private
	private List<EdgePiece> bottomEdgeScripts;
	private List<EdgePiece> topEdgeScripts;

	// private
	private EdgePiece tempScript;
	
	//			Vector3
	// private
	private Vector3 newPosition;

	//			Vector2
	// private
	private Vector2 newEdgeDimensions;

	//			float
	// private
	private float newDistance;

	void Awake(){
		instance = this;

		bottomEdges = new List<GameObject> ();
		topEdges = new List<GameObject> ();
		
		bottomEdgeScripts = new List<EdgePiece> ();
		topEdgeScripts = new List<EdgePiece> ();

		bottomContainer = Global.instance.bottomContainer;
		topContainer = Global.instance.topContainer;
	}

	// Update is called once per frame
	void Update () {
		if (Global.instance.currentState == Global.GameState.gamePlay) {

			if(bottomEdges.Count < 1){
				StartGame();
			}

			CheckEdges ();
		}
	}

	void StartGame(){
		bottomEdges.Add (Global.instance.firstBottom);
		bottomEdgeScripts.Add (Global.instance.firstBottom.GetComponent<EdgePiece>());

		topEdges.Add (Global.instance.firstTop);
		topEdgeScripts.Add(Global.instance.firstTop.GetComponent<EdgePiece>());

		AddEdge(bottomEdgeScripts, bottomEdges, 0 , bottomContainer);
		AddEdge(topEdgeScripts, topEdges, 0 , topContainer);
	}

	/// <summary>
	/// Checks the edges.
	/// </summary>
	void CheckEdges(){
		for (int i = 0; i < bottomEdges.Count; i += 1) {
			
			// if the bottom edge is visible and the right piece game obect is set to null
			if(bottomEdges[i].renderer.isVisible && bottomEdgeScripts[i].rightPiece == null){
				// add a new piece to the world
				AddEdge(bottomEdgeScripts, bottomEdges, i , bottomContainer);
			}


		}
		for (int i = 0; i < topEdges.Count; i += 1) {
			
			// if the top edge is visible and the right piece game obect is set to null
			if(topEdges[i].renderer.isVisible && topEdgeScripts[i].rightPiece == null){
				// add a new piece to the world
				AddEdge(topEdgeScripts, topEdges, i , topContainer);
			}
			
			
		}

	}

	void AddEdge(List<EdgePiece> scriptList, List<GameObject> edgeList, int currentEdge, GameObject container){
		// instantiating new object (SPAWNPOOL)
		tempGameObject = SpawnPoolController.instance.Get (edgePrefab.name);
		tempGameObject.SetActive (true);

		// temp script for the EdgeScript
		tempScript = SpawnPoolController.instance.edgeScripts [tempGameObject.transform.GetInstanceID ()];
		scriptList.Add (tempScript);

		tempScript.SetContainer (container);

		///////			TESTING BEGIN
		if (container = Global.instance.topContainer) {
			newEdgeDimensions = ProceduralGeneration.instance.NewTopPiece ();		
		}
		else{
			newEdgeDimensions = ProceduralGeneration.instance.NewBottomPiece ();	
		}

		tempScript.ChangeWidth ((int)newEdgeDimensions.x);
		tempScript.ChangeHeight ((int)newEdgeDimensions.y);
		///////			TESTING END

		tempScript.AddToWorld ();

		// updating the sprite size of the edge
		tempScript.UpdateWorldScale ();

		// new position of the newly added edge
		newPosition = edgeList[currentEdge].transform.position;
		newPosition = new Vector3(edgeList[currentEdge].transform.position.x + FindDistance(scriptList [currentEdge], tempScript), edgeList[currentEdge].transform.position.y, edgeList[currentEdge].transform.position.z);

		tempGameObject.gameObject.transform.position = newPosition;

		scriptList[currentEdge].rightPiece = tempGameObject;
		
		// add new piece and scripts to lists
		edgeList.Add(tempGameObject);
		
		// making the container its parent
		tempGameObject.transform.parent = container.transform;

	}
	
	float FindDistance(EdgePiece previousEdgeScript, EdgePiece newEdgeScript){

		newDistance = (previousEdgeScript.worldScale.x / 2f) + (newEdgeScript.worldScale.x / 2f);

		return newDistance;
	}

	
	/// <summary>
	/// Removes game object from lists.
	/// </summary>
	/// <param name="go">GameObject.</param>
	/// <param name="s">Script of the GameObject.</param>
	public void RemoveFromLists(GameObject go, EdgePiece s){
		// remove new piece and scripts to lists
		if(bottomEdges.Contains(go)){
			bottomEdges.Remove(go);
			bottomEdgeScripts.Remove(s);
		}
		else{
			topEdges.Remove(go);
			topEdgeScripts.Remove(s);
		}
		
		// disable
		SpawnPoolController.instance.Disable (go);
	}
}
