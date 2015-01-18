using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	public static bool avaliable = true;
	public static bool isPaused = false;
	private static bool shouldChange = false;
	private bool isPausing = false;
	public GUIStyle font = new GUIStyle ();
	public Texture texture;
	
	private float originalVolume;
	private bool originalGuiState;
	private bool originalCharState;
	private bool originalHoverState;
	private bool originalGroundState;
	private bool originalMovingState;
	
	public GUISkin guiskin;
	GUIButton[] buttons;
	int currentButton;
	bool changingFocus = false;
	Vector3 previousMousePos;
	bool joystickKeyboardEnabled = true;
	int buttonClicked = -1;
	
	// Use this for initialization
	void Start () {
		int i = 0;
		buttons = new GUIButton[3];
		buttons [i] = new GUIButton ();
		buttons[i].controlName = "continuar";
		buttons[i].text = "{ CONTINUAR }";
		buttons[i].rect = new Rect (Screen.width / 2 - 125 / 2, Screen.height / 2 + 50, 125, 25);
		i++;
		buttons [i] = new GUIButton ();
		buttons[i].controlName = "inicio";
		buttons[i].text = "{ TELA INICIAL }";
		buttons[i].rect = new Rect (Screen.width / 2 - 125 / 2, Screen.height / 2 + 100, 125, 25);
		i++;
		buttons [i] = new GUIButton ();
		buttons[i].controlName = "sair";
		buttons[i].text = "{ SAIR }";
		buttons[i].rect = new Rect (Screen.width / 2 - 125 / 2, Screen.height / 2  + 150, 125, 25);
		
		previousMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (avaliable && Input.GetButton ("Pause") && !isPausing || shouldChange) {
			if (isPaused) {
				isPaused = false;
				AudioListener.volume = originalVolume;
				Time.timeScale = 1f;
			}
			else {
				isPaused = true;
				originalVolume = AudioListener.volume;
				AudioListener.volume = 0.5f;
				Time.timeScale = 0.00001f;
			}
			isPausing = true;			
			StartCoroutine (Wait1Sec ());
			shouldChange = false;
		}
	}
	
	void OnGUI () {
		if (isPaused) {
			GUI.Box (new Rect (0f, 0f, Screen.width, Screen.height), "");
			GUI.DrawTexture (new Rect (Screen.width / 2f - 250f, Screen.height / 2f - 75f, 500f, 150f), texture);
			GUI.Label (new Rect (Screen.width / 2f - 65f, Screen.height / 2f - 25f, 120f, 50f), "PAUSE", font);
			
			if (previousMousePos != Input.mousePosition) {
				joystickKeyboardEnabled = false;
				previousMousePos = Input.mousePosition;
				currentButton = 0;
			}
			
			float axis = Input.GetAxis("Vertical");
			if (axis != 0 && !changingFocus) {
				if (GUI.GetNameOfFocusedControl() == "") {
					joystickKeyboardEnabled = true;
					currentButton = 0;
					GUI.FocusControl (buttons [0].controlName);
					changingFocus = true;
					StartCoroutine (ChangingFocus (1f));
				} else if (axis < 0) {
					joystickKeyboardEnabled = true;
					currentButton++;
					changingFocus = true;
					StartCoroutine (ChangingFocus (1f));
				} else if (axis > 0) {
					joystickKeyboardEnabled = true;
					currentButton--;
					changingFocus = true;
					StartCoroutine (ChangingFocus (1f));
				}
			}
			
			
			if (currentButton > buttons.Length - 1) {
				currentButton = 0;
			} else if (currentButton < 0) {
				currentButton = buttons.Length - 1;
			}
			
			GUI.skin = guiskin;
			
			foreach (var button in buttons) {
				GUI.SetNextControlName (button.controlName);
				switch (button.controlName) {
				case "continuar":
					if (GUI.Button (button.rect, button.text) || buttonClicked == 0) {
						isPaused = false;
						AudioListener.volume = originalVolume;
						
						Time.timeScale = 1f;
						isPausing = false;
						buttonClicked = -1;
					}
					break;
				case "inicio":								
					if (GUI.Button (button.rect, button.text)  || buttonClicked == 1) {
						isPaused = false;
						AudioListener.volume = originalVolume;				
						
						Time.timeScale = 1f;
						isPausing = false;
						buttonClicked = -1;

						GameObject camera = GameObject.Find ("Camera");
						if (camera != null) {
							camera.SendMessage ("OnApplicationQuit");
						}
						GameObject.Find("GameManager").GetComponent<CheckpointManager>().ClearCheckpoint();
						Application.LoadLevel (0);
					}								
					break;
				case "sair":
					if (GUI.Button (button.rect, button.text)  || buttonClicked == 2) {
						GameObject.Find("GameManager").GetComponent<CheckpointManager>().ClearCheckpoint();
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
	}
	
	IEnumerator Wait1Sec()    {
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + 0.5f) {
			yield return null;
		}
		isPausing = false;
	}
	
	IEnumerator ChangingFocus (float timeToWait)
	{		
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + timeToWait * 0.1f) {
			yield return null;
		}
		changingFocus = false;
	}
	
	public static void Pause() {
		shouldChange = true;
		isPaused = true;
	}
	
	public static void UnPause() {
		shouldChange = true;
		isPaused = false;
	}
}
