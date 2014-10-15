using UnityEngine;
using System.Collections;

public class EnemyController: MonoBehaviour {
	
	enum Direction {left = -1, right = 1, down = -1, up = 1, none = 0};
	
	public int scoreValue;
	public int damage;
	public float speed;
	public Vector2 minLimit = new Vector2(-1,-1);
	public Vector2 maxLimit = new Vector2(1,1);
	public Vector2 initialDirection;

	ScoreManager scoreManager;
	Vector2 initialPosition;
	Vector2 direction;
	Animator animator;
	bool dead = false;

	void Start(){
		animator = GetComponent<Animator>();
		scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
		initialPosition = transform.position;
		direction = initialDirection;
		if(initialDirection.x > 0) 
			transform.eulerAngles = new Vector3(0,180,0);
	}

	void Update () {
		if (!dead){
			if(transform.position.x <= initialPosition.x + minLimit.x){
				direction.x = (int)Direction.right;
				transform.eulerAngles = new Vector3(0,180,0);
			}
			else if(transform.position.x >= initialPosition.x + maxLimit.x){
				direction.x = (int)Direction.left;
				transform.eulerAngles = new Vector3(0,0,0);
			}

			if(transform.position.y <= initialPosition.y + minLimit.y){
				direction.y = (int)Direction.up;
			}
			else if(transform.position.y >= initialPosition.y + maxLimit.y){
				direction.y = (int)Direction.down;
			}
		}
	}
	
	void FixedUpdate () {
		if(!dead){
			rigidbody2D.velocity = new Vector2((int)direction.x * speed, (int)direction.y * speed);
		}
		else{
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag.Equals("Player")){
			if(other.contacts[0].normal.y < -0.8){
				other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x, 5);
				Die();
			}
			else {
				other.rigidbody.velocity = (-other.contacts[0].normal + Vector2.up) * 3;
				other.gameObject.GetComponent<PlayerController_Fase2>().HurtPlayer(damage);

			}
		}
	}

	public void Die(){
		Physics2D.IgnoreCollision (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), this.collider2D);
		audio.Play();
		dead = true;
		animator.SetBool("Dead", true);
		scoreManager.AddScore(scoreValue);
		Destroy(gameObject,2);
	}

	void OnDrawGizmos(){
		if(initialPosition.Equals(Vector2.zero)){
			Gizmos.DrawWireCube (new Vector2(transform.position.x + (maxLimit.x + minLimit.x)/2, transform.position.y + (maxLimit.y + minLimit.y)/2), 
			                     new Vector2(maxLimit.x - minLimit.x,maxLimit.y - minLimit.y));
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)initialDirection.normalized);
		}
		else{
			Gizmos.DrawWireCube (new Vector2(initialPosition.x + (maxLimit.x + minLimit.x)/2,  initialPosition.y + (maxLimit.y + minLimit.y)/2), 
			                     new Vector2(maxLimit.x - minLimit.x,maxLimit.y - minLimit.y));
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction.normalized);
		}
	}
	
}
