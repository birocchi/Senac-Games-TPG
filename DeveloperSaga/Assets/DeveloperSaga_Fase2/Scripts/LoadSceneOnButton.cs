using UnityEngine;
using System.Collections;

public class LoadSceneOnButton : MonoBehaviour
{

		public string loadButton;

		// Update is called once per frame
		void Update ()
		{
		
				if (Input.GetButtonDown (loadButton)) {
						string levelName = PlayerPrefs.GetString ("CurrentLevel");
						if (levelName != null && !"".Equals (levelName)) {
								Application.LoadLevel (levelName);
						} else {
								Application.LoadLevel ("menu");
						}
				}
		}
}
