using UnityEngine;
using System.Collections;

public class PlatformElevator : MonoBehaviour {
	private bool elevatorOn;
	private bool goUp;
	private float initialPosition;
	public float maxPosition;
	public bool moveSideways;
	
	// Use this for initialization
	void Awake () {
		if (moveSideways) {
			initialPosition = transform.position.x;
		} else {
			initialPosition = transform.position.y;
		}
		if (maxPosition == 0) {
			maxPosition = initialPosition + 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		bool startPoint = false;

		float currentPosition;
		if (moveSideways) {
			currentPosition = this.transform.position.x;
		} else {
			currentPosition = this.transform.position.y;
		}

		if(currentPosition >= maxPosition) {
			goUp = false;
		}
		if (currentPosition <= initialPosition) {
			goUp = true;
			startPoint = true;
		}
		
		if (startPoint && !elevatorOn) {
			return;
		}
		
		if (goUp == false) {
			if(moveSideways) {
				this.transform.Translate (-0.05f, 0, 0);
			} else {
				this.transform.Translate (0, -0.05f, 0);
			}
		} else {
			if(moveSideways) {
				this.transform.Translate (0.05f, 0, 0);
			} else {
				this.transform.Translate (0, 0.05f, 0);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag.Equals ("Player")) {
			elevatorOn = true;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag.Equals ("Player")) {
			elevatorOn = false;
		}
	}
}