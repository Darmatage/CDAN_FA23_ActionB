using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game_Inventory : MonoBehaviour {
	public GameObject InventoryMenu;
	public GameObject CraftingMenu;
	public bool InvIsOpen = false;
	public bool CraftIsOpen = false;

	public GameObject CraftButton_PitchFork;
	public GameObject CraftButton_PumpkinTroop;
	public GameObject CraftButton_Gasoline;
	public GameObject CraftButton_Scarecrow;
	public GameObject CraftButton_FlamingHay;	
	
	public Transform CraftSpawn;
	
	public GameObject defender_Pitchfork;
	public GameObject defender_PumpkinTroop;
	public GameObject defender_Scarecrow;
	public GameObject defender_FlamingHay;

      //5 Inventory Items:
      public static bool item1bool = false; // pumpkin, harvested
      public static bool item2bool = false; // grain, harvested
      public static bool item3bool = false; // sticks, gathered
      public static bool item4bool = false; // iron, gathered
      public static bool item5bool = false; // coal, gathered
	  public static bool item6bool = false; // gasoline
      public static bool item7bool = false; 
      public static bool item8bool = false; // seeds, pumpkin
      public static bool item9bool = false; // seeds, grain

      public static int item1num = 0;
      public static int item2num = 0;
      public static int item3num = 0;
      public static int item4num = 0;
      public static int item5num = 0;
      public static int item6num = 0;
      public static int item7num = 0;
      public static int item8num = 0;
      public static int item9num = 0;	  
	  
	  
      //public static int coins = 0;

      [Header("Add item image objects here")]
      public GameObject item1image;
      public GameObject item2image;
      public GameObject item3image;
      public GameObject item4image;
      public GameObject item5image;
      public GameObject item6image;
      public GameObject item7image;
      public GameObject item8image;
      public GameObject item9image;
      //public GameObject coinText;

      // Item number text variables. Comment out if each item is unique (1/2).
      [Header("Add item number Text objects here")]
      public Text item1Text;
      public Text item2Text;
      public Text item3Text;
      public Text item4Text;
      public Text item5Text;
      public Text item6Text;
      public Text item7Text;
      public Text item8Text;
      public Text item9Text;

      // Crafting buttons. Uncomment and add for each button:
       public GameObject buttonCraft1; // weapon1 creation

	void Start(){
		InventoryMenu.SetActive(false);
		CraftingMenu.SetActive(false);
		InventoryDisplay();
			
		CraftButton_PitchFork.SetActive(false);;
		CraftButton_PumpkinTroop.SetActive(false);;
		CraftButton_Gasoline.SetActive(false);;
		CraftButton_Scarecrow.SetActive(false);;
		CraftButton_FlamingHay.SetActive(false);;
	}

	void InventoryDisplay(){
            if (item1bool == true) {item1image.SetActive(true);} else {item1image.SetActive(false);}
            if (item2bool == true) {item2image.SetActive(true);} else {item2image.SetActive(false);}
            if (item3bool == true) {item3image.SetActive(true);} else {item3image.SetActive(false);}
            if (item4bool == true) {item4image.SetActive(true);} else {item4image.SetActive(false);}
            if (item5bool == true) {item5image.SetActive(true);} else {item5image.SetActive(false);}
            if (item6bool == true) {item6image.SetActive(true);} else {item6image.SetActive(false);}
            if (item7bool == true) {item7image.SetActive(true);} else {item7image.SetActive(false);}
            if (item8bool == true) {item8image.SetActive(true);} else {item8image.SetActive(false);}
            if (item9bool == true) {item9image.SetActive(true);} else {item9image.SetActive(false);}
			
            //Text coinTextB = coinText.GetComponent<Text>();
            //coinTextB.text = ("COINS: " + coins);

            // Item number updates. Comment out if each item is unique (2/2).
            Text item1TextB = item1Text.GetComponent<Text>();
            item1TextB.text = ("" + item1num);

            Text item2TextB = item2Text.GetComponent<Text>();
            item2TextB.text = ("" + item2num);

            Text item3TextB = item3Text.GetComponent<Text>();
            item3TextB.text = ("" + item3num);

            Text item4TextB = item4Text.GetComponent<Text>();
            item4TextB.text = ("" + item4num);

            Text item5TextB = item5Text.GetComponent<Text>();
            item5TextB.text = ("" + item5num);
			
			Text item6TextB = item6Text.GetComponent<Text>();
            item6TextB.text = ("" + item6num);

            Text item7TextB = item7Text.GetComponent<Text>();
            item7TextB.text = ("" + item7num);

            Text item8TextB = item8Text.GetComponent<Text>();
            item8TextB.text = ("" + item8num);

            Text item9TextB = item9Text.GetComponent<Text>();
            item9TextB.text = ("" + item9num);
			
			
      }

      public void InventoryAdd(string item){
            string foundItemName = item;
            if (foundItemName == "item1") {item1bool = true; item1num ++;}
            else if (foundItemName == "item2") {item2bool = true; item2num ++;}
            else if (foundItemName == "item3") {item3bool = true; item3num ++;}
            else if (foundItemName == "item4") {item4bool = true; item4num ++;}
            else if (foundItemName == "item5") {item5bool = true; item5num ++;}
			else if (foundItemName == "item6") {item6bool = true; item6num ++;}
            else if (foundItemName == "item7") {item7bool = true; item7num ++;}
            else if (foundItemName == "item8") {item8bool = true; item8num ++;}
            else if (foundItemName == "item9") {item9bool = true; item9num ++;}
            else { Debug.Log("This item does not exist to be added"); }
            InventoryDisplay();

            if (!InvIsOpen){
                  OpenCloseInventory();
            }
      }

      public void InventoryRemove(string item, int num){
            string itemRemove = item;
            if (itemRemove == "item1") {
                  item1num -= num;
                  if (item1num <= 0) { item1bool =false; }
                  // Add any other intended effects: new item crafted, speed boost, slow time, etc
             }
            else if (itemRemove == "item2") {
                  item2num -= num;
                  if (item2num <= 0) { item2bool =false; }
                  // Add any other intended effects
             }
            else if (itemRemove == "item3") {
                  item3num -= num;
                  if (item3num <= 0) { item3bool =false; }
                    // Add any other intended effects
            }
            else if (itemRemove == "item4") {
                  item4num -= num;
                  if (item4num <= 0) { item4bool =false; }
                    // Add any other intended effects
            }
            else if (itemRemove == "item5") {
                  item5num -= num;
                  if (item5num <= 0) { item5bool =false; }
                    // Add any other intended effects
            }
            else if (itemRemove == "item6") {
                  item6num -= num;
                  if (item6num <= 0) { item6bool =false; }
                  // Add any other intended effects
             }
            else if (itemRemove == "item7") {
                  item7num -= num;
                  if (item7num <= 0) { item7bool =false; }
                    // Add any other intended effects
            }
            else if (itemRemove == "item8") {
                  item8num -= num;
                  if (item8num <= 0) { item8bool =false; }
                    // Add any other intended effects
            }
            else if (itemRemove == "item9") {
                  item9num -= num;
                  if (item9num <= 0) { item9bool =false; }
                    // Add any other intended effects
            }			
			
            else { Debug.Log("This item does not exist to be removed"); }
            InventoryDisplay();
      }

      //public void CoinChange(int amount){
            //coins +=amount;
            //InventoryDisplay();
      //}

      // Open and Close the Inventory. Use this function on a button next to the inventory bar.
      public void OpenCloseInventory(){
            if (InvIsOpen){ InventoryMenu.SetActive(false); }
            else { InventoryMenu.SetActive(true); }
            InvIsOpen = !InvIsOpen;
      }

      //Open and Close the Cookbook
      //public void OpenCraftBook(){CraftMenu.SetActive(true);}
      //public void CloseCraftBook(){CraftMenu.SetActive(false);}


	// plant seeds #1
	public void PlantSeeds1(){
		InventoryRemove("item8", 1);
		GameObject.FindWithTag("Player").GetComponent<PlayerPlanting>().playerPlanting1();
	}

	// plant seeds #2
	public void PlantSeeds2(){
		InventoryRemove("item9", 1);
		GameObject.FindWithTag("Player").GetComponent<PlayerPlanting>().playerPlanting2();
	}

	// CRAFTING MENU:

	public void OpenCloseCraftingMenu(){
		if (CraftIsOpen){ CraftingMenu.SetActive(false); }
        else { CraftingMenu.SetActive(true); }
            CraftIsOpen = !CraftIsOpen;
	}

	// pitchfork: iron (item 4) + sticks (item 3) = spawn
	public void Craft_Pitchfork(){
		InventoryRemove ("item4", 1);
		InventoryRemove ("item3", 1);
		Instantiate(defender_Pitchfork, CraftSpawn.position, Quaternion.identity);
	}

	// Pumpkin Troop: pumpkin (item1) + stick(item3) = spawn 
	public void Craft_PumpkinTroop(){
		InventoryRemove ("item1", 1);
		InventoryRemove ("item3", 1);
		Instantiate(defender_PumpkinTroop, CraftSpawn.position, Quaternion.identity);
	}
	
	// gasoline: iron (item 4) + coal(item 5) + grain (item2) = gasoline (item 6)
	public void Craft_Gasoline(){
		InventoryRemove ("item4", 1);
		InventoryRemove ("item5", 1);
		InventoryRemove ("item2", 1);
		InventoryAdd("item6");
	}
	
	// Scarecrow (pumpkin captain): pumpkin (item1) + stick(item3) + grain (item2) = spawn 
	public void Craft_Scarecrow(){
		InventoryRemove ("item1", 1);
		InventoryRemove ("item3", 1);
		InventoryRemove ("item2", 1);
		Instantiate(defender_Scarecrow, CraftSpawn.position, Quaternion.identity);
	}

	//flamable hay stack: grain (item2) + gasoline(item6)  = spawn
	public void Craft_FlamingHay(){
		InventoryRemove ("item2", 1);
		InventoryRemove ("item6", 1);
		Instantiate(defender_FlamingHay, CraftSpawn.position, Quaternion.identity);
	}





      // Reset all static inventory values on game restart.
      public void ResetAllInventory(){
            item1bool = false;
            item2bool = false;
            item3bool = false;
            item4bool = false;
            item5bool = false;
			item6bool = false;
            item7bool = false;
            item8bool = false;
            item9bool = false;

            item1num = 0; // object name
            item2num = 0; // object name
            item3num = 0; // object name
            item4num = 0; // object name
            item5num = 0; // object name
		    item6num = 0; // object name
            item7num = 0; // object name
            item8num = 0; // object name
            item9num = 0; // object name
      }

}
