using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	public Component playerControllerScript;

	private LifeManager lifeManager;
	private KeysManager keysManager;
	private JumpScript jumpScript;
	private ShootingScript shotScript;
	private bool hasCheckpoint;

	void Awake(){
		lifeManager = GameObject.Find("GameManager").GetComponent<LifeManager>();
		jumpScript = GameObject.Find("Player").GetComponent<JumpScript>();
		shotScript = GameObject.Find("Player").GetComponent<ShootingScript>();
		keysManager = GameObject.Find("GameManager").GetComponent<KeysManager>();
	}

	public void SaveState(){
		hasCheckpoint = true;
		PlayerPrefs.SetFloat("PlayerX",playerControllerScript.transform.position.x);
		PlayerPrefs.SetFloat("PlayerY",playerControllerScript.transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ",playerControllerScript.transform.position.z);
		PlayerPrefs.SetInt("PlayerLife",lifeManager.PlayerLife);
		if(jumpScript != null)
			PlayerPrefs.SetInt("JumpEnabled",jumpScript.enabled ? 1 : 0);
		if(shotScript != null)
			PlayerPrefs.SetInt("ShotEnabled",shotScript.enabled ? 1 : 0);
		if(keysManager != null){
			PlayerPrefs.SetInt("RedKey",keysManager.HasKey(KeysManager.Keys.Red) ? 1 : 0);
			PlayerPrefs.SetInt("GreenKey",keysManager.HasKey(KeysManager.Keys.Green) ? 1 : 0);
		}

		PlayerPrefs.Save();
	}
	
	public void LoadState(){
		if(PlayerPrefs.GetInt("ReloadedLevel") == 1){
			playerControllerScript.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
			lifeManager.PlayerLife = PlayerPrefs.GetInt("PlayerLife");
			if(jumpScript != null)
				jumpScript.enabled = PlayerPrefs.GetInt("JumpEnabled") == 1 ? true : false;
			if(shotScript != null)
				shotScript.enabled = PlayerPrefs.GetInt("ShotEnabled") == 1 ? true : false;
			if(keysManager != null){
				keysManager.hasRedKey = PlayerPrefs.GetInt("RedKey") == 1 ? true : false;
				keysManager.hasGreenKey = PlayerPrefs.GetInt("GreenKey") == 1 ? true : false;
			}
		}
	}

	public void ReloadCheckPoint(){
		if(hasCheckpoint){
			PlayerPrefs.SetInt("ReloadedLevel", 1);
		}
		Application.LoadLevel (Application.loadedLevelName);
	}

	public static void ClearCheckpoint(){
		PlayerPrefs.DeleteKey("ReloadedLevel");
		PlayerPrefs.DeleteKey("PlayerX");
		PlayerPrefs.DeleteKey("PlayerY");
		PlayerPrefs.DeleteKey("PlayerZ");
		PlayerPrefs.DeleteKey("PlayerLife");
		PlayerPrefs.DeleteKey("JumpEnabled");
	}
}