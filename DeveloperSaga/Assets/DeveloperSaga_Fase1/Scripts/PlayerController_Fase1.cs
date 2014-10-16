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
public class PlayerController_Fase1: MonoBehaviour
{
	public bool jumpAllowed = false;
	public float jumpBaseIntensity = 7f;
	public float horizontalSpeed;
	private float bottomY;
	public GameObject corpse;
	public Vector3 latestCheckpoint;
	private bool restartLevel;
	private LifeManager lifeManager;

	void Awake() {
		GameObject gameManager = GameObject.Find ("GameManager");
		lifeManager = gameManager.GetComponent<LifeManager> ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")) {
			Jump();
		}
		if(Input.GetAxis("Horizontal") < 0) {
			WalkLeft();
		}
		if(Input.GetAxis("Horizontal") > 0) {
			WalkRight();
		}
		if(Input.GetAxis("Horizontal") == 0) {
			Stand();
		}

		if(restartLevel) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
	
	/// <summary>
	/// Jump the specified intensityMultiplier.
	/// </summary>
	/// <param name="intensityMultiplier">Intensity multiplier.</param>
	public void Jump(float intensityMultiplier) {
		if(IsGrounded() && jumpAllowed) {
			this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, intensityMultiplier * jumpBaseIntensity);
		}
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
		SetHorizontalSpeed(-horizontalSpeed);
	}
	
	/// <summary>
	/// Walk to the right.
	/// </summary>
	public void WalkRight() {
		SetHorizontalSpeed(horizontalSpeed);
	}
	
	/// <summary>
	/// Walk at the specified velocity.
	/// </summary>
	/// <param name="velocity">Velocity.</param>
	public void SetHorizontalSpeed(float velocity) {
		this.rigidbody2D.velocity = new Vector2(velocity, this.rigidbody2D.velocity.y);
	}

	void Stand() {
		if(IsGrounded()) {
			SetHorizontalSpeed(0);
		}
	}
	
	public bool IsGrounded() {
		BoxCollider2D boxCollider2D = (BoxCollider2D) this.collider2D;
		RaycastHit2D hit = Physics2D.Raycast((new Vector2(this.transform.position.x + boxCollider2D.center.x - boxCollider2D.size.x * this.transform.localScale.x / 2, 
		                                                  this.transform.position.y + boxCollider2D.center.y - boxCollider2D.size.y * this.transform.localScale.y / 2 - 0.025f)), 
		                                     Vector2.right, boxCollider2D.size.x * this.transform.localScale.x, 1 << 8); 
		
		Boolean isGrounded = hit.collider != null;
		return isGrounded;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag.Equals("Enemy")) {
			DoDamage(1);
		}
	}

	private void Die() {
		if(corpse != null) {
			Instantiate(corpse, this.transform.position, this.transform.rotation);
			this.renderer.enabled = false;
			this.rigidbody2D.isKinematic = true;
			if(latestCheckpoint != null) {
				StartCoroutine(WaitUp());
				//this.rigidbody2D.position = latestCheckpoint;
			}
		}
	}

	private void DoDamage (int damage)
	{
		if(lifeManager.PlayerLife == 1) {
			Die();
		} else {
			lifeManager.LifeDown (damage, LifeManager.LifeType.Player);
		}
	}

	IEnumerator WaitUp() {
		yield return new WaitForSeconds(1.5f);
		this.restartLevel = true;
	}
}