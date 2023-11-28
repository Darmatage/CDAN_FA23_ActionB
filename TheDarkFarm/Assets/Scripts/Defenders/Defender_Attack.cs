
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Defender_Attack : MonoBehaviour {
		
	public bool isPumpkinTroop;	
	public int pumpkinTroopDamage = 5;
	public int scarecrowDamage = 10;
		
	float scaleX = 0;
	//private Animator anim;
       
	public bool attackEnemy = false; // target enemy within range of player when player attacks
	public bool isAttacking = false; // attack a targeted enemy
       

//Attack variables
	public LayerMask enemyLayers;
	public GameObject enemyTarget;
	private Vector2 enemyPos;
	private float distToEnemy;
	private float timeBtwShots;
	public float startTimeBtwShots = 2;
	public GameObject projectile;
	public float attackRange = 10f;
       
	private int attackLimit = 10;  
	private int attackNum = 0;  	
	   
	void Start(){
		//anim = gameObject.GetComponentInChildren<Animator>();
		scaleX = gameObject.transform.localScale.x;
		
		//cut time to shoot in half for scarecrow? Or should it be doubled?
		if (isPumpkinTroop == false){
			startTimeBtwShots = startTimeBtwShots/2;
		}
	}

	void FixedUpdate(){
		
		//if enemy is near, enter combat
		if (GameHandler_DayNightPhases.isDayPhase==false){
			attackEnemy = true;
			FindTheEnemy();
		}

		//TRACK / FOLLOW / TURN TOWARDS ENEMY        
		if ((attackEnemy) && (enemyTarget != null)){
			enemyPos = enemyTarget.transform.position;
			distToEnemy = Vector2.Distance(transform.position, enemyPos);

			// Turn buddy toward enemy
			if (enemyTarget.transform.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			}
		}


		//Timer for shooting projectiles
		if ((attackEnemy==true)&&(enemyTarget !=null) && (distToEnemy < attackRange)){
			if (timeBtwShots <= 0) {
				isAttacking = true;
				//anim.SetBool("Attack", true);

				
				GameObject myProjectile = Instantiate (projectile, transform.position, Quaternion.identity);
				myProjectile.GetComponent<Defender_projectile>().enemyTrans = enemyTarget.transform;
				if (isPumpkinTroop){myProjectile.GetComponent<Defender_projectile>().damage = pumpkinTroopDamage;}
				else {myProjectile.GetComponent<Defender_projectile>().damage = scarecrowDamage;}

				attackNum += 1; 
				if (attackNum >= attackLimit){
					//need to instantiate destroyed troop before removing troop
					Destroy(gameObject);
				}

				timeBtwShots = startTimeBtwShots;
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
				//anim.SetBool("Attack", false);
			}
		} 
		//else {anim.SetBool("Attack", false);}

		//end bracket of FixedUpdate:
	}


	void FindTheEnemy(){
		//animator.SetTrigger ("Melee");
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("Buddy targeting " + enemy.name);
			enemyTarget = enemy.gameObject;
			//enemy.GetComponent ().TakeDamage(attackDamage);
		}
	}


	// DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

} 