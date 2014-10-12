using UnityEngine;
using System.Collections;

public class DestroyCollidingObject : MonoBehaviour {

	public float delay;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals("Player")){
			Destroy(other.gameObject, delay);
			GameObject.Find("GameManager").GetComponent<LifeManager>().EmptyLife(LifeManager.LifeType.Player);
		}
		else if (other.tag.Equals("Boss")){
			GameObject.Find("GameManager").GetComponent<LifeManager>().EmptyLife(LifeManager.LifeType.Boss);
			Destroy(other.gameObject,0.2f);
		}
		Destroy(other.gameObject,0.2f);
	}
}
