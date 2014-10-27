using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{
		public GUIStyle font;
		public string upText;
		public string downText;
		public Transform target;
		public Texture lockImage;
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
						Vector3 position = Camera.main.WorldToScreenPoint (target.position);			
						GUI.DrawTexture (new Rect (position.x, position.y + 25f, 250, 203), lockImage);
						GUI.skin.label.wordWrap = true;
						GUI.Label (new Rect (position.x, position.y + 35f, 200, 180), message, font);
				}
		}
	
		void LateUpdate ()
		{
				if (showMessage && !shouldMove) {						
						if ((up && Input.GetKeyUp (KeyCode.UpArrow)) || (!up && Input.GetKeyUp (KeyCode.DownArrow))) {
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

