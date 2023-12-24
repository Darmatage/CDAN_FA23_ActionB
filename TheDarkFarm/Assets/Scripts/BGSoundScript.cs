using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGSoundScript : MonoBehaviour {

	private static BGSoundScript instance = null;

	string thisSceneName;
	Scene thisScene;

	void Start(){
		thisScene = SceneManager.GetActiveScene();
		thisSceneName = thisScene.name;
		
		if ((thisSceneName == "Mainmenu")
			|| (thisSceneName == "EndWon")
			|| (thisSceneName == "EndLose")
			|| (thisSceneName == "Credits")
			|| (thisSceneName == "LevelComic")){
			GetComponent<AudioSource>().UnPause();
			GetComponent<AudioSource>().Play();
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