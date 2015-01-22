using UnityEngine;
using System.Collections;

public class StageStartMessageController : MonoBehaviour {
	public string stageNumber;
	bool showGui = false;
	bool fadeIn = false;
	bool fadeOut = false;
	bool waiting = false;
	Texture2D stageNumberTexture;
	Texture2D stageNameTexture;
	Texture2D lineTexture;
	float alpha;
	float tparamNumber = 0.05f;
	float velNumber = 0f;
	float tparamName = 0.05f;
	float velName = 0f;

	// Use this for initialization
	void Start () {
		stageNumberTexture = Resources.Load<Texture2D>("Shared/GUI/stages/pt/stage." + stageNumber + ".number");
		stageNameTexture = Resources.Load<Texture2D>("Shared/GUI/stages/pt/stage." + stageNumber + ".name");
		lineTexture = Resources.Load<Texture2D>("Shared/GUI/stages/stage.line");
		StartCoroutine(WaitAndStart ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		if (fadeIn) {
			alpha = Mathf.Lerp (alpha, 1, 1f * Time.deltaTime);
			if (!waiting) {
				StartCoroutine(FadeIn ());
			}
		}
		else if (fadeOut) {
			alpha = Mathf.Lerp (alpha, 0, 3f * Time.deltaTime);
			if (!waiting) {
				StartCoroutine(FadeOut ());
			}
		}

		if (showGui) {
			if (!waiting && !fadeIn && !fadeOut) {
				StartCoroutine(ShowAndWait());
			}

			GUI.color = new Color(1,1,1, alpha);

			tparamNumber += Time.deltaTime;
			tparamName += Time.deltaTime;
			GUI.DrawTexture (new Rect(Screen.width - stageNumberTexture.width - Mathf.SmoothDamp(280f, 0f, ref velNumber, tparamNumber), 200, stageNumberTexture.width, stageNumberTexture.height), stageNumberTexture);
			GUI.DrawTexture (new Rect(Screen.width - lineTexture.width - 50, 246, lineTexture.width, lineTexture.height), lineTexture);
			GUI.DrawTexture (new Rect(Screen.width - stageNameTexture.width - Mathf.SmoothDamp(90f, 300f, ref velName, tparamName), 256, stageNameTexture.width, stageNameTexture.height), stageNameTexture);
		}
	}

	private IEnumerator FadeIn ()
	{
		waiting = true;
		showGui = true;
		yield return new WaitForSeconds (2f);
		waiting = false;
		fadeIn = false;
	}

	private IEnumerator FadeOut ()
	{
		waiting = true;
		yield return new WaitForSeconds (2f);
		waiting = false;
		fadeOut = false;
		showGui = false;
	}

	private IEnumerator ShowAndWait ()
	{
		waiting = true;
		yield return new WaitForSeconds (3f);
		waiting = false;
		fadeOut = true;
	}

	private IEnumerator WaitAndStart ()
	{
		yield return new WaitForSeconds (1f);
		fadeIn = true;
	}
}
