using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walkSounds : MonoBehaviour{
	
	public AudioSource theWalkSound;
	//public AudioSource theSplash;
	
	public bool inWater = false;
	public bool isWalking = false;
	public bool isTeleporting = false;
	
	public AudioSource walking_outdoors;
	public AudioSource walking_indoors;
	//public AudioSource[] splashes;
	public AudioSource walking_water;
	
    void Start(){
		//establish initial sounds:
        theWalkSound = walking_outdoors;
		//theSplash = splashes[0];
    }

    void FixedUpdate(){
		//if walking, play current walk sound
		if ((!isTeleporting)&&(!inWater)){
			if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
				isWalking = true;
				if (!theWalkSound.isPlaying){
					theWalkSound.Play();
					walking_water.Stop();
				}
			} 
			else {
				isWalking = false;
				theWalkSound.Stop();
			}
		} else {
			theWalkSound.Stop();
		}
		
		//if in water, play current splashes sound
		if (inWater){
			if (!walking_water.isPlaying){
				walking_water.Play();
				//StopCoroutine(EndWater());
				//StartCoroutine(EndWater());
			}
		}
		else {
			walking_water.Stop();
			//for (int i=0;i<splashes.Length;i++){
			//	splashes[i].Stop();
			//}
		}
		
		//switch inside and outside walking sounds
		if (GameHandler.isInside){
			theWalkSound = walking_indoors;
			walking_outdoors.Stop();
			} 
		else {
			theWalkSound = walking_outdoors;
			walking_indoors.Stop();
			}
    }
	
	//trigger water sounds when in water
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag=="Water"){
			//theSplash.Stop();
			inWater = true;
			//int splashNum = Random.Range(0,splashes.Length);
			//theSplash = splashes[splashNum];
		}
		
		/*
		if (other.gameObject.tag=="WaterStop"){
			theSplash.Stop();
			//also stop each water sound individually:
			for (int i=0;i<splashes.Length;i++){
				splashes[i].Stop();
			}
		}*/
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag=="Water"){
			inWater = false;
		}
	}
	
	public void TeleportStopWalkSounds(){
		theWalkSound.Stop();
		walking_outdoors.Stop();
		walking_indoors.Stop();
		isTeleporting = true;
	}
	
	public void TeleportStartWalkSounds(){
		isTeleporting = false;
	}
	
	IEnumerator EndWater(){
		yield return new WaitForSeconds(1f);
		walking_water.Stop();
	}
}
