using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	public static bool avaliable = true;
	private bool isPausing = false;
	private bool paused = false;	
	public GUIStyle font = new GUIStyle ();
	public Texture texture;

	private float originalVolume;
	private bool originalGuiState;
	private bool originalCharState;
	private bool originalHoverState;
	private bool originalGroundState;
	private bool originalMovingState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (avaliable && Input.GetButton ("Pause") && !isPausing) {
			if (paused) {
				paused = false;
				AudioListener.volume = originalVolume;
				GUIController.halt = originalGuiState;
				CharController.halt = originalCharState;
				HoverEnemy.shouldMove = originalHoverState;
				GroundedEnemy.shouldMove = originalGroundState;
				MovingEnemy.shouldMove = originalMovingState;


				Time.timeScale = 1f;
			}
			else {
				paused = true;
				originalVolume = AudioListener.volume;
				AudioListener.volume = 0.5f;
				originalGuiState = GUIController.halt;
				GUIController.halt = true;
				originalCharState = CharController.halt;
				CharController.halt = true;
				originalHoverState = HoverEnemy.shouldMove;
				HoverEnemy.shouldMove = false;
				originalGroundState = GroundedEnemy.shouldMove;
				GroundedEnemy.shouldMove = false;
				originalMovingState = MovingEnemy.shouldMove;
				MovingEnemy.shouldMove = false;
				Time.timeScale = 0.00001f;
			}
			isPausing = true;			
			StartCoroutine (Wait1Sec ());
		}
	}

	void OnGUI () {
		if (paused) {
			GUI.Box (new Rect (0f, 0f, Screen.width, Screen.height), "");
			GUI.DrawTexture (new Rect (Screen.width / 2f - 250f, Screen.height / 2f - 75f, 500f, 150f), texture);
			GUI.Label (new Rect (Screen.width / 2f - 65f, Screen.height / 2f - 25f, 120f, 50f), "PAUSE", font);
		}
	}

	IEnumerator Wait1Sec()    {
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + 0.5f) {
			yield return null;
		}
		isPausing = false;
	}
}
