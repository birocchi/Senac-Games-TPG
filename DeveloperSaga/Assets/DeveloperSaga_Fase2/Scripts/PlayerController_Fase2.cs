using UnityEngine;
using System.Collections;

public class PlayerController_Fase2 : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public AudioSource ouchSound;

	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isHurt;

	float horizontalMove;
	LifeManager lifeManager;
	Transform groundCheck;
	
	void Awake () {
		//Set up references
		groundCheck = transform.FindChild("GroundCheck");
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		isGrounded = true;
		isHurt = false;
	}

	void Update (){
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Steppable"));
		Debug.DrawLine(transform.position, groundCheck.position);

		//Jump if is grounded
		if(Input.GetButtonDown("Jump") && isGrounded ){
			rigidbody2D.velocity = new Vector2(horizontalMove * speed, Mathf.Abs(jumpForce));
			audio.Play();
		}
	}
	
	void FixedUpdate () {
		//Get the player Input
		horizontalMove = Input.GetAxis("Horizontal");

		//Rotate the player acording to its movement direction
		if(horizontalMove < 0){
			transform.eulerAngles = new Vector3(0,180,0);
		} else if (horizontalMove > 0){
			transform.eulerAngles = new Vector3(0,0,0);
		}

		if(!isHurt){
			//Move the player
			rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);
		}
	}

	public void HurtPlayer(int damage){
		if(!isHurt){
			lifeManager.LifeDown(damage);
			ouchSound.Play();
			StartCoroutine(StunPlayer(0.8f));
		}
	}

	IEnumerator StunPlayer(float stunTime){
		isHurt = true;
		yield return new WaitForSeconds(stunTime);
		isHurt = false;
	}
}
