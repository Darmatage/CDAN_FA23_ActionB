using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler_DayNightPhases : MonoBehaviour{
	
	public static int roundNumber = 0;
	public GameObject roundText;
	
	//night overlay colors
	public GameObject nightOverlay;
	private Color nightColor;
	float Nr, Ng, Nb, Na;
	
	//day and night phase timing
	public static bool isDayPhase = true;
	public bool isTwilight = false;
	public GameObject timeText;
	private float timerDisplay;
	public float timeDayLength = 35f;
	public float timeNightLength = 20f;
	private float theDayTimer = 0;
	private float theNightTimer = 0;
	private float displayCount = 0;

	//enemy spawning
	public GameObject Enemy1;
	public Transform[] monsterSpawns;
	//public Transform[] monsterSpawnsCurrent;
	public List<Transform> monsterSpawnsCurrent = new List<Transform>();
	float spawnTimer = 0f;
	float spawnLimit = 5f;
	public GameObject PS_MonsterSmoke;	

	public GameObject[] farms;


    void Start(){
		roundText.SetActive(false);
        nightOverlay.SetActive(false);
		nightColor = nightOverlay.GetComponent<Image>().color;
		Nr = nightColor.r;
		Ng = nightColor.g;
		Nb = nightColor.b;
		Na = nightColor.a;
		
		//timerDisplay = (int)(timeDayLength - theDayTimer);
		timerDisplay = timeDayLength - theDayTimer;
		updateTimeText();
		
		//set initial farm displays
		farms[0].SetActive(true);
		for (int i = 1; i < farms.Length; i++){
			farms[i].SetActive(false);
		}
		
		//prime the first night for monsters spawns:
		monsterSpawnsCurrent.Add(monsterSpawns[0]);
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
				int randSpawn = Random.Range(0, monsterSpawnsCurrent.Count);
				GameObject newEnemy = Instantiate(Enemy1, monsterSpawnsCurrent[randSpawn].position, Quaternion.identity);
				Debug.Log("spawned enemy at location #" + monsterSpawnsCurrent[randSpawn] + ", " + monsterSpawnsCurrent[randSpawn].position);
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
		roundNumber += 1;
		spawnLimit -= (spawnLimit/10);
		
		//based on the day, display the correct ground...
		//hide all farms:
		for (int i = 0; i < farms.Length; i++){
			farms[i].SetActive(false);
		}
		//unhide currentfarm (using this order: 1,1,2,3,4,5,6,7,8,8 -- display #8 for all remaining):
		if (roundNumber <10){farms[roundNumber - 1].SetActive(true);} 
		else {farms[7].SetActive(true);}
		
		//decide the spawn rate:
		spawnLimit = (int)(10/roundNumber+1);
		
		//... and populate a new array with the correct # of spawn points
		monsterSpawnsCurrent.Clear();
		for (int m = 0; m < roundNumber; m++){
			monsterSpawnsCurrent.Add(monsterSpawns[m]);
		}
		
		//end game at round 11:
		if (roundNumber >= 11){
			SceneManager.LoadScene("EndWon");
		}
		
		Text roundTextTemp = roundText.GetComponent<Text>();
		roundTextTemp.text = "DAY #: " + roundNumber;
		
		StartCoroutine(FadeTextInAndOut());
	}
	
	//display timer on screen
	public void updateTimeText(){
		Text timeTextTemp = timeText.GetComponent<Text>();
		if (isDayPhase){timeTextTemp.text = "TIME TO SUNSET: " + timerDisplay;}
		else {timeTextTemp.text = "TIME TO SUNRISE: " + timerDisplay;}
	}
	
	IEnumerator FadeTextInAndOut(){
		StartCoroutine(FadeTextIn(roundText));
		yield return new WaitForSeconds(2f);
		StartCoroutine(FadeTextOut(roundText));
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
		yield return new WaitForSeconds(1f);
		GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject monster in allMonsters){
			yield return new WaitForSeconds(0.07f);
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
	
	
	
	IEnumerator FadeTextIn(GameObject fadeText){
		float alphaLevel = 0;
		fadeText.GetComponent<Text>().color = new Color(Nr, Ng, Nb, 0);
		roundText.SetActive(true);
		for(int i = 0; i < 200; i++){
			alphaLevel += 0.01f;
			yield return null;
			if (alphaLevel <= Na){ //set upper limit to alpha
				fadeText.GetComponent<Text>().color = new Color(Nr, Ng, Nb, alphaLevel);
			}
		}
	}

	IEnumerator FadeTextOut(GameObject fadeText){
		float alphaLevel = 1;
		fadeText.GetComponent<Text>().color = new Color(Nr, Ng, Nb, Na);
		for(int i = 0; i < 200; i++){
			alphaLevel -= 0.01f;
			yield return null;
			fadeText.GetComponent<Text>().color = new Color(Nr, Ng, Nb, alphaLevel);
		}
		roundText.SetActive(false);
	} 
	
	
	
	
}
