using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProcess : MonoBehaviour{
	
	public bool isPumpkin = true;
	private bool canHarvest = false;
	public float growInterval = 4f;
	
	public GameObject smallPlant;
	public GameObject mediumPlant;
	public GameObject largePlant;
	public GameObject harvestablePlant;
	
    void Start(){
		smallPlant.SetActive(true);
		mediumPlant.SetActive(false);
		largePlant.SetActive(false);
		harvestablePlant.SetActive(false);

		gameObject.GetComponent<PickUp>().canPickup = false;
		gameObject.GetComponent<PickUp_Pulse>().enabled = false;
		gameObject.GetComponent<PickUp_Inventory>().canPickup = false;

		StartCoroutine(GrowProcess());
    }
	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			if (!canHarvest){
				growInterval += 0.1f;
				//make the player animate watering the plant, and make it grow faster?
			}
			
		}
	}
	
	IEnumerator GrowProcess(){
		yield return new WaitForSeconds(growInterval);
		smallPlant.SetActive(false);
		mediumPlant.SetActive(true);
		yield return new WaitForSeconds(growInterval);
		mediumPlant.SetActive(false);
		largePlant.SetActive(true);
		yield return new WaitForSeconds(growInterval);
		largePlant.SetActive(false);
		harvestablePlant.SetActive(true);
		gameObject.GetComponent<PickUp>().canPickup = true;
		gameObject.GetComponent<PickUp_Pulse>().enabled = true;
		gameObject.GetComponent<PickUp_Inventory>().canPickup = true;
		canHarvest = true;
	}

}
