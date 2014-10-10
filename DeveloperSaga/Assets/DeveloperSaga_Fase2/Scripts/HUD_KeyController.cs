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
		if(keysManager.HasKey(keyColor) && !image.texture.Equals(keyEnabled)){
			image.texture = keyEnabled;
		} else if(!keysManager.HasKey(keyColor) && !image.texture.Equals(keyDisabled)){
			image.texture = keyDisabled;
		}
//		switch (keyColor){
//		case KeysManager.Keys.Red:
//			if(keysManager.HasRedKey && !image.texture.Equals(keyEnabled)){
//				image.texture = keyEnabled;
//			} else if(!keysManager.HasRedKey && !image.texture.Equals(keyDisabled)){
//				image.texture = keyDisabled;
//			}
//			break;
//		case KeysManager.Keys.Green:
//			if(keysManager.HasGreenKey && !image.texture.Equals(keyEnabled)){
//				image.texture = keyEnabled;
//			} else if(!keysManager.HasGreenKey && !image.texture.Equals(keyDisabled)){
//				image.texture = keyDisabled;
//			}
//			break;
//		case KeysManager.Keys.Blue:
//			if(keysManager.HasBlueKey && !image.texture.Equals(keyEnabled)){
//				image.texture = keyEnabled;
//			} else if(!keysManager.HasBlueKey && !image.texture.Equals(keyDisabled)){
//				image.texture = keyDisabled;
//			}
//			break;
//		case KeysManager.Keys.Yellow:
//			if(keysManager.HasYellowKey && !image.texture.Equals(keyEnabled)){
//				image.texture = keyEnabled;
//			} else if(!keysManager.HasYellowKey && !image.texture.Equals(keyDisabled)){
//				image.texture = keyDisabled;
//			}
//			break;
//		}

	}
}
