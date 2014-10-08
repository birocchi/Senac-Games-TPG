﻿using UnityEngine;
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
		private bool beingHit = false;
		private bool colored = false;
		private float blinkingColorValue = 0f;
		public GameObject blinking1;
		public GameObject blinking2;
		private bool rotateLeft = false;
		private bool rotateRight = false;
		public int demageFlashTimes;
		public AudioClip hit;
		public AudioClip ah;
		private bool invincible = false;
		public GameObject[] damageParticles;
		private GameObject[] damageInstances;
		public Transform damagePosition;
		public GameObject camera;
		public bool rotate = false;
		public bool rotated = false;
		public float rotation = 0f;
	
		
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
				
				if (Input.GetAxis ("Horizontal") * -1 < 0 && !back) {
						rotateLeft = true;
									
						
				} else if (Input.GetAxis ("Horizontal") * -1 > 0 && back) {
						rotateRight = true;
				}
				
				if (rotateLeft) {
						toRotate++;
						if (toRotate <= 20) {
								playerAvatar.transform.Rotate (new Vector3 (0, -9, 0));
						} else {
								back = true;
								rotateLeft = false;
								toRotate = 0;
						}		
				} else if (rotateRight) {
						toRotate--;
						if (toRotate >= -20) {
								playerAvatar.transform.Rotate (new Vector3 (0, 9, 0));
						} else {
								back = false;
								rotateRight = false;
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
				
				if (Input.GetButton ("Fire3")) {
							
				}
				
		}
		
		void FixedUpdate ()
		{
				if (rotate && !rotated) {
						rotation++;
						if (rotation <= 10) {
								camera.transform.RotateAround (this.transform.position, new Vector3 (0, -9, 0), 9);
								this.transform.Rotate (new Vector3 (0, -9, 0));
						} else {
								rotated = true;
								rotate = false;
								rotation = 0f;
						}
				}
		}
	 
		void OnControllerColliderHit (ControllerColliderHit hit)
		{
				Debug.Log ("Collision hit: " + hit.gameObject.tag);
				if (hit.collider.gameObject.tag.Equals ("Enemy") || hit.collider.gameObject.tag.Equals ("Bullet") && Time.timeScale >= 0.75f) {
						if (!invincible) {
								Player.energia -= 0.5f;
								StartCoroutine (CollideFlash ());
						}
				}
				if (hit.collider.gameObject.tag.Equals ("Coin")) {	
						hit.collider.gameObject.SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
				}
				if (hit.collider.gameObject.tag.Equals ("MovingEnemy")) {	
						
						hit.collider.gameObject.SendMessage ("PlayerNear");
				}

		}

		void OnTriggerEnter (Collider trigger)
		{
				if (trigger.collider.gameObject.tag.Equals ("Enemy") || trigger.collider.gameObject.tag.Equals ("Bullet") && Time.timeScale >= 0.75f) {
						if (!invincible) {
								Player.energia -= 0.5f;
								StartCoroutine (CollideFlash ());
						}
				}
				if (trigger.collider.gameObject.tag.Equals ("Coin")) {	
						SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
						trigger.collider.gameObject.SendMessage ("GetCoin", SendMessageOptions.DontRequireReceiver);
				}
				if (trigger.collider.gameObject.tag.Equals ("MovingEnemy")) {
						
						trigger.collider.gameObject.SendMessage ("PlayerNear");
				}
		}
	
		IEnumerator CollideFlash ()
		{		
				for (int i = 0; i <= damageParticles.Length - 1; i++) {
						damageParticles [i].particleSystem.enableEmission = true;						
						damageParticles [i].particleSystem.Play ();
				}
				
				TocarSomNumPonto (hit, this.transform.position, 0.2f, 0.5f);
				TocarSomNumPonto (ah, this.transform.position, 1, 1f);
				for (int i = 0; i <= demageFlashTimes; i++) {
						invincible = true;
						blinking1.renderer.enabled = false;
						blinking2.renderer.enabled = false;
						yield return new WaitForSeconds (0.025f);
						blinking1.renderer.enabled = true;
						blinking2.renderer.enabled = true;
						yield return new WaitForSeconds (0.025f);
				}
				
				invincible = false;
				
				for (int i = 0; i <= damageParticles.Length - 1; i++) {
						damageParticles [i].particleSystem.enableEmission = false;
						damageParticles [i].particleSystem.Stop ();					
				}
		}
		
		private void TocarSomNumPonto (AudioClip clip, Vector3 position, float volume, float pitch)
		{
				GameObject obj = new GameObject ();
				obj.name = "Sound Clip";
				obj.transform.position = position;
				obj.AddComponent<AudioSource> ();
				obj.audio.pitch = pitch;
				obj.audio.PlayOneShot (clip, volume);
				Destroy (obj, clip.length / pitch);
		}
}
