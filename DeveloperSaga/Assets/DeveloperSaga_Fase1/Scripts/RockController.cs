using UnityEngine;
using System.Collections;

public class RockController : MonoBehaviour {
	private Collider2D playerCollider;

	void Start () {
		playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log(collision.gameObject.layer);
		if (collision.gameObject.layer == 8) {
			Debug.Log("Pedra no chao!");
			Physics2D.IgnoreCollision (playerCollider, this.collider2D);
		}
	}
}