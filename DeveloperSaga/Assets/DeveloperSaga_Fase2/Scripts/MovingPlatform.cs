using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Transform destination;
	public float speed = 1;

	private Vector3 initialPosition;
	private Vector3 finalPosition;
	private Vector2 movingDirection;
	private bool waiting;

	void Start() {
		waiting = false;
		initialPosition = transform.position;
		finalPosition = destination.position;
		movingDirection = (finalPosition - initialPosition).normalized;
	}

	void FixedUpdate() {

		if(Vector3.Distance(transform.position, finalPosition) > 0.01f){
			rigidbody2D.velocity = movingDirection * speed;
		}
		else if(!waiting){
			rigidbody2D.velocity = Vector2.zero;
			StartCoroutine(ChangeDirection(1));
		}
	}

	IEnumerator ChangeDirection(float seconds){
		waiting = true;

		yield return new WaitForSeconds(seconds);

		Vector3 tempSwap = finalPosition;
		finalPosition = initialPosition;
		initialPosition = tempSwap;

		movingDirection = -movingDirection;

		waiting = false;
	}

	void OnDrawGizmos(){
		if(finalPosition.Equals(Vector3.zero) && initialPosition.Equals(Vector3.zero)){
			Gizmos.DrawWireCube(destination.position + new Vector3(0,0.15f,0) , new Vector3(2.1f, 0.4f, 0f));
		}
		else {
			Gizmos.DrawWireCube(initialPosition + new Vector3(0,0.15f,0) , new Vector3(2.1f, 0.4f, 0f));
			Gizmos.DrawWireCube(finalPosition + new Vector3(0,0.15f,0) , new Vector3(2.1f, 0.4f, 0f));
		}
	}
}
