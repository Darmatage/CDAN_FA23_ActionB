using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_HayBaleOnFire : MonoBehaviour{
	
	public GameObject enemyDeathSmokeVFX;
	public GameObject HayBale_normal;
	public GameObject HayBale_onfire;
	public GameObject HayBale_ashes;
	
	
	//public AudioSource monsterDeathSFX;
	//public AudioSource burningHayBaleSFX;
	//public AudioSource extinguishedHayBaleSFX;
	public GameObject extinguishSmokeVFX;
	
	bool isOnFire = false;
	bool isAshes = false;
	public float timeBeforeAshes = 30f;
	public float burnCounter = 0f;

	void Start(){
		//set normal hay to visible at start, other invisible
		HayBale_normal.SetActive(true);
		HayBale_onfire.SetActive(false);
		HayBale_ashes.SetActive(false);
	}

	void FixedUpdate(){
		//the first time the haybale encounters night, ignite!
		if ((GameHandler_DayNightPhases.isDayPhase == false)&&(isAshes==false)){
			HayBale_normal.SetActive(false);
			HayBale_onfire.SetActive(true);
			isOnFire = true;
		}
		
		//once ignited, count down to extinguish
		if (isOnFire == true){
			burnCounter += 0.01f;
			if (burnCounter >= timeBeforeAshes){
				ExtinguishHayBale();
				isOnFire = false;
				isAshes = true;
			}
		}
	}

	//destroy enemies on contact if on fire
	public void OnTriggerEnter2D(Collider2D other){
		if ((other.gameObject.tag == "Enemy")&&(isOnFire == true)){
			Debug.Log("I burned an enemy!");
			//monsterDeathSFX.Play();
			GameObject smokeVFX = Instantiate(enemyDeathSmokeVFX, other.transform.position, Quaternion.identity);
			StartCoroutine(DelayDestroySmoke(smokeVFX));
			Destroy(other.gameObject);
		}
	}
	
	//delay to destroy enemy dissolve smoke
	IEnumerator DelayDestroySmoke(GameObject smokeVFX){
		yield return new WaitForSeconds(1f);
		Destroy(smokeVFX);
	}
	
	//turn hay into ashes, remove collider, spawn piller of smoke
	public void ExtinguishHayBale(){
		GetComponent<Collider2D>().enabled=false;
		//turn off art
		//GameObject breakVFX = Instantiate(breakPitchforkVFX, transform.position, Quaternion.identity);
		//extinguishedHayBaleSFX.Play();
		//burningHayBaleSFX.Stop();
		HayBale_normal.SetActive(false);
		HayBale_onfire.SetActive(false);
		HayBale_ashes.SetActive(true);
		GameObject extingishVFX = Instantiate(extinguishSmokeVFX, transform.position, Quaternion.identity);
		StartCoroutine(DelayExtinguishSmoke(extingishVFX));
	}
	
	//delay to destroy extingished hay smoke
	IEnumerator DelayExtinguishSmoke(GameObject smokeVFX){
		yield return new WaitForSeconds(5f);
		Destroy(smokeVFX);
	}
	
}
