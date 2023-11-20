using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTeleporter_Enemy : MonoBehaviour{
	
	public Transform entryPoint;
	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy"){
			//teleport to other front door
			other.gameObject.transform.position = entryPoint.position;
			
			other.gameObject.GetComponent<EnemyMoveHit>().goInside();
		}	
	}
	
}
