using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

	//			Script
	public static HudController instance;

	//			GameObject
	// public
	public GameObject energyBar;

	//			float
	// private
	private float energy;

	//			Gravity
	// public
	public Gravity currentGravity;
	public enum Gravity{
		normal, reverse
	}

	void Awake(){
		instance = this;
	}


	// Use this for initialization
	void Start () {
		currentGravity = Gravity.normal;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnergyBar ();
	}

	public void ReverseGravity(){
		
		if (currentGravity == Gravity.reverse) {
			// setting gravity
			Physics2D.gravity = new Vector3(0, Global.instance.gravity, 0);
			
			currentGravity = Gravity.normal;
			
			Global.instance.gravityDirection = 1f;
			
			if(Global.instance.gravityArrow.transform.localRotation.z != 0){
				
				Global.instance.gravityArrow.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
				
			}
		}
		
		else{
			// setting gravity
			Physics2D.gravity = new Vector3(0, (-1) * Global.instance.gravity, 0);
			
			currentGravity = Gravity.reverse;
			
			Global.instance.gravityDirection = -1f;
			
			if(Global.instance.gravityArrow.transform.localRotation.z != 180){
				
				Global.instance.gravityArrow.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180));
			}
		}
		
	}

	private void UpdateEnergyBar(){

		// updating the energy gathered
		energy = Global.instance.energyGathered / Global.instance.energyNeeded;

		if (energy > 1) {
			energy = 1;		
		}
		energyBar.transform.localScale = new Vector3(energy, 1f, 1f);
	}
}
