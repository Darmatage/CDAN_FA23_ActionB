using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit_Woods : MonoBehaviour {

	public bool isDay = true;
	public GameObject creatureNight; //active monster at night
	public GameObject creatureDay; //inert object -- stone?
	private bool turnedToDay = false;
	private bool turnedToNight = false;
	
	private Animator anim;
	private Rigidbody2D rb2D;
	
	public float knockBackForce = 20f;
	public float speed = 4f;
	private Transform target;
	public int damage = 10;

	//public int EnemyLives = 3;
	private GameHandler gameHandler;

	public float attackRange = 5;
	public bool isAttacking = false;
	private float scaleX;

	public bool isWalking = true;
	public float walkRange = 10;
	private Vector2 lastPos;
	private Vector2 nextPos;

	void Start () {
		anim = GetComponentInChildren<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		scaleX = gameObject.transform.localScale.x;

		creatureNight.SetActive(false);
		creatureDay.SetActive(true);

		//for chasing and attacking
		if (GameObject.FindGameObjectWithTag("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		}

		//for recording attacks on player health
		if (GameObject.FindWithTag ("GameHandler") != null) {
			gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
		}
	}

	void Update () {
		if ((GameHandler_DayNightPhases.isDayPhase)&&(!turnedToDay)){
			isDay=true;
			turnedToDay = true;
			turnedToNight = false;
			ChangeFormToDay();
		}
		else if ((!GameHandler_DayNightPhases.isDayPhase==false)&&(!turnedToNight)) {
			isDay=false;
			turnedToDay = false;
			turnedToNight = true;
			ChangeFormToNight();
		}
		
		float DistToPlayer = Vector3.Distance(transform.position, target.position);

		if ((target != null) && (DistToPlayer <= attackRange) && (!isDay)){
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			anim.SetBool("Walk", true);

			//flip enemy to face player direction. Wrong direction? Swap the * -1.
			if (target.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			}
		}
		else { anim.SetBool("Walk", false);}
	}

	public void OnTriggerEnter2D(Collider2D other){
		if ((other.gameObject.tag == "Player")&&(!isDay)) {
			isAttacking = true;
			anim.SetTrigger("Attack");
			anim.SetBool("Walk", false);
			gameHandler.playerGetHit(damage);
					 
			//This method adds force to the player, pushing them back without teleporting:
			Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
			Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
					
			//but first tell player to end knockback:
			other.gameObject.GetComponent<PlayerMoveAround>().EndTheKnockBack();
			pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
		}
	}

	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			isAttacking = false;
		}
	}
	
	
	public void ChangeFormToDay(){
		creatureNight.SetActive(false);
		creatureDay.SetActive(true);
		anim.SetTrigger("becomeRock");
	}
	public void ChangeFormToNight(){
		creatureNight.SetActive(true);
		creatureDay.SetActive(false);
		anim.SetTrigger("becomeFlesh");
	}

	//DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, walkRange);
	}
}