using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 movingDirection;
	public float speed = 1;
	public float movingTime = 1;

	private float elapsedTime;
	private Rigidbody2D standingBody;

	void Start() {
		elapsedTime = 0;
		movingDirection = movingDirection.normalized;
	}

	void FixedUpdate() {

		if(elapsedTime < movingTime){
			rigidbody2D.velocity = movingDirection * speed;
			elapsedTime += Time.fixedDeltaTime;
		}
		else {
			speed = -speed;
			elapsedTime = 0;
		}

	}

}
