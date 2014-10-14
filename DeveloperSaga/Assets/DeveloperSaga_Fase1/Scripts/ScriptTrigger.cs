using UnityEngine;
using System.Collections;

public class ScriptTrigger : MonoBehaviour {
	private PlayerController_Fase1 playerController;
	
	void Start () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Fase1>();
	}

	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player")  ) {
			this.ApplyScript ();
		}
	}
	
	void ApplyScript() {
		playerController.jumpAllowed = true;
	}
}