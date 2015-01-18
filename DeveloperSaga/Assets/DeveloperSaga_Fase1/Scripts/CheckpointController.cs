using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other != null && other.gameObject.tag.Equals("Player")){
			GameObject.Find("GameManager").GetComponent<CheckpointManager>().SaveState();
		}
	}
}
