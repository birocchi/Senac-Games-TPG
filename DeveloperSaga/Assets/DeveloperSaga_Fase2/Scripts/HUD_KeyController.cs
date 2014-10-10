using UnityEngine;
using System.Collections;

public class HUD_KeyController : MonoBehaviour {

	public Texture2D keyEnabled;
	public Texture2D keyDisabled;
	public KeysManager.Keys keyColor;

	private KeysManager keysManager;
	private GUITexture image;

	// Use this for initialization
	void Start () {
		keysManager = GameObject.Find("GameManager").GetComponent<KeysManager>();
		image = GetComponent<GUITexture>();
		image.texture = keyDisabled;
	}
	
	// Update is called once per frame
	void Update () {
		switch (keyColor){
		case KeysManager.Keys.Red:
			if(keysManager.HasRedKey && !image.texture.Equals(keyEnabled)){
				image.texture = keyEnabled;
			} else if(!keysManager.HasRedKey && !image.texture.Equals(keyDisabled)){
				image.texture = keyDisabled;
			}
			break;
		case KeysManager.Keys.Green:
			if(keysManager.HasGreenKey && !image.texture.Equals(keyEnabled)){
				image.texture = keyEnabled;
			} else if(!keysManager.HasGreenKey && !image.texture.Equals(keyDisabled)){
				image.texture = keyDisabled;
			}
			break;
		case KeysManager.Keys.Blue:
			if(keysManager.HasBlueKey && !image.texture.Equals(keyEnabled)){
				image.texture = keyEnabled;
			} else if(!keysManager.HasBlueKey && !image.texture.Equals(keyDisabled)){
				image.texture = keyDisabled;
			}
			break;
		case KeysManager.Keys.Yellow:
			if(keysManager.HasYellowKey && !image.texture.Equals(keyEnabled)){
				image.texture = keyEnabled;
			} else if(!keysManager.HasYellowKey && !image.texture.Equals(keyDisabled)){
				image.texture = keyDisabled;
			}
			break;
		}
		if(keyColor.Equals(KeysManager.Keys.Red)){

		}
		if(keysManager.HasGreenKey && keyColor.Equals(KeysManager.Keys.Green) && !image.texture.Equals(keyEnabled)){
			image.texture = keyEnabled;
		}
		if(keysManager.HasBlueKey && keyColor.Equals(KeysManager.Keys.Blue) && !image.texture.Equals(keyEnabled)){
			image.texture = keyEnabled;
		}
		if(keysManager.HasYellowKey && keyColor.Equals(KeysManager.Keys.Yellow) && !image.texture.Equals(keyEnabled)){
			image.texture = keyEnabled;
		}
	}
}
