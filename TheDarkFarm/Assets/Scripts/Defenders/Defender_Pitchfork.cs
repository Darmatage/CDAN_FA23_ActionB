using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_Pitchfork : MonoBehaviour{
	
	public GameObject enemyDeathSmoke;
	//public AudioSource monsterDeathSFX;
	
	//public GameObject breakPitchforkVFX;
	//public AudioSource breakPitchforkSFX;
	
	public int hitsBeforeBreaking = 3;

	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy"){
			Debug.Log("I forked an enemy!");
			//monsterDeathSFX.Play();
			GameObject smokeVFX = Instantiate(enemyDeathSmoke, other.transform.position, Quaternion.identity);
			StartCoroutine(DelayDestroySmoke(smokeVFX));
			//Destroy(other.gameObject);
			other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(100);
			hitsBeforeBreaking--;
			if (hitsBeforeBreaking <= 0){
				BreakPitchfork();
			}
		}
	}
	
	IEnumerator DelayDestroySmoke(GameObject smokeVFX){
		yield return new WaitForSeconds(1f);
		Destroy(smokeVFX);
	}
	
	public void BreakPitchfork(){
		GetComponent<Collider2D>().enabled=false;
		StartCoroutine(DelayDestroySelf());
		//turn off art
		//GameObject breakVFX = Instantiate(breakPitchforkVFX, transform.position, Quaternion.identity);
		//breakPitchforkSFX.Play();
		//StartCoroutine(DelayDestroySelf(breakVFX));
		
	}
	
	IEnumerator DelayDestroySelf(){
	//IEnumerator DelayDestroySelf(GameObject breakVFX){
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
	
	
}
