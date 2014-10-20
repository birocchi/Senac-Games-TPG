using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	//Movement Variables
	public float speed;
	public float jumpForce;
	public float safeDistance;
	public float viewDistance;
	public float armUpTime = 1.2f;

	//Shot variables
	public GameObject shot;
	public float fireRate = 1;
	public float shotSpeed = 9;
	public float shotLifeTime = 0.6f;
	private float fireInterval;
	private float elapsedTime;
	private float armUpCounter;
	private GameObject shotInstance;
	
	//Used by the AnimationController
	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isHurt;
	[HideInInspector]
	public float horizontalMove;
	[HideInInspector]
	public bool isShooting;

	//Other variables
	public Transform target;
	public AudioSource ouchSound;
	public AudioSource explosionSound;
	public GameObject explosion;
	public GameObject lasers;
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
		if(target != null){
			// The boss is grounded if a linecast to the groundcheck position hits anything on the ground layer.
			isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Steppable"));
			Debug.DrawLine(transform.position, groundCheck.position);

			//Shoot
			if(Vector2.Distance(target.position,transform.position) < viewDistance && elapsedTime >= fireInterval){
				isShooting = true;
				shotInstance = (GameObject)Instantiate(shot, transform.position, transform.rotation);
				shotInstance.GetComponent<ShotController>().Initialize(shotSpeed, transform.rotation * (Vector3)Vector2.right, shotLifeTime );
				Physics2D.IgnoreCollision(this.collider2D, shotInstance.collider2D);
				elapsedTime = 0;
				armUpCounter = armUpTime;
			}
			
			if(elapsedTime < fireInterval){
				elapsedTime += Time.fixedDeltaTime;
			}

			if(armUpCounter <= 0){
				isShooting = false;
			}else{
				armUpCounter -= Time.deltaTime;
			}
		}
	}
	
	void FixedUpdate () {

		if(target != null){
			//Follow player
			if(Vector2.Distance(target.position,transform.position) > safeDistance &&  Vector2.Distance(target.position,transform.position) < viewDistance){
				horizontalMove = (target.position - transform.position).normalized.x;
			}
			else {
				horizontalMove = 0;
			}

			//Jump if the player is in a higher position
			if(target.position.y > transform.position.y + 0.7f && Vector2.Distance(target.position,transform.position) < viewDistance){
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
		if(lifeManager.BossLife <= 0){
			EndBossBattle();
		}
		yield return new WaitForSeconds(stunTime);
		isHurt = false;
	}

	void Explode(){
		GameObject particle = (GameObject)GameObject.Instantiate(explosion,transform.position,transform.rotation);
		particle.GetComponent<ParticleSystem>().renderer.sortingLayerName = "ForeGround";
		particle.GetComponent<ParticleSystem>().particleSystem.renderer.sortingOrder = 1;
		AudioSource.PlayClipAtPoint(explosionSound.clip,transform.position);
	}

	void EndBossBattle(){
		Explode();
		lasers.SetActive(false);
		SongController.songToPlay = 0;
		Destroy(gameObject);
	}

	void OnDrawGizmos(){
		Vector3 leftDistance = new Vector2(transform.position.x - viewDistance, transform.position.y);
		Vector3 rightDistance = new Vector2(transform.position.x + viewDistance, transform.position.y);
		Gizmos.DrawLine(leftDistance, rightDistance);
	}
}
