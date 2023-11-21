using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
       private Renderer rend;
       //public Animator anim;
	   //public AudioSource deathSound;
       public GameObject seedLoot1;
	   public GameObject seedLoot2;
	   private GameObject theLoot;
       public int maxHealth = 9;
       public int currentHealth;

       void Start(){
              rend = GetComponentInChildren<Renderer> ();
              //anim = GetComponentInChildren<Animator> ();
              currentHealth = maxHealth;
       }

       public void TakeDamage(int damage){
              currentHealth -= damage;
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
              //StartCoroutine(ResetColor());
              //anim.SetTrigger ("Hurt");
              if (currentHealth <= 0){
                     Die();
              }
       }

       void Die(){
		   int lootRand = Random.Range(0,2);
		   if (lootRand == 0){theLoot = seedLoot1;}
		   else {theLoot = seedLoot2;}
		   
              Instantiate (theLoot, transform.position, Quaternion.identity);
              //anim.SetBool ("isDead", true);
              GetComponent<Collider2D>().enabled = false;
              StartCoroutine(Death());
       }

       IEnumerator Death(){
		   //deathSound.Play();
              yield return new WaitForSeconds(0.3f);
              Debug.Log("You Killed a baddie. You deserve loot!");
              Destroy(gameObject);
       }

       IEnumerator ResetColor(){
              yield return new WaitForSeconds(0.3f);
              rend.material.color = Color.white;
       }
}