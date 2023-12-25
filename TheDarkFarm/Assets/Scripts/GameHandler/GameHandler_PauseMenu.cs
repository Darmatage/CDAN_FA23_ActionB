using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameHandler_PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
	public GameObject hintsMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 0.5f;
    private Slider sliderVolumeCtrl;

    void Awake()
    {
        SetLevel(volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start(){
        pauseMenuUI.SetActive(false);
		hintsMenuUI.SetActive(false);
        GameisPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
		hintsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameisPaused = true;
		mixer.SetFloat("MusicVolume", -80f);
    }

    public void Resume(){
		hintsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
		SetLevel(volumeLevel);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        volumeLevel = sliderValue;
    }
	
	public void PauseButton(){
		if (GameisPaused){Resume();}
		else{Pause();}
	}
	
	public void ShowHintsButton(){
		hintsMenuUI.SetActive(true);
	}
	
}