using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGSoundScript : MonoBehaviour {

	private static BGSoundScript instance = null;

	public string thisSceneName;
	Scene thisScene;

	void FixedUpdate(){
		thisScene = SceneManager.GetActiveScene();
		thisSceneName = thisScene.name;
		
		if ((thisSceneName == "MainMenu")
			|| (thisSceneName == "EndWon")
			|| (thisSceneName == "EndLose")
			|| (thisSceneName == "Credits")
			|| (thisSceneName == "LevelComic")){
			GetComponent<AudioSource>().UnPause();
			
			if (!GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public static BGSoundScript Instance{
		get {return instance;}
	}

        void Awake(){
                if (instance != null && instance != this){
                        Destroy(this.gameObject);
                        return;
                } else {
                        instance = this;
                }
                DontDestroyOnLoad(this.gameObject);
        }
} 