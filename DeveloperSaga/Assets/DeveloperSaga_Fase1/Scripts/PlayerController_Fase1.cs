using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerController_Fase1: MonoBehaviour {
	private PlayerController playerController;
	public float horizontalSpeed;

	// Use this for initialization
	void Awake () {
		playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnJumpDown() {
		if(playerController.IsGrounded()) {
			playerController.Jump();
		}
	}

	void OnLeft() {
		playerController.SetHorizontalSpeed(-horizontalSpeed);
	}
	void OnRight() {
		playerController.SetHorizontalSpeed(horizontalSpeed);
	}

	void OnCenter() {
		if(playerController.IsGrounded()) {
			playerController.SetHorizontalSpeed(0);
		}
	}
}
