using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
		public Texture2D logo;
		public GUISkin guiskin;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		void OnGUI ()
		{
				
		
				GUI.skin = guiskin;
				GUI.DrawTexture (new Rect (Screen.width / 2 - logo.width / 2, Screen.height / 2 - logo.height / 2 - 200, logo.width, logo.height), logo);
				if (GUI.Button (new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 150, 300, 50), "{ INICIAR }")) {
						Application.LoadLevel ("cutscene1");
				}
				GUI.Button (new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 220, 300, 50), "{ CONTINUAR }");
				if (GUI.Button (new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 290, 300, 50), "{ SAIR }")) {
						Application.Quit ();
				}
		}
}
