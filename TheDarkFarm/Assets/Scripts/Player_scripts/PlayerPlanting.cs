using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour{

	//private Animator anim;
	public Transform plantPoint;
	private GameObject gameHandler;
	
	public GameObject plantPumpkin;
	public GameObject plantStraw;
	
	private Transform playerObj;
	private Transform cameraMainObj;
	//public Transform playerArtForItchTest;
	public GameObject playerShadow;
	
	//public AudioSource plantingSFX;

    void Start(){
		//anim.GetComponentInChildren<Animator>();
        gameHandler = GameObject.FindWithTag("GameHandler");
		
		playerObj = GameObject.FindWithTag("Player").GetComponent<Transform>();
		cameraMainObj = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
	}

    void Update(){
		
		/*
		//this input is just for testing a bug on itch.io
		if (Input.GetKeyDown("p")){
			Debug.Log("The Player is currently at: " + playerObj.position + " and the Camera is at: " + cameraMainObj.position);
			Debug.Log("The Player Art is currently at: " + playerArtForItchTest.position);
		}
		*/

		if (GameHandler_DayNightPhases.isDayPhase==true){
			playerShadow.SetActive(true);
			/*
			if ((Input.GetKeyDown("1"))&&(Game_Inventory.item8num > 0)&&(!GameHandler.isInside)){
				Debug.Log("Player tried to plant a pumpkin plant: " + plantPoint.position);
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds1();
				//playerPlanting1();
			} 
			
			if ((Input.GetKeyDown("2"))&&(Game_Inventory.item9num > 0)&&(!GameHandler.isInside)){
				Debug.Log("Player tried to plant a straw plant: " + plantPoint.position);
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds2();
				//playerPlanting2();
			}
			*/
		} else {playerShadow.SetActive(false);}
    }

	public void playerPlanting1(){
		//plantingSFX.Play();
		Debug.Log("Player tried to plant a pumpkin plant: " + plantPoint.position);
		Instantiate(plantPumpkin, plantPoint.position, Quaternion.identity);
		//anim.SetTrigger("planting");
	}
	
	public void playerPlanting2(){
		//plantingSFX.Play();
		Debug.Log("Player tried to plant a straw plant: " + plantPoint.position);
		Instantiate(plantStraw, plantPoint.position, Quaternion.identity);
		//anim.SetTrigger("planting");
	}
	
}
