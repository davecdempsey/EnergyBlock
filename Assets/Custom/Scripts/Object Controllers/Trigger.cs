using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	//			Script
	// public
	public CornerController cornerController;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider != null) {
			cornerController.Collision(collider.gameObject, gameObject);
		}
	}
}
