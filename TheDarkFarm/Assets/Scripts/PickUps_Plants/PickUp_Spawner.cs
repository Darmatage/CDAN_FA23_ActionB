using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Spawner : MonoBehaviour{
	
	public GameObject currentPickUp; //optional: parent a copy of the prefab to spawner, drag into this slot to start
	public GameObject pickupPrefab; // drag the prefab from the prefab folder into this
	
	private float spawnTimeLimit = 30f; // spawns once a day?
	private float theTimer = 0;
	

    // Update is called once per frame
    void FixedUpdate(){
		theTimer += 0.01f;
		
		//at each time interval: 
		if (theTimer >= spawnTimeLimit){
			theTimer = 0;
			
			//if the Curerent Pickup slot is empty, add a new pickup!
			if (currentPickUp == null){
				GameObject newPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
				currentPickUp = newPickup;
				newPickup.transform.parent = gameObject.transform;
			}
			else {
				//Debug.Log("There is already a pickup at this spawner");
			}
		}
		
    }
	
}
