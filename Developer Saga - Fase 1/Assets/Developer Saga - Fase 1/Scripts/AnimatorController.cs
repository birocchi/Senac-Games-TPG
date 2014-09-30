using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Animator> ().SetFloat ("VelocityX", this.rigidbody2D.velocity.x);
		this.GetComponent<Animator> ().SetFloat ("VelocityY", this.rigidbody2D.velocity.y);

		if (this.rigidbody2D.velocity.x > 0) {
			this.transform.localScale = new Vector3 (1, 1, 1);
		}
		if (this.rigidbody2D.velocity.x < 0) {
			this.transform.localScale = new Vector3 (-1, 1, 1);
		}

	}
}
