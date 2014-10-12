using UnityEngine;
using System.Collections;

public class PlayerController_Fase2 : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public AudioSource ouchSound;

	//Used by the AnimationController
	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isHurt;
	[HideInInspector]
	public Rigidbody2D movingPlatform;
	[HideInInspector]
	public float horizontalMove;

	private LifeManager lifeManager;
	private Transform groundCheck;
	private bool jumpPressed;

	
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
			jumpPressed = true;
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
			if(movingPlatform != null){
				float speedSwitch = jumpPressed ? rigidbody2D.velocity.y : movingPlatform.velocity.y;
				rigidbody2D.velocity = new Vector2((horizontalMove * speed) + movingPlatform.velocity.x, speedSwitch);
			} else{
				//Move the player
				rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);
			}

		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "MovingPlatform" && other.contacts[0].normal.y >= 0.9){
			movingPlatform = other.rigidbody;
			jumpPressed = false;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.tag == "MovingPlatform" && other.contacts[0].normal.y < 0.9){
			movingPlatform = null;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "MovingPlatform"){
			movingPlatform = null;
		}
	}

	public void HurtPlayer(int damage){
		if(!isHurt){
			lifeManager.LifeDown(damage,LifeManager.LifeType.Player);
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
