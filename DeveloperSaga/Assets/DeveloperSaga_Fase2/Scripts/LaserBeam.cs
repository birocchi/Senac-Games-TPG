using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	public int damage = 1;

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag.Equals("Player")){
			other.rigidbody.velocity = (-other.contacts[0].normal + Vector2.up) * 3;
			other.gameObject.GetComponent<PlayerController_Fase2>().HurtPlayer(damage);
		}
	}
}
