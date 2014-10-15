using UnityEngine;
using System.Collections;

public class GetScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player")){
			other.gameObject.GetComponent<ShootingScript>().enabled = true;
			Destroy(gameObject);
		}
	}

}
