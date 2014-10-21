using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {

	private Animator animator;
	private PlayerController_Fase1 playerController;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		playerController = GetComponent<PlayerController_Fase1>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("VelocityX", Mathf.Abs(rigidbody2D.velocity.x));
		animator.SetFloat ("VelocityY", Mathf.Abs(rigidbody2D.velocity.y));
		animator.SetFloat("HorizontalMove", Mathf.Abs(playerController.horizontalMove));
		animator.SetBool("OnMovingPlatform", playerController.movingPlatform != null);
		animator.SetBool("isGrounded", playerController.isGrounded);
	}
}
