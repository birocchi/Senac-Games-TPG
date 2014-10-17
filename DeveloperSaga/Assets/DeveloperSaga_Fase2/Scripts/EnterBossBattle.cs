using UnityEngine;
using System.Collections;

public class EnterBossBattle : MonoBehaviour {

	public GameObject lasers;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player")){
			SongController.songToPlay = 1;
			lasers.SetActive(true);
			Destroy(gameObject);
		}
	}
}
