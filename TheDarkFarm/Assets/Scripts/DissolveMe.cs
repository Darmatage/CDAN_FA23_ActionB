using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveMe : MonoBehaviour{

	public float dissolveTime = 2f;

    void Start(){
        StartCoroutine(TimeToDissolve());
    }

    IEnumerator TimeToDissolve(){
        yield return new WaitForSeconds(dissolveTime);
		Destroy(gameObject);
    }
}
