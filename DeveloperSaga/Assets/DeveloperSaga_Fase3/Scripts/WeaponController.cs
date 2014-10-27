using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{	
		public static bool enabled = true;
		private Animator animator;
		public Transform pontoArmaFrente;	
		public Transform pontoArmaDiagonal;	
		public Transform pontoArmaCima;
		public Transform pontoAtivo;
		public GameObject arma;
		public Transform pontoMira;
		public float weight;
		public GameObject bullet;
		

		// Use this for initialization
		void Start ()
		{
				animator = this.GetComponent<Animator> ();
				pontoAtivo = pontoArmaFrente;
		}
	
		// Update is called once per frame
		void  Update ()
		{
				if (Time.timeScale > 0.75 && enabled) {
						if (Input.GetButtonDown ("Fire1")) {
								Shot ();
						}
				}
		}

		void Shot ()
		{
				GameObject instance = (GameObject)Instantiate (bullet, pontoMira.position, pontoMira.rotation);
				BulletController bc = instance.GetComponent<BulletController> ();
				bc.damage = 1;
				instance.rigidbody.velocity = pontoMira.up * -5;
				Destroy (instance, 10f);
		}

		void OnAnimatorIK (int layerIndex)
		{
				

				if (animator != null && pontoAtivo != null && enabled) {
						if (Input.GetButton ("Fire1")) {
								if (Time.timeScale > 0.75) {
										if (Input.GetAxis ("Vertical") > 0 && Input.GetAxis ("Horizontal") == 0f) {
												pontoAtivo = pontoArmaCima;	
										} else if (Input.GetAxis ("Vertical") > 0 && Input.GetAxis ("Horizontal") != 0f) {
												pontoAtivo = pontoArmaDiagonal;
										} else if (Input.GetAxis ("Vertical") <= 0) {
												pontoAtivo = pontoArmaFrente;
										}
										weight += 0.1f;
										if (weight >= 10f) {
												weight = 1f;
										}
								}
						} else {
								
								weight -= 0.05f;
								if (weight <= 0f) {
										pontoAtivo = pontoArmaFrente;
										weight = 0f;
								}
						}
						
						
				
						animator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1f);
						animator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1f);	
						if (pontoArmaFrente != null) {
								
								animator.SetIKPositionWeight (AvatarIKGoal.RightHand, weight);
								animator.SetIKPosition (AvatarIKGoal.RightHand, pontoAtivo.position);
								animator.SetIKRotation (AvatarIKGoal.RightHand, pontoAtivo.rotation);
						}
				}
				
		}
}