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
	float duration = 0.9F;
	float startTime;
	
	// Use this for initialization
	void Start () {
		stageNumberTexture = Resources.Load<Texture2D>("Shared/GUI/stages/pt/stage." + stageNumber + ".number");
		stageNameTexture = Resources.Load<Texture2D>("Shared/GUI/stages/pt/stage." + stageNumber + ".name");
		lineTexture = Resources.Load<Texture2D>("Shared/GUI/stages/pt/stage." + stageNumber + ".line");
		StartCoroutine(WaitAndStart ());
		startTime = Time.time;
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
		
		float posNum = 0f;
		float posNam = 0f;
		
		if (fadeIn || (!fadeOut && showGui)) {
			float t = (Time.time - startTime) / duration;
			posNum = Mathf.SmoothStep(100f, 300f, t);
			posNam = Mathf.SmoothStep(300f, 100f, t);
		}
		else if (fadeOut) {
			float t = (Time.time - startTime) / duration;
			posNum = Mathf.SmoothStep(300f, 100f, t);
			posNam = Mathf.SmoothStep(100f, 300f, t);
		}
		
		if (showGui) {
			if (!waiting && !fadeIn && !fadeOut) {
				StartCoroutine(ShowAndWait());
			}
			
			GUI.color = new Color(1,1,1, alpha);
			
			GUI.DrawTexture (new Rect(Screen.width - stageNumberTexture.width - 100 - posNum, 220, stageNumberTexture.width, stageNumberTexture.height), stageNumberTexture);
			GUI.DrawTexture (new Rect(Screen.width - stageNameTexture.width - 100 - posNam, 276, stageNameTexture.width, stageNameTexture.height), stageNameTexture);
			GUI.DrawTexture (new Rect(Screen.width - lineTexture.width - 100 - 50, 266, lineTexture.width, lineTexture.height), lineTexture);
		}
	}
	
	private IEnumerator FadeIn ()
	{
		waiting = true;
		showGui = true;
		startTime = Time.time;
		yield return new WaitForSeconds (2f);
		waiting = false;
		fadeIn = false;
	}
	
	private IEnumerator FadeOut ()
	{
		waiting = true;
		startTime = Time.time;
		yield return new WaitForSeconds (2f);
		waiting = false;
		fadeOut = false;
		showGui = false;
	}
	
	private IEnumerator ShowAndWait ()
	{
		waiting = true;
		yield return new WaitForSeconds (3.5f);
		waiting = false;
		fadeOut = true;
	}
	
	private IEnumerator WaitAndStart ()
	{
		yield return new WaitForSeconds (1f);
		fadeIn = true;
	}
}
