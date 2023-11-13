using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp_Inventory: MonoBehaviour {

      private Game_Inventory gameInventory;
      public string ItemName = "item1";
	  
	  public bool canPickup = true;

      void Awake(){
            if (GameObject.FindWithTag("GameHandler") != null) {
                  gameInventory = GameObject.FindWithTag("GameHandler").GetComponent<Game_Inventory>();
            }
      }

      void OnTriggerEnter2D(Collider2D other){
            if ((other.gameObject.tag == "Player")&&(canPickup)){
                  //Debug.Log("You found an" + ItemName);
                  gameInventory.InventoryAdd(ItemName);
            }
      }
}

//NOTE: This script is meant to co-exist on the pickup object with PickUp.cs,
// which handles SFX and destruction (turn off boosts) 
