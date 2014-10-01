using UnityEngine;
using System.Collections;

public class LoadSceneOnButton : MonoBehaviour {

	public string SceneName;
	public string loadButton;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(loadButton)){
			Application.LoadLevel(SceneName);
		}
	}
}
