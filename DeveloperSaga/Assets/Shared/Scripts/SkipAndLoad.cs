using UnityEngine;
using System.Collections;

public class SkipAndLoad : MonoBehaviour
{
		public string levelName;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyUp (KeyCode.Escape) || Input.GetButton("Pause") || Input.touchCount > 0) {
						PlayerPrefs.SetString ("CurrentLevel", levelName);
						PlayerPrefs.Save ();
						Application.LoadLevel (levelName);
				}
		}
}
