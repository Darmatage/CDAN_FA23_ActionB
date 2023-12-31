using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_HayBaleOnFire : MonoBehaviour{
	
	public GameObject HayBale_normal;
	public GameObject HayBale_onfire;
	public GameObject HayBale_ashes;
	
	public GameObject pillarSmokeVFX;
	GameObject smokeVFX;
	public GameObject enemyDeathSmoke;
	
	bool isOnFire = false;
	bool isAshes = false;
	
	//night tracking:
	public int nightCount = 0; // after second night, extinguish
	bool canSetFirstNight = true;
	bool canSetSecondNight = false;
	
	//public GameObject enemyDeathSmokeVFX;
	//public AudioSource monsterDeathSFX;
	//public AudioSource burningHayBaleSFX;
	//public AudioSource extinguishedHayBaleSFX;
	
	//public float timeBeforeAshes = 30f;
	//public float burnCounter = 0f;

	void Start(){
		//set normal hay to visible at start, others invisible
		HayBale_normal.SetActive(true);
		HayBale_onfire.SetActive(false);
		HayBale_ashes.SetActive(false);
	}

	void FixedUpdate(){
		//the first night the hay bale ignites! Lasts two nights:
		if ((GameHandler_DayNightPhases.isDayPhase == false)&&(isAshes==false)){
			HayBale_normal.SetActive(false);
			HayBale_onfire.SetActive(true);
			isOnFire = true;
			
			if ((nightCount ==0)&&(canSetFirstNight)){
				canSetFirstNight = false; //so VFX is only created once
				nightCount=1;
				Vector3 smokeRotation = new Vector3(90,0,0);
				//smokeVFX = Instantiate(pillarSmokeVFX, transform.position, Quaternion.identity);
				smokeVFX = Instantiate(pillarSmokeVFX, transform.position, Quaternion.Euler(-90, 0, 0));
			}
			else if ((nightCount ==1)&&(canSetSecondNight)){nightCount=2;}
			//else {Debug.Log("unexpected hay bale condition");}
			
		}
		
		//the first day after first night, enable second night. The day after second night, extinguish
		if (GameHandler_DayNightPhases.isDayPhase == true){
			if (nightCount==1) {
				canSetSecondNight = true;
			}
			else if ((nightCount==2)&&(canSetSecondNight)){
				canSetSecondNight=false; //so coroutine only called once
				StartCoroutine(ExtinguishHayBale());
			}
		}
	}

	//destroy enemies on contact if on fire
	public void OnTriggerEnter2D(Collider2D other){
		if (isOnFire == true){
			if ((other.gameObject.tag == "Enemy")||(other.gameObject.tag == "EnemyGoo")){
				GameObject smokeVFX = Instantiate(enemyDeathSmoke, other.transform.position, Quaternion.identity);
				StartCoroutine(DelayDestroySmoke(smokeVFX));
				other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(100);
				Debug.Log("I burned an enemy!");
				//monsterDeathSFX.Play();
				//Destroy(other.gameObject);
			}
		}
	}
	
	//delay to destroy enemy dissolve smoke
	IEnumerator DelayDestroySmoke(GameObject smokeVFX){
		yield return new WaitForSeconds(1f);
		Destroy(smokeVFX);
	}
	
	//turn hay into ashes, remove collider, remove piller of smoke
	IEnumerator ExtinguishHayBale(){
		yield return new WaitForSeconds(1f);
		isOnFire = false;
		isAshes = true;
		GetComponent<Collider2D>().enabled=false;
		//extinguishedHayBaleSFX.Play();
		//burningHayBaleSFX.Stop();
		HayBale_normal.SetActive(false);
		HayBale_onfire.SetActive(false);
		HayBale_ashes.SetActive(true);
		
		//delay to destroy extingished hay smoke
		yield return new WaitForSeconds(5f);
		Destroy(smokeVFX);
	}	
}
