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
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds1();
				//playerPlanting1();
			}
			if ((Input.GetKeyDown("2"))&&(Game_Inventory.item9num > 0)){
				gameHandler.GetComponent<Game_Inventory>().PlantSeeds2();
				//playerPlanting2();
			}
		}
    }

	public void playerPlanting1(){
		//plantingSFX.Play();
		Instantiate(plantPumpkin, plantPoint.position, Quaternion.identity);
		//anim.SetTrigger("planting");
	}
	
	public void playerPlanting2(){
		//plantingSFX.Play();
		Instantiate(plantStraw, plantPoint.position, Quaternion.identity);
		//anim.SetTrigger("planting");
	}
	
}
