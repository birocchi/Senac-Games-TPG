using UnityEngine;
using System.Collections;

public class PlatformElevator : MonoBehaviour {
	private bool elevatorOn;
	private bool goingUp;
	public float initialPosition;
	public float maxPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position.y;
		maxPosition = initialPosition + 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (elevatorOn) {
			if(this.transform.position.y >= maxPosition) {
				goingUp = false;
			}
			if(this.transform.position.y <= initialPosition) {
				goingUp = true;
			}

			if (goingUp) {
				this.transform.Translate (0, 0.05f, 0);
			}
			if (goingUp == false) {
				this.transform.Translate (0, -0.05f, 0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag.Equals ("Player")) {
			elevatorOn = true;
		}
	}

	void OnCollisionExit2d(Collision2D collision){
		if (collision.gameObject.tag.Equals ("Player")) {
			elevatorOn = false;
		}
	}

	void GoUp(){
		rigidbody2D.AddForce(Vector3.up * 1000);
	}
}
