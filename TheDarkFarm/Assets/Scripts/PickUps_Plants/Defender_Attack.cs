using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Defender_Attack : MonoBehaviour {
       //this script offers basic flocking behavior for friendly NPCs to follow the player
       //commented-out are functions for followers to help attack enemies if the player attacks
		
		float scaleX = 0;
		float followDistance = 5f;
       //private Animator anim;
       
        public bool attackEnemy = false; // target enemy within range of player when player attacks
        public bool isAttacking = false; // attack a targeted enemy
        public float peacefullTime = 4f;
       

//Attack variables
        public LayerMask enemyLayers;
        public GameObject enemyTarget;
        private Vector2 enemyPos;
        private float distToEnemy;
        private float timeBtwShots;
        public float startTimeBtwShots = 2;
        public GameObject projectile;
        public float attackRange = 10f;
       

       void Start(){
              //anim = gameObject.GetComponentInChildren<Animator>();
              //moveSpeed = Random.Range((topSpeed * 0.7f), topSpeed);
              scaleX = gameObject.transform.localScale.x;
       }

/*
       void Update(){
              //if enemy is near, enter combat
              
              if (){
                     followPlayer = false;
                     attackEnemy = true;
                     StartCoroutine(StopAttackingEnemies());
                     FindTheEnemy();
               }
               
        }
*/
	void FixedUpdate(){

                //FOLLOW ENEMY
                /*
                if ((attackEnemy) && (enemyTarget != null)){
                        enemyPos = enemyTarget.transform.position;
                        distToEnemy = Vector2.Distance(transform.position, enemyPos);

                        // Retreat from enemy
                        if (distToEnemy <= followDistance){
                                transform.position = Vector2.MoveTowards (transform.position, enemyPos, -moveSpeed * Time.deltaTime);
                                anim.SetBool("Walk", true);
                        }

                        // Stop following enemy
                        if ((distToEnemy > followDistance) && (distToEnemy < startFollowDistance)){
                                transform.position = this.transform.position;
                                anim.SetBool("Walk", false);
                        }

                        // Follow enemy
                        else if ((distToEnemy >= startFollowDistance)){
                                transform.position = Vector2.MoveTowards (transform.position, enemyPos, moveSpeed * Time.deltaTime);
                                anim.SetBool("Walk", true);
                        }

                        // Turn buddy toward enemy
                        if (enemyTarget.transform.position.x > gameObject.transform.position.x){
                                gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                        } else {
                                gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                        }
                }
                */

		//Timer for shooting projectiles
                /*
                if ((attackEnemy==true)&&(enemyTarget !=null) && (distToEnemy > followDistance) && (distToEnemy < startFollowDistance)){
                        if (timeBtwShots <= 0) {
                                isAttacking = true;
                                anim.SetBool("Attack", true);

                                GameObject myProjectile = Instantiate (projectile, transform.position, Quaternion.identity);
                                myProjectile.GetComponent ().attackPlayer = false;
                                myProjectile.GetComponent ().enemyTrans = enemyTarget.transform;

                                timeBtwShots = startTimeBtwShots;
                        } else {
                                timeBtwShots -= Time.deltaTime;
                                isAttacking = false;
                                anim.SetBool("Attack", true);
                        }
                }
                */

		//end bracket of FixedUpdate:
	}


	/*
	void FindTheEnemy(){
                //animator.SetTrigger ("Melee");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerPos, attackRange, enemyLayers);

                foreach(Collider2D enemy in hitEnemies){
                        Debug.Log("Buddy targeting " + enemy.name);
                        enemyTarget = enemy.gameObject;
                        //enemy.GetComponent ().TakeDamage(attackDamage);
                }
        }
	*/

	/*
	IEnumerator StopAttackingEnemies(){
                yield return new WaitForSeconds(peacefullTime);
                followPlayer = true;
                attackEnemy = false;
	}
	*/

        // DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, followDistance);
	}

} 