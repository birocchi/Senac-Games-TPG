using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour
{
		public float speed = 2.5f;	
		public float jumpSpeed = 1.0f;
		private float vSpeed = 0f;
		private float gravity = 125f;
		private bool back = false;
		private float toRotate;
		private Animator animator2;
		public GameObject playerAvatar;
		private CharacterController controller;

		// Use this for initialization
		void Start ()
		{
				controller = GetComponent<CharacterController> ();
				animator2 = playerAvatar.GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				Vector3 moveDirection;				
				moveDirection = new Vector3 (0, 0, Input.GetAxis ("Horizontal") * -1);
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				if (controller.isGrounded) {
						vSpeed = -2;
						if (Input.GetButtonDown ("Jump")) {
								vSpeed = jumpSpeed;
						}
				}
				vSpeed -= gravity * Time.deltaTime;
				moveDirection.y = vSpeed;
				controller.Move (moveDirection * Time.deltaTime);
				
				if (Input.GetAxis ("Horizontal") < 0 && !back) {
						toRotate++;
						if (toRotate <= 20) {
								playerAvatar.transform.Rotate (new Vector3 (0, 9, 0));
						} else {
								back = true;
								toRotate = 0;
						}					
						
				} else if (Input.GetAxis ("Horizontal") > 0 && back) {
						toRotate--;
						if (toRotate >= -20) {
								playerAvatar.transform.Rotate (new Vector3 (0, -9, 0));
						} else {
								back = false;
								toRotate = 0;
						}					
				}
				
				if (Input.GetButtonDown ("Jump")) {
						animator2.SetBool ("jump", true);		
				}
				if (controller.isGrounded) {
						animator2.SetBool ("jump", false);					
				}
				
				if (Input.GetAxis ("Horizontal") > 0.1f || Input.GetAxis ("Horizontal") < -0.1f) {			
						animator2.SetBool ("run", true);
				} else {
						animator2.SetBool ("run", false);		
				}
		}
	 
		void OnControllerColliderHit (ControllerColliderHit hit)
		{
				if (hit.collider.gameObject.tag.Equals ("Enemy") && Time.timeScale >= 0.75f) {
						Player.energia -= 0.25f;
				}
				if (hit.collider.gameObject.tag.Equals ("Coin")) {	
						hit.collider.gameObject.SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
				}
		}

		void OnTriggerEnter (Collider trigger)
		{
				if (trigger.collider.gameObject.tag.Equals ("Enemy") && Time.timeScale >= 0.75f) {
						Player.energia -= 0.25f;
				}
				if (trigger.collider.gameObject.tag.Equals ("Coin")) {	
						SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
						trigger.collider.gameObject.SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
				}
		}

}
