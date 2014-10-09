using UnityEngine;
using System.Collections;

public class StoneTrigger : MonoBehaviour {
	public GameObject Stone;

	private void ReleaseStone() {
		Stone.rigidbody2D.isKinematic = false;
		this.collider2D.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals ("Player")  ) {
			Debug.Log ("Soltar Pedra!");
			this.ReleaseStone ();
		}
	}
}
