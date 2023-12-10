using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour{

	private Animator anim;
	private Rigidbody2D rb2D;
	private CameraShake cameraShake;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		rb2D = transform.GetComponent<Rigidbody2D>();   

		cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
	}

	public void playerHit(){
		anim.SetTrigger ("Hurt");
		cameraShake.ShakeCamera(0.12f, 0.2f);
	}

	public void playerDead(){
		rb2D.isKinematic = true;
		anim.SetTrigger ("Dead");
	}

}
