using UnityEngine;
using System.Collections;

public class KeyholeSwitch : MonoBehaviour {

	public KeysManager.Keys keyColor;
	KeysManager keysManager;

	// Use this for initialization
	void Start () {
		keysManager = GameObject.Find("GameManager").GetComponent<KeysManager>();
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag.Equals("Player") && keysManager.HasKey(keyColor)){
			AudioSource.PlayClipAtPoint(audio.clip, transform.position);
			keysManager.LoseKey(keyColor);
			Destroy(gameObject);
		}
	}
}
