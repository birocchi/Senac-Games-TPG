using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneName;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag.Equals("Player")){
			Application.LoadLevel(sceneName);
		}
	}
}
