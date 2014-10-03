using UnityEngine;
using System.Collections;

public class RockTrigger : MonoBehaviour {
	public GameObject Rock;

	private void ReleaseRock() {
		Rock.rigidbody2D.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player")) {
			Debug.Log ("Soltar Pedra!");
			this.ReleaseRock ();
		}
	}
}
