using UnityEngine;
using System.Collections;

public class ChangeSceneOnTrigger : MonoBehaviour {
	public string sceneName;

	void OnTriggerEnter2D(Collider2D other) {
		Application.LoadLevel (sceneName);
	}
}
