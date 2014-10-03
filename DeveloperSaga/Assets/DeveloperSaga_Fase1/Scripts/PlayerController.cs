using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Enables the events:
/// OnfFire1Down
/// OnJumpDown
/// OnJump
/// OnLeft
/// OnRight
/// OnCenter
/// </summary>
public class PlayerController: MonoBehaviour
{
	public float jumpBaseIntensity = 7f;

	private float bottomY;

	void Awake() {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			this.BroadcastMessage("OnFire1Down", SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetButtonDown("Jump")) {
			this.BroadcastMessage("OnJumpDown", SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetButton("Jump")) {
			this.BroadcastMessage("OnJump", SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetAxis("Horizontal") < 0) {
			this.BroadcastMessage("OnLeft", Input.GetAxis("Horizontal"), SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetAxis("Horizontal") > 0) {
			this.BroadcastMessage("OnRight", Input.GetAxis("Horizontal"), SendMessageOptions.DontRequireReceiver);
		}
		if(Input.GetAxis("Horizontal") == 0) {
			this.BroadcastMessage("OnCenter", Input.GetAxis("Horizontal"), SendMessageOptions.DontRequireReceiver);
		}
	}
	
	/// <summary>
	/// Jump the specified intensityMultiplier.
	/// </summary>
	/// <param name="intensityMultiplier">Intensity multiplier.</param>
	public void Jump(float intensityMultiplier) {
		this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, intensityMultiplier * jumpBaseIntensity);
	}
	
	/// <summary>
	/// Jump
	/// </summary>
	public void Jump() {
		Jump (1);
	}
	
	/// <summary>
	/// Walk to the left.
	/// </summary>
	public void WalkLeft() {
		SetHorizontalSpeed(-1f);
	}
	
	/// <summary>
	/// Walk to the right.
	/// </summary>
	public void WalkRight() {
		SetHorizontalSpeed(1f);
	}
	
	/// <summary>
	/// Walk at the specified velocity.
	/// </summary>
	/// <param name="velocity">Velocity.</param>
	public void SetHorizontalSpeed(float velocity) {
		this.rigidbody2D.velocity = new Vector2(velocity, this.rigidbody2D.velocity.y);
}
	
	public bool IsGrounded() {
		BoxCollider2D boxCollider2D = (BoxCollider2D) this.collider2D;
		RaycastHit2D hit = Physics2D.Raycast((new Vector2(this.transform.position.x + boxCollider2D.center.x - boxCollider2D.size.x * this.transform.localScale.x / 2, 
		                                                  this.transform.position.y + boxCollider2D.center.y - boxCollider2D.size.y * this.transform.localScale.y / 2 - 0.025f)), 
		                                     Vector2.right, boxCollider2D.size.x * this.transform.localScale.x, 1 << 8); 
		
		Boolean isGrounded = hit.collider != null;
		return isGrounded;
	}
}