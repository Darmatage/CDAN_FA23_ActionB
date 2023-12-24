using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

	public static bool isInside = false;

	private GameObject player;
	public static int playerHealth = 100;
	public int StartPlayerHealth = 100;
	public GameObject healthText;
	  
	public static int artifactHealth = 100;
	public GameObject artifactText;
	  

	public static int gotTokens = 0; // use score for Day #
	public GameObject tokensText;

	public bool isDefending = false;

	public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

	private string sceneName;
	public static string lastLevelDied;  //allows replaying the Level where you died

	void Start(){
		player = GameObject.FindWithTag("Player");
		sceneName = SceneManager.GetActiveScene().name;
		//if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
		playerHealth = StartPlayerHealth;
		//}
		updateStatsDisplay();
	}

	// use score for Day #
	public void playerGetTokens(int newTokens){
		gotTokens += newTokens;
		updateStatsDisplay();
	}

	public void playerGetHit(int damage){
           if (isDefending == false){
                  playerHealth -= damage;
                  if (playerHealth >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
                  }
            }

           if (playerHealth > StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
                  updateStatsDisplay();
            }

           if (playerHealth <= 0){
                  playerHealth = 0;
                  updateStatsDisplay();
                  playerDies();
            }
	}

	public void updateStatsDisplay(){
		Text healthTextTemp = healthText.GetComponent<Text>();
		healthTextTemp.text = "HEALTH: " + playerHealth;

		// use score for Day #
		Text tokensTextTemp = tokensText.GetComponent<Text>();
		tokensTextTemp.text = "DAY: " + gotTokens;
			
		Text artifactTextTemp = artifactText.GetComponent<Text>();
		artifactTextTemp.text = "ARTIFACT: " + artifactHealth;
	}

	public void ArtifactDamage(int hits){
		artifactHealth -= hits;
		if (artifactHealth <= 0){
			artifactHealth = 0;
			StartCoroutine(DeathPause());
		}
		updateStatsDisplay();
	}

      public void playerDies(){
            player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            lastLevelDied = sceneName;       //allows replaying the Level where you died
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            //player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
			yield return new WaitForSeconds(1.0f);
			ResetAllInventory();
            SceneManager.LoadScene("EndLose");
      }

      public void StartGame() {
            SceneManager.LoadScene("LevelComic");
      }

	// Return to MainMenu from FrameScenes:
	public void RestartGame() {
		SceneManager.LoadScene("MainMenu");
	}
	  
	// Return to MainMenu from game pause menu
	public void RestartFromInGame() {
		ResetAllInventory();
		SceneManager.LoadScene("MainMenu");
	}
	  
	public void ResetAllInventory(){
		Debug.Log("reseting all stats...");
		//reset the static variables in each GameHandler script:
		Time.timeScale = 1f;
        GameHandler_PauseMenu.GameisPaused = false;
		GameHandler_DayNightPhases.roundNumber = 0;
		GameHandler_DayNightPhases.isDayPhase = true;
		playerHealth = StartPlayerHealth;
		artifactHealth = 100;
		
		//reset the inventory:
		Game_Inventory.item1bool = false;
		Game_Inventory.item2bool = false;
		Game_Inventory.item3bool = false;
		Game_Inventory.item4bool = false;
		Game_Inventory.item5bool = false;
		Game_Inventory.item6bool = false;
		Game_Inventory.item7bool = false;
		Game_Inventory.item8bool = false;
		Game_Inventory.item9bool = false;

		Game_Inventory.item1num = 0; // object name
		Game_Inventory.item2num = 0; // object name
		Game_Inventory.item3num = 0; // object name
		Game_Inventory.item4num = 0; // object name
		Game_Inventory.item5num = 0; // object name
		Game_Inventory.item6num = 0; // object name
		Game_Inventory.item7num = 0; // object name
		Game_Inventory.item8num = 0; // object name
		Game_Inventory.item9num = 0; // object name
		Debug.Log("All stats reset. roundNumber = " + GameHandler_DayNightPhases.roundNumber);
      }
	  
	  

      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}
