using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {
	private Animator animator;
	private PlayerController_Fase1 playerController;

	// Use this for initialization
	void Awake () {
		animator = this.GetComponent<Animator>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Fase1>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("VelocityX", this.rigidbody2D.velocity.x);
		animator.SetFloat ("VelocityY", this.rigidbody2D.velocity.y);
		animator.SetBool("OnMovingPlatform", playerController.movingPlatform != null);

		if (this.rigidbody2D.velocity.x > 0) {
			this.transform.localScale = new Vector3 (1, 1, 1);
		}
		if (this.rigidbody2D.velocity.x < 0) {
			this.transform.localScale = new Vector3 (-1, 1, 1);
		}

	}
}
