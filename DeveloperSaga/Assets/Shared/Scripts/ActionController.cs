using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{
		public GUIStyle font;
		public string upText;
		public string downText;
		public Transform target;
		
		public Texture background;
		public Texture joystickAction;
		public Texture keyboardAction;

		private bool showMessage;
		private float alpha = 0f;
		
		private bool shouldMove = false;
		private bool moved = false;

		public bool up = false;
		public Transform goToPoint;
		public Transform startingPoint;
		public Vector3 axis;
		public GameObject toMove;
		public bool shouldGoBack = false;
		public float velocity = 5f;
	
		void Start ()
		{

		}



		void OnGUI ()
		{
				if (showMessage && !shouldMove) {
						string message = "";
						if (up) {
								message = upText;
						} else {
								message = downText;
						}
						
						GUI.skin.label.wordWrap = true;

						//Vector3 position = Camera.main.WorldToScreenPoint (target.position);			
						GUI.DrawTexture (new Rect (Screen.width / 2f - 250f, Screen.height - 185f, 500, 150), background);
						
						//Buttons
						//Detect Joystick
						if (Input.GetJoystickNames ().Length > 0) {
								GUI.DrawTexture (new Rect (Screen.width / 2f - 250f + 110f, Screen.height - 145f, 64, 64), joystickAction);
								GUI.DrawTexture (new Rect (Screen.width / 2f - 250f + 182f, Screen.height - 135f, 48, 48), keyboardAction);
								GUI.Label (new Rect (Screen.width / 2f - 82f, Screen.height - 162f, 200, 180), message, font);
						} else {
								GUI.DrawTexture (new Rect (Screen.width / 2f - 250f + 152f, Screen.height - 135f, 48, 48), keyboardAction);
								GUI.Label (new Rect (Screen.width / 2f - 82f, Screen.height - 162f, 200, 180), message, font);
						}

				}
		}
	
		void LateUpdate ()
		{
				if (showMessage && !shouldMove) {						
						if (Input.GetButton("Action")) {
								moved = false;
								shouldMove = true; 
						}
				}

				if (shouldMove && !moved) {
						float moveValue = velocity * Time.deltaTime;
							
						if (axis.x == 1) {
								if (toMove.transform.position.x < goToPoint.transform.position.x + moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x + moveValue, toMove.transform.position.y, toMove.transform.position.z);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up; 
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						} else if (axis.x == -1) {
								if (toMove.transform.position.x > goToPoint.transform.position.x - moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x - moveValue, toMove.transform.position.y, toMove.transform.position.z);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up;
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						} else if (axis.y == 1) {
								if (toMove.transform.position.y < goToPoint.transform.position.y + moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x, toMove.transform.position.y + moveValue, toMove.transform.position.z);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up;
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						} else if (axis.y == -1) {
								if (toMove.transform.position.y > goToPoint.transform.position.y - moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x, toMove.transform.position.y - moveValue, toMove.transform.position.z);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up;
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						} else if (axis.z == 1) {
								if (toMove.transform.position.z < goToPoint.transform.position.z + moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x, toMove.transform.position.y, toMove.transform.position.z + moveValue);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up;
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						} else if (axis.y == -1) {
								if (toMove.transform.position.z > goToPoint.transform.position.z - moveValue) {
										toMove.transform.position = new Vector3 (toMove.transform.position.x, toMove.transform.position.y, toMove.transform.position.z - moveValue);
										
								} else {
										toMove.transform.position = toMove.transform.position;
										shouldMove = false;
										up = !up;
										axis = axis * -1f;
										Transform temp = goToPoint;
										goToPoint = startingPoint;
										startingPoint = temp;
								}
						}						
				}
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag.Equals ("Player")) {
						showMessage = true;
				}
		}

		void OnTriggerExit (Collider other)
		{
				if (other.gameObject.tag.Equals ("Player")) {
						if (shouldGoBack && up) {
								moved = false;
								shouldMove = true; 
						}
						showMessage = false;
				}
		}

}

