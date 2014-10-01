using UnityEngine;
using System.Collections;

public class DestroyCollidingObject : MonoBehaviour {

	public float delay;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Destroy(other.gameObject, delay);
			GameObject.Find("GameManager").GetComponent<LifeManager>().EmptyLife();
		}
	}
}
