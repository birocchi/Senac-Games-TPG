using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowMessage : MonoBehaviour
{
		private bool messageShowing = false;
		public List<Message> messageList = new List<Message> (1);
		public List<Message> firstMessage = new List<Message> (1);
		private Message messageToShow = null;
		private bool endMessages = false;
		private bool showMGUI = false;
		public string name;	
		public GUIStyle font = new GUIStyle ();

	
		//This is our custom class with our variables
		[System.Serializable]
		public class Message
		{
				public string messageText;
				public Transform transform;
				public float duration;
				public Texture texture = new Texture ();
		}
	
		void AddNew ()
		{
				//Add a new index position to the end of our list
				messageList.Add (new Message ());
		}
	
		void Remove (int index)
		{
				//Remove an index position from our list at a point in our list array
				messageList.RemoveAt (index); 
		}
	
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!messageShowing && showMGUI) {
						{
								messageShowing = true;
				
								StartCoroutine (ShowDialogue ());
						}
				}
		}
	
		private IEnumerator ShowDialogue ()
		{

				foreach (Message message in messageList) {
						messageToShow = message;
						showMGUI = true;
						yield return new WaitForSeconds (messageToShow.duration);
						showMGUI = false;
						messageToShow = null;
			
				}
		
				messageToShow = null;
				endMessages = true;

				this.collider.enabled = false;

				Destroy (this, 5);
		}
	
		void OnTriggerEnter (Collider other)
		{				
				if (other.collider.gameObject.tag.Equals ("Player")) {
						showMGUI = true;
				}
		}
	
		void OnGUI ()
		{
				if (showMGUI && messageToShow != null) {
						Vector3 pos = Camera.main.WorldToScreenPoint (messageToShow.transform.position);
						GUI.DrawTexture (new Rect (pos.x, pos.y + 25f, messageToShow.texture.width, messageToShow.texture.height), messageToShow.texture);
						GUI.skin.label.wordWrap = true;
						GUIStyle fontToUse = font;
						GUI.Label (new Rect (pos.x, pos.y + 35f, messageToShow.texture.width - 50, messageToShow.texture.height - 50), messageToShow.messageText, fontToUse);
				}
		}
}
