using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTeleporter_Enemy : MonoBehaviour{
	public Transform waitingAreaPoint;
	public Transform houseEntryPoint;
	public GameObject portalVFX_in;
	public GameObject portalVFX_out;
	
	private float portal_X = 0;
	private float portal_Y = 0;
	private Vector3 scaleZero;
	
	public AudioSource monsterPortal_SFX;
	
	void Start(){
		portalVFX_in.SetActive(false);
		portalVFX_out.SetActive(false);
		portal_X = portalVFX_in.transform.localScale.x;
		portal_Y = portalVFX_in.transform.localScale.y;
		scaleZero = new Vector3(0,0,0);
	}
	
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy"){
			//teleport to other front door
			other.gameObject.transform.position = waitingAreaPoint.position;
			StartCoroutine(portalManager(other.gameObject));
		}	
	}
	
	IEnumerator portalManager(GameObject monster){
		//set portals to 0 scale, and reveal:
		portalVFX_in.transform.localScale = scaleZero;
		portalVFX_out.transform.localScale = scaleZero;
		portalVFX_in.SetActive(true);
		portalVFX_out.SetActive(true);
		
		monsterPortal_SFX.Play();
		
		//set scale values, and activate scale
		Vector3 scaleFrom = scaleZero;
		Vector3 scaleTo = new Vector3(portal_X, portal_Y, 1f);
		StartCoroutine(ScaleOverSeconds(portalVFX_in, scaleFrom, scaleTo, 1.0f));
		yield return new WaitForSeconds(0.75f);
		StartCoroutine(ScaleOverSeconds(portalVFX_in, scaleTo, scaleFrom, 0.5f));
		
		StartCoroutine(ScaleOverSeconds(portalVFX_out, scaleFrom, scaleTo, 1.0f));
		monster.transform.position = houseEntryPoint.position;
		monster.GetComponent<EnemyMoveHit>().goInside();
		yield return new WaitForSeconds(0.75f);
		StartCoroutine(ScaleOverSeconds(portalVFX_out, scaleTo, scaleFrom, 0.5f));
		portalVFX_in.SetActive(false);
		portalVFX_out.SetActive(false);
	}
	
	
	public  IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleFrom, Vector3 scaleTo, float seconds){
		float elapsedTime = 0;
		//Vector3 startingScale = objectToScale.transform.localScale;
		while (elapsedTime < seconds){
			objectToScale.transform.localScale = Vector3.Lerp(scaleFrom, scaleTo, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		objectToScale.transform.localScale = scaleTo;
	}
	
	
	
	
	
}
