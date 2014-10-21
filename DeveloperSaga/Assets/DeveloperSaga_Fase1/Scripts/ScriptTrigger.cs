using UnityEngine;
using System.Collections;

public class ScriptTrigger : MonoBehaviour {
	private PlayerController_Fase1 playerController;
	Animator animator;
	
	void Awake () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Fase1>();
		animator = this.GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other != null && other.tag.Equals ("Player")  ) {
			Destroy(this.collider2D);
			animator.SetBool("ScriptTaken", true);
			other.GetComponent<JumpScript>().enabled = true;
		}
	}

}