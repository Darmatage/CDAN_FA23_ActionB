using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler_DayNightPhases : MonoBehaviour{
	
	//night overlay colors
	public GameObject nightOverlay;
	private Color nightColor;
	float Nr, Ng, Nb, Na;
	
	//day and night phase timing
	public bool isDayPhase = true;
	public bool isTwilight = false;
	public GameObject timeText;
	private float timerDisplay;
	public float timeDayLength = 10f;
	public float timeNightLength = 10f;
	private float theDayTimer = 0;
	private float theNightTimer = 0;
	private float displayCount = 0;

	//enemy spawning
	public GameObject Enemy1;
	public Transform[] monsterSpawns;
	float spawnTimer = 0f;
	float spawnLimit = 1f;
	public GameObject PS_MonsterSmoke;	

    void Start(){
        nightOverlay.SetActive(false);
		nightColor = nightOverlay.GetComponent<Image>().color;
		Nr = nightColor.r;
		Ng = nightColor.g;
		Nb = nightColor.b;
		Na = nightColor.a;
		
		//timerDisplay = (int)(timeDayLength - theDayTimer);
		timerDisplay = timeDayLength - theDayTimer;
		updateTimeText();
    }

    void FixedUpdate(){
		//Day / Night Timers:
		if (!isTwilight){
			if (isDayPhase){
				theDayTimer += 0.01f;
				timerDisplay = timeDayLength - theDayTimer;
				if (theDayTimer >= timeDayLength){
					isDayPhase = false;
					theDayTimer = 0f;
					SwitchToNight(); // call switch to night function
				}
			} else {
				//display countdown:
				theNightTimer += 0.01f;
				timerDisplay = timeNightLength - theNightTimer;
				if (theNightTimer >= timeNightLength){
					isDayPhase = true;
					theNightTimer = 0f;
					SwitchToDay(); // call switch to day function
				}
			}
			//set timer to only show up to second decimal 
			timerDisplay = Mathf.Round(timerDisplay * 10f) / 10f; 
			updateTimeText();
		}
		
		//Spawn enemies from spawn points, at spawn limit interval
		if (!isDayPhase){
			spawnTimer += 0.01f;
			if (spawnTimer >= spawnLimit){
				spawnTimer = 0;
				int randSpawn = Random.Range(0, monsterSpawns.Length);
				GameObject newEnemy = Instantiate(Enemy1, monsterSpawns[randSpawn].position, Quaternion.identity);
			}		
		}
		
    }
	
	//all night functionality starts, day functionality ends
	public void SwitchToNight(){
		nightOverlay.SetActive(true);
		StartCoroutine(FadeIn(nightOverlay));
	}
	
	//all day functionality starts, night functinality ends
	public void SwitchToDay(){
		StartCoroutine(FadeOut(nightOverlay));
	}
	
	//display timer on screen
	public void updateTimeText(){
		Text timeTextTemp = timeText.GetComponent<Text>();
		if (isDayPhase){timeTextTemp.text = "TIME TO SUNSET: " + timerDisplay;}
		else {timeTextTemp.text = "TIME TO SUNRISE: " + timerDisplay;}
	}
	
	//fade effects
	IEnumerator FadeIn(GameObject fadeImage){
		isTwilight = true;
		float alphaLevel = 0;
		fadeImage.GetComponent<Image>().color = new Color(Nr, Ng, Nb, 0);
		for(int i = 0; i < 200; i++){
			alphaLevel += 0.01f;
			yield return null;
			if (alphaLevel <= Na){ //set upper limit to alpha
				fadeImage.GetComponent<Image>().color = new Color(Nr, Ng, Nb, alphaLevel);
				//Debug.Log("Alpha is: " + alphaLevel);
			}
		}
		isTwilight = false;
	}

	IEnumerator FadeOut(GameObject fadeImage){
		isTwilight = true;
		float alphaLevel = 1;
		fadeImage.GetComponent<Image>().color = new Color(Nr, Ng, Nb, Na);
		for(int i = 0; i < 200; i++){
			alphaLevel -= 0.01f;
			yield return null;
			fadeImage.GetComponent<Image>().color = new Color(Nr, Ng, Nb, alphaLevel);
			//Debug.Log("Alpha is: " + alphaLevel);
		}
		
		//create a list of enemies, so they can be deleted
		GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject monster in allMonsters){
			yield return new WaitForSeconds(0.1f);
			GameObject PS_monSmoke = Instantiate(PS_MonsterSmoke, monster.transform.position, Quaternion.identity);
			Destroy(monster);
			StartCoroutine(DestroySmoke(PS_monSmoke));
		}
		
		//nightOverlay.SetActive(false);
		isTwilight = false;
	} 
	
	IEnumerator DestroySmoke(GameObject newSmoke){
		yield return new WaitForSeconds(3f);
		Destroy(newSmoke);
	}
	
}
