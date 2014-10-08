using UnityEngine;
using System.Collections;

public class GetCoin : MonoBehaviour {
	
	private ScoreManager scoreManager;

	void Start(){
		scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			scoreManager.AddScore(10);
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			Destroy(gameObject);
		}
	}
}