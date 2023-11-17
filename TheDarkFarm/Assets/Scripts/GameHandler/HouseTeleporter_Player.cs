using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTeleporter_Player : MonoBehaviour{
	
	private Transform player;
	private Transform cameraMain;
	public Transform InnerDoor;
	public Transform OuterDoor;
	public bool isOutside = true;
	
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
		cameraMain = GameObject.FindWithTag("MainCamera").transform;
    }
	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			//teleport to other front door
			if (isOutside){
				player.position = InnerDoor.position;
				Vector3 camPos = new Vector3(InnerDoor.position.x, InnerDoor.position.y, -10);
				cameraMain.position = camPos;
				isOutside = false;
			} else {
				player.position = OuterDoor.position;
				Vector3 camPos = new Vector3(OuterDoor.position.x, OuterDoor.position.y, -10);
				cameraMain.position = camPos;
				isOutside = true;
			}
		}	
	}
	
}
