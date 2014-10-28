using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoolController : MonoBehaviour {

	//			SpawnPoolController
	// public
	public static SpawnPoolController instance;

	//			ObjectToPool
	// public
	public ObjectToPool[] spawnableGameObject;                  // object to want to insantiate to the pool

	//			Dictionary
	// public
	public Dictionary<string, Dictionary<int, GameObject>> spawnPool;
	public Dictionary<string, List<int>> objectIDs;
	public Dictionary<int, EdgePiece> edgeScripts;

	//			int
	// private
	private int spawnCounter;                                   // the counter of the spawnable objects

	//			GameObject
	// public
	public GameObject spawnPoolContainer;

	// private
	private GameObject tempGameObject;


	//			bool
	// public
	public bool loaded = false;

	void Awake () {
		instance = this;

		spawnPool = new Dictionary<string, Dictionary<int, GameObject>> ();
		objectIDs = new Dictionary<string, List<int>> ();
		edgeScripts = new Dictionary<int, EdgePiece> ();


		// spawn counter counts the amount of objects to be spawned 
		spawnCounter = 0;

		StartCoroutine("SpawnGameObjects");
	}
	
	// Update is called once per frame
	void Update () {

	}
	

	IEnumerator SpawnGameObjects () {

		yield return null;
		while (spawnCounter != spawnableGameObject.Length){

			for (int i = 0; i < spawnableGameObject.Length; i++){

				// if the pool doesnt contain the name
				if (!spawnPool.ContainsKey(spawnableGameObject[i].gameObject.name.ToLower())){

					spawnPool.Add(spawnableGameObject[i].gameObject.name.ToLower(), new Dictionary<int ,GameObject>());
					objectIDs.Add(spawnableGameObject[i].gameObject.name.ToLower(), new List<int>());
				}
				
				// if the pool if count is less than the amount to be spawned
				if (spawnPool[spawnableGameObject[i].gameObject.name.ToLower()].Count < spawnableGameObject[i].amount){

					// instantiaing the game and pushig it onto the pool
					tempGameObject = Instantiate(spawnableGameObject[i].gameObject) as GameObject;

					// disabling gameObject
					Disable(tempGameObject);

					// adding gameobject, id and script to collections
					spawnPool[spawnableGameObject[i].gameObject.name.ToLower()].Add(tempGameObject.transform.GetInstanceID(), tempGameObject);
					objectIDs[spawnableGameObject[i].gameObject.name.ToLower()].Add(tempGameObject.transform.GetInstanceID());
					edgeScripts.Add(tempGameObject.transform.GetInstanceID(), tempGameObject.GetComponent<EdgePiece>());
				} 

				else {
					// increasing the spawnCounter
					spawnCounter += 1;
				}
			}
			yield return null;
		}

		yield return null;
		System.GC.Collect();
		yield return null;

		GameController.instance.StartGame ();
	}

	public GameObject Get(string prefabName)
	{
		// if the pool contains the key which is the name of the prefab
		if (spawnPool.ContainsKey(prefabName.ToLower()))
		{
			try
			{
				for (int i = 0; i < spawnPool[prefabName.ToLower()].Count; i++)
				{
					if (!spawnPool[prefabName.ToLower()][objectIDs [prefabName.ToLower()] [i]].activeInHierarchy)
					{
						return spawnPool[prefabName.ToLower()][objectIDs [prefabName.ToLower()] [i]];
					}
				}
				print(prefabName + " has run out in the pool!");
				return null;
			}
			catch (System.Exception e)
			{
				print (e);
				return null;
			}
		}
		else
		{
			print("is null " + prefabName);
			return null;
		}
	}

	public void AddEdgeToCollections(GameObject go){
		// if the pool doesnt contain the name
		if (!spawnPool.ContainsKey(go.name.ToLower())){
			
			spawnPool.Add(go.name.ToLower(), new Dictionary<int ,GameObject>());
			objectIDs.Add(go.name.ToLower(), new List<int>());
		}

		// adding gameobject, id and script to collections
		spawnPool[go.gameObject.name.ToLower()].Add(go.transform.GetInstanceID(), go);
		objectIDs[go.gameObject.name.ToLower()].Add(go.transform.GetInstanceID());
		edgeScripts.Add(go.transform.GetInstanceID(), go.GetComponent<EdgePiece>());
	}

	public void Disable(GameObject go){
		// setting the new objects parent and disabling
		go.transform.parent = spawnPoolContainer.transform;
		go.SetActive (false);
	}

}

[System.Serializable] 
public class ObjectToPool
{
	public GameObject gameObject;
	public int amount;
}
