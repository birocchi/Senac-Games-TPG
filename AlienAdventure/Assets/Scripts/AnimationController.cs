using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	PlayerController playerController;
	Animator animator;
	
	void Awake () {
		//Set up references
		playerController = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
	}

	void Update () {
		animator.SetFloat("Speed", Mathf.Abs(playerController.rigidbody2D.velocity.x));
		if(!playerController.isGrounded){
			animator.SetBool("Jump",true);
		}else{
			animator.SetBool("Jump",false);
		}
		if(playerController.isHurt){
			animator.SetBool("Hurt",true);
		}else{
			animator.SetBool("Hurt",false);
		}
	}
}
