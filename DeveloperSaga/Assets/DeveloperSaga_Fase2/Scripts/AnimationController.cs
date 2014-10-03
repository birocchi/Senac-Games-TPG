using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	PlayerController_Fase2 playerController;
	Animator animator;
	
	void Awake () {
		//Set up references
		playerController = GetComponent<PlayerController_Fase2>();
		animator = GetComponent<Animator>();
	}

	void Update () {
		animator.SetFloat("Speed", Mathf.Abs(playerController.rigidbody2D.velocity.x));
		animator.SetFloat("HorizontalMove", Mathf.Abs(playerController.horizontalMove));
		animator.SetBool("Jump", playerController.isGrounded ? false : true);
		animator.SetBool("Hurt", playerController.isHurt ? true : false);
		animator.SetBool("OnMovingPlatform", playerController.movingPlatform != null ? true : false);
	}
}
