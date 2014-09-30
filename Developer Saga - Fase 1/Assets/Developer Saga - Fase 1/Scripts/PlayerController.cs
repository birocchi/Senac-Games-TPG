using UnityEngine;
using System.Collections;
using Char.Controller;

public class PlayerController : MonoBehaviour {

	private CharController charController;

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
		charController.SetHorizontalSpeed(-5f);
	}
	void OnRight() {
		charController.SetHorizontalSpeed(5f);
	}

	void OnCenter() {
		if(charController.IsGrounded()) {
			charController.SetHorizontalSpeed(0);
		}
	}
}
