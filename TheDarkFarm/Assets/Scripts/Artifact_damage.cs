using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact_damage : MonoBehaviour{
	
	public int damage = 5;
	private GameHandler gameHandler;
	private Color artifactColor;

    void Start(){
		if (GameObject.FindWithTag("GameHandler") != null)
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
	
		artifactColor = GetComponentInChildren<SpriteRenderer>().color;
    }


	//if a monsters collides with the artifact
    public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag== "Enemy"){
			StartCoroutine(FlashMe());
			gameHandler.ArtifactDamage(damage); //artifact loses health
			Destroy(other.gameObject); // destroy the monster (perhaps send message to monster?)
		}
    }
	
	
	IEnumerator FlashMe(){
		artifactColor = new Color(2f,1f,1f,1f); //red
		yield return new WaitForSeconds (0.5f);
		artifactColor = new Color(2.5f,2.5f,2.5f,1f);
		yield return new WaitForSeconds (0.5f);
		artifactColor = new Color(2f,1f,1f,1f);//red
		yield return new WaitForSeconds (0.5f);
		artifactColor = new Color(2.5f,2.5f,2.5f,1f);
	}
	
}
