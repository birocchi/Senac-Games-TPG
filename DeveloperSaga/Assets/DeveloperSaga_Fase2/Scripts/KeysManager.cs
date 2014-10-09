using UnityEngine;
using System.Collections;

public class KeysManager : MonoBehaviour {

	public enum Keys {Red = 0, Green = 1, Blue = 2, Yellow = 3};

	public bool HasRedKey {get; set;}
	public bool HasBlueKey {get; set;}
	public bool HasGreenKey {get; set;}
	public bool HasYellowKey {get; set;}

	// Use this for initialization
	void Start () {
		HasRedKey = false;
     	HasBlueKey = false;
     	HasGreenKey = false;
     	HasYellowKey = false;
	}

}
