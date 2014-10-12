using UnityEngine;
using System.Collections;

public class GetPlant : MonoBehaviour {

	private LifeManager lifeManager;
	
	// Use this for initialization
	void Start () {
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			lifeManager.FillLife(LifeManager.LifeType.Player);
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			Destroy(gameObject);
		}
	}
}
