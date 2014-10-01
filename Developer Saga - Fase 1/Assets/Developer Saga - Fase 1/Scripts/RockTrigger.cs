using UnityEngine;
using System.Collections;

public class RockTrigger : MonoBehaviour {
	public GameObject Rock;

	private void ReleaseRock() {
		Rock.rigidbody2D.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Soltar Pedra!");
		this.ReleaseRock();
	}
}
