using UnityEngine;
using System.Collections;

public class GetScript : MonoBehaviour {

	public GameObject hiddenEnemies;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player")){
			other.gameObject.GetComponent<ShootingScript>().enabled = true;
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			hiddenEnemies.SetActive(true);
			Destroy(gameObject);
		}
	}

}
