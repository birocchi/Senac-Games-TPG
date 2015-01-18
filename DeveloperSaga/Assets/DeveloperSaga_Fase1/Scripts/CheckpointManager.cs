using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	public Component playerControllerScript;

	private LifeManager lifeManager;
	private JumpScript jumpScript;

	void Awake(){
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		jumpScript = GameObject.Find("Player").GetComponent<JumpScript>();
	}

	public void SaveState(){
		PlayerPrefs.SetFloat("PlayerX",playerControllerScript.transform.position.x);
		PlayerPrefs.SetFloat("PlayerY",playerControllerScript.transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ",playerControllerScript.transform.position.z);
		PlayerPrefs.SetInt("PlayerLife",lifeManager.PlayerLife);
		if(jumpScript != null)
			PlayerPrefs.SetInt("JumpEnabled",jumpScript.enabled ? 1 : 0);

		PlayerPrefs.Save();
	}
	
	public void LoadState(){
		if(PlayerPrefs.GetInt("ReloadedLevel") == 1){
			playerControllerScript.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
			lifeManager.PlayerLife = PlayerPrefs.GetInt("PlayerLife");
			if(jumpScript != null)
				jumpScript.enabled = PlayerPrefs.GetInt("JumpEnabled") == 1 ? true : false;
		}
	}

	public void ReloadCheckPoint(){
		PlayerPrefs.SetInt("ReloadedLevel", 1);
		Application.LoadLevel (Application.loadedLevelName);
	}

	public static void ClearCheckpoint(){
		PlayerPrefs.SetInt("ReloadedLevel", 0);
	}
}