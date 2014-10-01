using UnityEngine;
using System.Collections;
using Char.Controller;

public class PlayerController : MonoBehaviour {
	private CharController charController;
	public float horizontalSpeed;

	// Use this for initialization
	void Awake () {
		charController = GetComponent<CharController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnJumpDown() {
		if(charController.IsGrounded()) {
			charController.Jump();
		}
	}

	void OnLeft() {
		charController.SetHorizontalSpeed(-horizontalSpeed);
	}
	void OnRight() {
		charController.SetHorizontalSpeed(horizontalSpeed);
	}

	void OnCenter() {
		if(charController.IsGrounded()) {
			charController.SetHorizontalSpeed(0);
		}
	}
}
