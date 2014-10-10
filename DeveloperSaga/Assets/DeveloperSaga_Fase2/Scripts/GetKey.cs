using UnityEngine;
using System.Collections;

public class GetKey : MonoBehaviour {

	public KeysManager.Keys keyColor;
	KeysManager keysManager;
	
	void Start () {
		keysManager = GameObject.Find("GameManager").GetComponent<KeysManager>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Equals("Player")){
			keysManager.GetKey(keyColor);
			AudioSource.PlayClipAtPoint(audio.clip,transform.position);
			Destroy(gameObject);
		}
	}
}
