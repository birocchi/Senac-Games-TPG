using UnityEngine;
using System.Collections;

public class KeysManager : MonoBehaviour {

	public enum Keys {Red = 0, Green = 1, Blue = 2, Yellow = 3};

	bool hasRedKey;
	bool hasBlueKey;
	bool hasGreenKey;
	bool hasYellowKey;

	public bool HasRedKey { get{return hasRedKey;} }
	public bool HasBlueKey { get{return hasBlueKey;} }
	public bool HasGreenKey { get{return hasGreenKey;} }
	public bool HasYellowKey { get{return hasYellowKey;} }

	// Use this for initialization
	void Start () {
		hasRedKey = false;
     	hasBlueKey = false;
     	hasGreenKey = false;
     	hasYellowKey = false;
	}

	public void GetKey(Keys keyColor){
		switch(keyColor){
		case Keys.Red:
			hasRedKey = true;
			break;
		case Keys.Green:
			hasGreenKey = true;
			break;
		case Keys.Blue:
			hasBlueKey = true;
			break;
		case Keys.Yellow:
			hasYellowKey = true;
			break;
		}
	}

	public void LoseKey(Keys keyColor){
		switch(keyColor){
		case Keys.Red:
			hasRedKey = false;
			break;
		case Keys.Green:
			hasGreenKey = false;
			break;
		case Keys.Blue:
			hasBlueKey = false;
			break;
		case Keys.Yellow:
			hasYellowKey = false;
			break;
		}
	}

	public void ResetKeys(){
		hasRedKey = false;
		hasGreenKey = false;
		hasBlueKey = false;
		hasYellowKey = false;
	}

}
