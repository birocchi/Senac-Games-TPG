using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {
	public bool enabled;
	PlayerController_Fase1 playerController;
	void Awake () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Fase1>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other != null && other.tag.Equals ("Player")  ) {
			playerController.latestCheckpoint = this.transform.position;
		}
	}
}