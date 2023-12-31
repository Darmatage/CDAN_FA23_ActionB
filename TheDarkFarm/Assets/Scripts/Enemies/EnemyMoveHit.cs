using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

	public float knockBackForce = 10f;
	private Animator anim;
	private Rigidbody2D rb2D;
	public float speed = 4f;
	
	public Transform target;
	private Transform targetPlayer;
	private Transform targetHouse;
	private Transform targetHouseArtifact;
	private Transform targetDefender;
	
	private bool targetSwitch1 = false;
	private bool targetSwitch2 = false;
	public float delayTimeSwitch = 1f;
	   
	public int damage = 6;

	//public int EnemyLives = 3;
	private GameHandler gameHandler;

	public float attackRange = 5;
	public bool isAttacking = false;
	private float scaleX;
	   
	public AudioSource attackSFX;

	public bool isInside = false;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		scaleX = gameObject.transform.localScale.x;

		if (GameObject.FindWithTag ("GameHandler") != null) {
			gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
		}

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			targetPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		}
			  
		if (GameObject.FindGameObjectWithTag ("House") != null) {
			targetHouse = GameObject.FindGameObjectWithTag ("House").GetComponent<Transform> ();
		}
			
		if (GameObject.FindGameObjectWithTag ("House_Artifact") != null) {
			targetHouseArtifact = GameObject.FindGameObjectWithTag ("House_Artifact").GetComponent<Transform> ();
		}
			  
		target = targetHouse;
	}

	void Update () {
		float DistToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

		if (DistToPlayer <= attackRange){
			target = targetPlayer;
			targetSwitch1 = true;
			targetSwitch2 = true;
		} else {
			if (targetSwitch1){
				target = null;
				anim.SetBool("Walk", false);
				if(targetSwitch2){ 
					targetSwitch2 = false; //this makes sure the coroutine is only activated one time
					//StopCoroutine(targetSwitchDelay());
					StartCoroutine(targetSwitchDelay());
				}
			} 
			//if ready to get new target (!targetswitch1):
			else {
				if (!isInside){target = targetHouse;}
				else {target = targetHouseArtifact;}
			}
		}

		if (target != null){
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			anim.SetBool("Walk", true);
			//flip enemy to face player direction. Wrong direction? Swap the * -1:
			if (target.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			}
		}
		//else { anim.SetBool("Walk", false);}
	}

	public void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			if (!attackSFX.isPlaying){
				attackSFX.Play();
			}
				  
			isAttacking = true;
			anim.SetBool("Attack", true);
			gameHandler.playerGetHit(damage);
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			//StartCoroutine(HitEnemy());

			//This method adds force to the player, pushing them back without teleporting:
			Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
			Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
					
			//tell player to end knockback:
			other.gameObject.GetComponent<PlayerMoveAround>().EndTheKnockBack();
			pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
			//StartCoroutine(EndKnockBack(pushRB));
		}
	}

/*
     IEnumerator EndKnockBack(Rigidbody2D otherRB){
              yield return new WaitForSeconds(0.2f);
              otherRB.velocity= new Vector3(0,0,0);
       }
*/   
	   
	public void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			isAttacking = false;
			//anim.SetBool("Attack", false);
		}
	}

	IEnumerator targetSwitchDelay(){
		yield return new WaitForSeconds(delayTimeSwitch);
		targetSwitch1 = false;
	}

	//this function is referenced by HouseTeleporter_Enemy.cs
	public void goInside(){
		isInside = true;
	}


	//DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}