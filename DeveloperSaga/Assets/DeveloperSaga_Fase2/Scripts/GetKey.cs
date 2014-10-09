using UnityEngine;
using System.Collections;

public class GetKey : MonoBehaviour {

	KeysManager keyManager;

	// Use this for initialization
	void Start () {
		keyManager = GameObject.Find("GameManager").GetComponent<KeysManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			//keyManager.GetKey();
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			Destroy(gameObject);
		}
	}
}
