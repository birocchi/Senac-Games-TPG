using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	PlayerController_Fase1 playerController;

	void Start () {
		playerController = GameObject.Find("Player").GetComponent<PlayerController_Fase1>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other != null && other.tag.Equals ("Player")  ) {
			playerController.latestCheckpoint = this.transform.position;
		}
	}
}