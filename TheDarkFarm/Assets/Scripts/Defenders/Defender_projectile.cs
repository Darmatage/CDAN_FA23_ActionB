using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_projectile : MonoBehaviour{
	
    public int damage = 1;
	public float speed = 10f;
	public float SelfDestructTime = 2.0f;
	
	public GameHandler gameHandlerObj;
	public GameObject hitEffectAnim;
	public Transform enemyTrans;
	private Vector2 target;


	void Start() {
		//NOTE: transform gets location, but we need Vector2 for direction, so we can use MoveTowards.
		
		target = new Vector2(enemyTrans.position.x, enemyTrans.position.y);

		if (gameHandlerObj == null){
			gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		}
		StartCoroutine(selfDestruct());
	}

	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
	}

	//if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
		}
		
		if (other.gameObject.tag != "Player") {
			GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
			Destroy (animEffect, 0.5f);
			Destroy (gameObject);
		}
		
	}

	IEnumerator selfDestruct(){
		yield return new WaitForSeconds(SelfDestructTime);
		Destroy (gameObject);
	}
}