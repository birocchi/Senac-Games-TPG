using UnityEngine;
using System.Collections;

public class EnemyController: MonoBehaviour {
	
	enum Direction {left = -1, right = 1};
	
	public int scoreValue;
	public int damage;
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
		rigidbody2D.velocity = new Vector2((int)direction * speed, rigidbody2D.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			if(other.contacts[0].normal.y < -0.8){
				other.rigidbody.velocity += Vector2.up * 5;
				Physics2D.IgnoreCollision (other.collider, this.collider2D);
				audio.Play();
				dead = true;
				speed = 0;
				animator.SetBool("Dead", true);
				scoreManager.AddScore(scoreValue);
				Destroy(gameObject,2);
			}
			else {

				other.rigidbody.velocity = (-other.contacts[0].normal + Vector2.up) * 3;
				other.gameObject.GetComponent<PlayerController>().HurtPlayer(damage);

			}
		}
	}
	
}
