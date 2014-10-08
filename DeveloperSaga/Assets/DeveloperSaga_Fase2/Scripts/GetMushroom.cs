using UnityEngine;
using System.Collections;

public class GetMushroom : MonoBehaviour {

	public int healthRecover = 1;

	private LifeManager lifeManager;

	// Use this for initialization
	void Start () {
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			lifeManager.LifeUp(healthRecover);
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			Destroy(gameObject);
		}
	}
}
