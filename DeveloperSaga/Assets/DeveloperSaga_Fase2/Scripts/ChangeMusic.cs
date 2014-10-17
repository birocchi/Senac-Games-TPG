using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		SongController.songToPlay = 1;
	}
}
