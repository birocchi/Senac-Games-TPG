using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEvents : MonoBehaviour
{
		public GameObject boss;
		private bool enableAnimation = false;
		private Animator bossAnimator;
		private bool messageShowing = false;
		public List<Message> messageList = new List<Message> (1);
		public Message messageToShow = null;
		public bool endMessages = false;
		private bool showMGUI = false;
		public ParticleSystem transformationParticles;
		public bool bossExiting = false;
		public Transform explosionPosition;
		public Light[] lights;
		public Material[] materialToChange;
		public GameObject[] thunders;

		//This is our custom class with our variables
		[System.Serializable]
		public class Message
		{
				public string messageText;
				public Transform transform;
				public float duration;
				public Texture texture = new Texture ();
				public GUIStyle font = new GUIStyle ();
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
				bossAnimator = boss.GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!messageShowing) {
						AnimatorStateInfo state = bossAnimator.GetCurrentAnimatorStateInfo (0);
						if (state.IsName ("Event Boss Floating")) {								
								StartCoroutine (ShowDialogue ());
								messageShowing = true;
						}
				}
				if (endMessages && !bossExiting) {
						bossExiting = true;
						StartCoroutine (ExitBoss ());
						
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

		
				yield return new WaitForSeconds (0.5f);

				messageToShow = null;
				endMessages = true;
		}

		private IEnumerator ExitBoss ()
		{

				
				bossAnimator.SetBool ("goaway", true);
				yield return new WaitForSeconds (1.5f);
				ParticleSystem o = (ParticleSystem)Instantiate (transformationParticles, explosionPosition.position, explosionPosition.rotation);
		
				yield return new WaitForSeconds (0.2f);
		
				RenderSettings.skybox.SetColor ("_Tint", new Color (0f, 0.2f, 0f, 1f));

				foreach (Light light in lights) {
						light.intensity = light.intensity / 3;
				}


				foreach (Material mat in materialToChange) {
						mat.SetColor ("_Color", new Color (0f, 0.2f, 0f, 1f));
				}
				
				foreach (GameObject thunder in thunders) {
						thunder.SetActive (true);
				}

				
				Destroy (o, 5);
				Destroy (this, 5);
		
				yield return new WaitForSeconds (1f);

				SongController.songToPlay = 3;
		
				CharController.halt = false;
				GUIController.halt = false;
				MovingEnemy.shouldMove = true;
				GroundedEnemy.shouldMove = true;
				HoverEnemy.shouldMove = true;
				WeaponController.enabled = true;
				
		}

		void OnTriggerEnter (Collider other)
		{				
				if (other.collider.gameObject.tag.Equals ("Player")) {		
						SongController.songToPlay = 1;
						boss.SetActive (true);
						CharController.halt = true;
						GUIController.halt = true;
						MovingEnemy.shouldMove = false;
						GroundedEnemy.shouldMove = false;
						HoverEnemy.shouldMove = false;			
						WeaponController.enabled = false;
				}
		}
	 
		void OnGUI ()
		{
				if (showMGUI) {
						Vector3 pos = Camera.main.WorldToScreenPoint (messageToShow.transform.position);
						GUI.DrawTexture (new Rect (pos.x, pos.y, messageToShow.texture.width, messageToShow.texture.height), messageToShow.texture);
						GUI.skin.label.wordWrap = true;
						GUI.Label (new Rect (pos.x, pos.y + 20, messageToShow.texture.width - 100, messageToShow.texture.height - 50), messageToShow.messageText, messageToShow.font);
				}
		}
}
