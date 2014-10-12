using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	//Movement Variables
	public float speed;
	public float jumpForce;
	public float safeDistance;
	public float viewDistance;

	//Shot variables
	public GameObject shot;
	public float fireRate = 1;
	public float shotSpeed = 9;
	private float fireInterval;
	private float elapsedTime;
	private GameObject shotInstance;
	
	//Used by the AnimationController
	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isHurt;
	[HideInInspector]
	public Rigidbody2D movingPlatform;
	[HideInInspector]
	public float horizontalMove;

	//Other variables
	public Transform target;
	public AudioSource ouchSound;
	public GameObject explosion;
	private LifeManager lifeManager;
	private Transform groundCheck;
	
	void Awake () {
		//Set up references
		groundCheck = transform.FindChild("GroundCheck");
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		isGrounded = true;
		isHurt = false;
		fireInterval = 1f / fireRate;
		elapsedTime = fireInterval;
	}
	
	void Update (){
		// The boss is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Steppable"));
		Debug.DrawLine(transform.position, groundCheck.position);

		//Shoot
		if(Vector2.Distance(target.position,transform.position) < viewDistance && elapsedTime >= fireInterval){
			shotInstance = (GameObject)Instantiate(shot, transform.position, transform.rotation);
			shotInstance.GetComponent<ShotController>().Initialize(shotSpeed, transform.rotation * (Vector3)Vector2.right );
			Physics2D.IgnoreCollision(this.collider2D, shotInstance.collider2D);
			elapsedTime = 0;
		}
		
		if(elapsedTime < fireInterval){
			elapsedTime += Time.fixedDeltaTime;
		}
	}
	
	void FixedUpdate () {

		//Follow player
		if(Vector2.Distance(target.position,transform.position) > safeDistance &&  Vector2.Distance(target.position,transform.position) < viewDistance){
			horizontalMove = (target.position - transform.position).normalized.x;
		}
		else {
			horizontalMove = 0;
		}

		//Jump if the player is in a higher position
		if(target.position.y > transform.position.y + 0.7f){
			Jump();
		}

		//Rotate the boss acording to its movement direction
		if((target.position - transform.position).x < 0){
			transform.eulerAngles = new Vector3(0,180,0);
		} else if ((target.position - transform.position).x > 0){
			transform.eulerAngles = new Vector3(0,0,0);
		}
		
		if(!isHurt){
			rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);
		}
	}

	void Jump(){
		//Jump if is grounded
		if(isGrounded ){
			rigidbody2D.velocity = new Vector2(horizontalMove * speed, Mathf.Abs(jumpForce));
			audio.Play();
		}
	}

	public void HurtBoss(int damage){
		if(!isHurt){
			lifeManager.LifeDown(damage,LifeManager.LifeType.Boss);
			ouchSound.Play();
			StartCoroutine(StunBoss(0.8f));
		}
		Debug.Log("Boss Life: " + lifeManager.BossLife);
	}
	
	IEnumerator StunBoss(float stunTime){
		isHurt = true;
		yield return new WaitForSeconds(stunTime);
		if(lifeManager.BossLife <= 0){
			Explode();
			Destroy(gameObject);
		}
		isHurt = false;
	}

	void Explode(){
		GameObject particle = (GameObject)GameObject.Instantiate(explosion,transform.position,transform.rotation);
		particle.GetComponent<ParticleSystem>().renderer.sortingLayerName = "ForeGround";
		particle.GetComponent<ParticleSystem>().particleSystem.renderer.sortingOrder = 1;
	}

	void OnDrawGizmos(){
		Vector3 leftDistance = new Vector2(transform.position.x - viewDistance, transform.position.y);
		Vector3 rightDistance = new Vector2(transform.position.x + viewDistance, transform.position.y);
		Gizmos.DrawLine(leftDistance, rightDistance);
	}
}
