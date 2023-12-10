using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTeleporter_Player2 : MonoBehaviour{
	
	private Transform player;
	private Transform cameraMain;
	public Transform theDoor;
	
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
		cameraMain = GameObject.FindWithTag("MainCamera").transform;
    }

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			//teleport to other door
			player.position = theDoor.position;
			Debug.Log("Player teleported to:" + theDoor.position);
			Vector3 camPos = new Vector3(theDoor.position.x, theDoor.position.y, -10);
			cameraMain.position = camPos;
			Debug.Log("Camera teleported to:" + camPos);	
		}
	}
}
