using UnityEngine;
using System.Collections;

public class RockTrigger : MonoBehaviour {
	public GameObject Rock;

	private void ReleaseRock() {
		Rock.rigidbody2D.isKinematic = false;
		this.collider2D.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player")  ) {
			this.ReleaseRock ();
		}
	}
}