using UnityEngine;
using System.Collections;

public class EnemyController: MonoBehaviour {
	
	enum Direction {left = -1, right = 1};
	
	public float speed;
	public float minWalkLimit;
	public float maxWalkLimit;
	public Sprite deathSprite;

	ScoreManager scoreManager;

	Vector3 initialPosition;
	Direction direction = Direction.left;
	Animator animator;
	bool dead = false;

	void Start(){
		animator = GetComponent<Animator>();
		scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
		initialPosition = transform.position;
	}

	void Update () {
		if (!dead){
			if(transform.position.x <= initialPosition.x + minWalkLimit){
				direction = Direction.right;
				transform.eulerAngles = new Vector3(0,180,0);
			}
			else if(transform.position.x >= initialPosition.x + maxWalkLimit){
				direction = Direction.left;
				transform.eulerAngles = new Vector3(0,0,0);
			}
		}
	}
	
	void FixedUpdate () {
		rigidbody2D.velocity = new Vector2((int)direction * speed,rigidbody2D.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player" && other.contacts[0].normal.y < -0.8){
			Physics2D.IgnoreCollision (other.collider, this.collider2D);
			if(!dead)
				audio.Play();
			dead = true;
			speed = 0;
			animator.SetBool("Dead", true);
			scoreManager.AddScore(50);
			Destroy(gameObject,5);
		}
	}
	
}
