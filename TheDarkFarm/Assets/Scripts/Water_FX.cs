using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_FX : MonoBehaviour{
	
	//load all splash sounds in this array:
	public AudioSource[] splashes;
	
	private AudioSource theSplash;
	int splashNum = 0;

	float effectTimeLimit = 2f;
	float theTimer = 0;
	
	public bool inWater = false;
	public bool isWalking = false;
	public bool doneWalking = false;

	void Start(){
		//set initial sound effect:
		theSplash = splashes[0];
	}

	void FixedUpdate(){
		//determine of player is walking:
		if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){isWalking = true;} 
		else {isWalking=false; doneWalkingDelay();}
		
		if ((inWater)&&(!doneWalking)){
			//play the current sound:
			if (!theSplash.isPlaying){
				theSplash.Play();
			}
			
			//timer for switching sounds:
			/*
			theTimer += 0.01f;
			if (theTimer >= effectTimeLimit){
				SwitchEffects();
				theTimer = 0;
			}
			*/
		} 
		else {
			theSplash.Stop();
		}
	}

	//enter the collider, in water:
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="PlayerFeet"){
			inWater = true;
			doneWalking = false;
			int splashNum = Random.Range(0,splashes.Length);
			theSplash = splashes[splashNum];
		}
    }
	
	//exit the collider, not in water:
	void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag=="PlayerFeet"){
			inWater = false;
		}
    }
	
	IEnumerator doneWalkingDelay(){
		yield return new WaitForSeconds(2f);
		doneWalking = true;
	}
	
	
	//switch between sound effects:
	void SwitchEffects(){
		if (splashNum < (splashes.Length-1)){splashNum ++;}
		else {splashNum =0;}
		theSplash = splashes[splashNum];
	}
	
}
