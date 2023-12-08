using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour{

	//private Animator anim;
	public Transform plantPoint;
	private GameObject gameHandler;
	
	public GameObject plantPumpkin;
	public GameObject plantStraw;
	
	//public AudioSource plantingSFX;

    void Start(){
		//anim.GetComponentInChildren<Animator>();
        gameHandler = GameObject.FindWithTag("GameHandler");
	}

    void Update(){
		if (GameHandler_DayNightPhases.isDayPhase==true){
			if ((Input.GetKeyDown("1"))&&(Game_Inventory.item8num > 0)){
				Debug.Log("Player tried to plant a pumpkin plant: " + plantPoint.position);
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds1();
				//playerPlanting1();
			} else {Debug.Log("");}
			if ((Input.GetKeyDown("2"))&&(Game_Inventory.item9num > 0)){
				Debug.Log("Player tried to plant a straw plant: " + plantPoint.position);
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds2();
				//playerPlanting2();
			}
		}
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
