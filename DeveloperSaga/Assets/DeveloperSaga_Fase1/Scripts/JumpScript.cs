using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

	PlayerController_Fase1 playerController;
	public bool jumpPressed;

	void Start (){
		playerController = GetComponent<PlayerController_Fase1>();
	}

	// Update is called once per frame
	void Update () {
		//Jump if is grounded
		if(Input.GetButtonDown("Jump") && playerController.isGrounded ){
			rigidbody2D.velocity = new Vector2(playerController.horizontalMove * playerController.speed, Mathf.Abs(playerController.jumpForce));
			jumpPressed = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "MovingPlatform" && other.contacts[0].normal.y >= 0.9){
			jumpPressed = false;
		}
	}
}
