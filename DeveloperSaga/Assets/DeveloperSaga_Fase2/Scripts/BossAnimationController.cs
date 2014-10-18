using UnityEngine;
using System.Collections;

public class BossAnimationController : MonoBehaviour {
	
	BossController bossController;
	Animator animator;
	
	void Awake () {
		//Set up references
		bossController = GetComponent<BossController>();
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		animator.SetFloat("Speed", Mathf.Abs(bossController.rigidbody2D.velocity.x));
		animator.SetBool("Jump", bossController.isGrounded ? false : true);
		animator.SetBool("Hurt", bossController.isHurt ? true : false);
		animator.SetFloat("Shooting", bossController.isShooting ? 1 : 0);
	}
}
