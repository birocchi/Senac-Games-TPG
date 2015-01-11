using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
		public Texture2D logo;
		public GUISkin guiskin;
		GUIButton[] buttons;
		int currentButton;
		bool changingFocus = false;
		Vector3 previousMousePos;
		bool joystickKeyboardEnabled = true;
		int buttonClicked = -1;

		// Use this for initialization
		void Start ()
		{
			int i = 0;
			buttons = new GUIButton[4];
			buttons [i] = new GUIButton ();
			buttons[i].controlName = "iniciar";
			buttons[i].text = "{ INICIAR }";
			buttons[i].rect = new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 130, 300, 50);
			i++;
			if (PlayerPrefs.GetString ("CurrentLevel") != null && !PlayerPrefs.GetString ("CurrentLevel").Equals ("")) {
				buttons [i] = new GUIButton ();
				buttons[i].controlName = "continuar";
				buttons[i].text = "{ CONTINUAR }";
				buttons[i].rect = new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 200, 300, 50);
			}
			i++;
			buttons [i] = new GUIButton ();
			buttons[i].controlName = "configurar";
			buttons[i].text = "{ CONFIGURAR }";
			buttons[i].rect = new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 270, 300, 50);
			i++;
			buttons [i] = new GUIButton ();
			buttons[i].controlName = "sair";
			buttons[i].text = "{ SAIR }";
			buttons[i].rect = new Rect (Screen.width / 2 - 300 / 2, Screen.height / 2 - 50 / 2 + 340, 300, 50);

			previousMousePos = Input.mousePosition;
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnGUI ()
	{
				GUI.DrawTexture (new Rect (Screen.width / 2 - logo.width / 2, Screen.height / 2 - logo.height / 2 - 200, logo.width, logo.height), logo);
				GUI.skin = guiskin;
				
				if (previousMousePos != Input.mousePosition) {
					joystickKeyboardEnabled = false;
					previousMousePos = Input.mousePosition;
					currentButton = 0;
				}

				Debug.Log (Input.mousePosition);

				float axis = Input.GetAxis("Vertical");
				if (axis != 0 && !changingFocus) {
					if (GUI.GetNameOfFocusedControl() == "") {
						joystickKeyboardEnabled = true;
						currentButton = 0;
						GUI.FocusControl (buttons [0].controlName);
						changingFocus = true;
						StartCoroutine (ChangingFocus (12f * Time.deltaTime));
					} else if (axis < 0) {
						joystickKeyboardEnabled = true;
						currentButton++;
						changingFocus = true;
						StartCoroutine (ChangingFocus (12f * Time.deltaTime));
					} else if (axis > 0) {
						joystickKeyboardEnabled = true;
						currentButton--;
						changingFocus = true;
						StartCoroutine (ChangingFocus (12f * Time.deltaTime));
					}
				}	
				
				if (currentButton > buttons.Length - 1) {
					currentButton = 0;
				} else if (currentButton < 0) {
					currentButton = buttons.Length - 1;
				}

				foreach (var button in buttons) {
						GUI.SetNextControlName (button.controlName);
						switch (button.controlName) {
						case "iniciar":
								if (GUI.Button (button.rect, button.text) || buttonClicked == 0) {
										PlayerPrefs.SetString ("CurrentLevel", "Fase1");
										Application.LoadLevel ("cutscene1");
								}
								break;
						case "continuar":								
								if (GUI.Button (button.rect, button.text)  || buttonClicked == 1) {
										Application.LoadLevel (PlayerPrefs.GetString ("CurrentLevel"));
								}								
								break;
						case "configurar":
								if (GUI.Button (button.rect, button.text)  || buttonClicked == 2) {
							
								}
								break;
						case "sair":
								if (GUI.Button (button.rect, button.text)  || buttonClicked == 3) {
										Application.Quit ();
								}
								break;
						}
					
						if (joystickKeyboardEnabled) {
							GUI.FocusControl (buttons [currentButton].controlName);
						if (Input.GetButton("Menu Selection")) {
								buttonClicked = currentButton;
							}
						}
						else {
							GUI.FocusControl ("");
						}
				}
		}

	IEnumerator ChangingFocus (float timeToWait)
	{		
		yield return new WaitForSeconds (timeToWait);
		changingFocus = false;
	}

}
