﻿using UnityEngine;
using System.Collections;

public class EnemyController: MonoBehaviour {
	
	enum Direction {left = -1, right = 1, down = -1, up = 1, none = 0};
	
	public int scoreValue;
	public int damage;
	public float speed;
	public Vector2 minLimit = new Vector2(-1,-1);
	public Vector2 maxLimit = new Vector2(1,1);
	public Vector2 initialDirection;
	public Sprite deathSprite;

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
		if(initialDirection.x == (int)Direction.right) 
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
		if(other.gameObject.tag == "Player"){
			if(other.contacts[0].normal.y < -0.8){
				other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x, 5);
				Physics2D.IgnoreCollision (other.collider, this.collider2D);
				audio.Play();
				dead = true;
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
