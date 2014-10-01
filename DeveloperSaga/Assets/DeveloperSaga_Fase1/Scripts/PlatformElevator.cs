using UnityEngine;
using System.Collections;

public class PlatformElevator : MonoBehaviour {
	private bool elevatorOn;
	private bool goUp;
	private float initialPosition;
	public float maxPosition;
	
	// Use this for initialization
	void Start () {
		initialPosition = transform.position.y;
		if (maxPosition == 0) {
			maxPosition = initialPosition + 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		bool startPoint = false;
		
		if(this.transform.position.y >= maxPosition) {
			goUp = false;
		}
		if (this.transform.position.y <= initialPosition) {
			goUp = true;
			startPoint = true;
		}
		
		if (startPoint && !elevatorOn) {
			return;
		}
		
		if (goUp == false) {
			this.transform.Translate (0, -0.05f, 0);
		} else {
			this.transform.Translate (0, 0.05f, 0);
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