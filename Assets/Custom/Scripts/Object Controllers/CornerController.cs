using UnityEngine;
using System.Collections;

public class CornerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void Collision(GameObject collision, GameObject other){
		//print (collision.name + " with id : " + collision.transform.GetInstanceID() + " collided with: " + other.name + " with id : " + other.transform.GetInstanceID());
	}
}
