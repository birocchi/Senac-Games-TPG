using UnityEngine;
using System.Collections;

public class FimFase : MonoBehaviour
{

		public string levelToLoad;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKey (KeyCode.LeftControl)) {
				if (Input.GetKey (KeyCode.LeftAlt)) {
					if (Input.GetKey (KeyCode.F12)) {	
						CheckpointManager.ClearCheckpoint();
						Application.LoadLevel (levelToLoad);
					}	
				}
			}
		}

		void OnCollisionEnter2D (Collision2D coll)
		{
				if (coll.gameObject.tag == "Player") 
						CheckpointManager.ClearCheckpoint();
						Application.LoadLevel (levelToLoad);
		
		}

		void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") 
				CheckpointManager.ClearCheckpoint();
				Application.LoadLevel (levelToLoad);
			
		}
}
