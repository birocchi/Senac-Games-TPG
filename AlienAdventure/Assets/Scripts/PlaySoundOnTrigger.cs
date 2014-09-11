using UnityEngine;
using System.Collections;

public class PlaySoundOnTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		audio.Play();
	}
}
