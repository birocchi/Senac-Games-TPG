using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
		public ParticleSystem traceParticles;
		public int damage = 1;
		public Color originalColor;
		private AbilitiesManager abilitiesManager;

		// Use this for initialization
		void Start ()
		{
				GameObject gameManager = GameObject.Find ("GameManager");
				abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();
				originalColor = renderer.material.GetColor ("_Emission");
		
				Destroy (gameObject, 10f);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (abilitiesManager.IsAbilityActive ("SuperTiro.cs")) {						
						renderer.material.SetColor ("_Emission", Color.red);
				} else {						
						renderer.material.SetColor ("_Emission", originalColor);
				}
		}

		void OnCollisionEnter (Collision collision)
		{
				if (collision.gameObject.tag.Equals ("Enemy")) {						
						if (abilitiesManager.IsAbilityActive ("SuperTiro.cs")) {
								collision.gameObject.SendMessage ("DoDamage", damage * 3);
						} else {
								collision.gameObject.SendMessage ("DoDamage", damage);
						}
				} else if (collision.gameObject.tag.Equals ("Player")) {
						if (abilitiesManager.IsAbilityActive ("Escudos.cs")) {
								collision.gameObject.SendMessage ("DoDamage", damage / 2);
						} else {
								collision.gameObject.SendMessage ("DoDamage", damage);
						}
				} else if (!name.Equals ("Boss Bullet")) {						
						foreach (ContactPoint c in collision.contacts) {
								if (c.otherCollider.gameObject.name.Equals ("AreaDanoBoss")) {
										if (abilitiesManager.IsAbilityActive ("SuperTiro.cs")) {
												collision.gameObject.SendMessage ("DoDamage", damage * 3);
										} else {
												collision.gameObject.SendMessage ("DoDamage", damage);
										}
								}
						}						
				}
				this.traceParticles.enableEmission = false;
				this.traceParticles.Stop ();
				this.renderer.enabled = false;
				this.rigidbody.detectCollisions = false;
				this.collider.enabled = false;

				Destroy (gameObject, 2);
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag.Equals ("Enemy")) {						
						if (abilitiesManager.IsAbilityActive ("SuperTiro.cs")) {
								other.gameObject.SendMessage ("DoDamage", damage * 3);
						} else {
								other.gameObject.SendMessage ("DoDamage", damage);
						}
						this.traceParticles.enableEmission = false;
						this.traceParticles.Stop ();
						this.renderer.enabled = false;
						this.rigidbody.detectCollisions = false;
						this.collider.enabled = false;
			
						Destroy (gameObject, 2);
				} else if (other.gameObject.tag.Equals ("Player")) {
						if (abilitiesManager.IsAbilityActive ("Escudos.cs")) {
								other.gameObject.SendMessage ("DoDamage", damage / 2);
						} else {
								other.gameObject.SendMessage ("DoDamage", damage);
						}
						this.traceParticles.enableEmission = false;
						this.traceParticles.Stop ();
						this.renderer.enabled = false;
						this.rigidbody.detectCollisions = false;
						this.collider.enabled = false;
			
						Destroy (gameObject, 2);
				} else if (!name.Equals ("Boss Bullet")) {	
						if (other.gameObject.name.Equals ("AreaDanoBoss")) {
								if (abilitiesManager.IsAbilityActive ("SuperTiro.cs")) {
										other.gameObject.SendMessage ("DoDamage", damage * 3);
								} else {
										other.gameObject.SendMessage ("DoDamage", damage);
								}
								this.traceParticles.enableEmission = false;
								this.traceParticles.Stop ();
								this.renderer.enabled = false;
								this.rigidbody.detectCollisions = false;
								this.collider.enabled = false;
						}
				}
				if (other.gameObject.tag.Equals ("StaticEnemy")) {												
						this.traceParticles.enableEmission = false;
						this.traceParticles.Stop ();
						this.renderer.enabled = false;
						this.rigidbody.detectCollisions = false;
						this.collider.enabled = false;
				
						Destroy (gameObject, 2);
				} else if (!other.gameObject.tag.Equals ("EnemyArea")) {
						if (other.gameObject.renderer != null && other.gameObject.renderer.enabled) {
								this.traceParticles.enableEmission = false;
								this.traceParticles.Stop ();
								this.renderer.enabled = false;
								this.rigidbody.detectCollisions = false;
								this.collider.enabled = false;
			
								Destroy (gameObject, 2);
						}
				}
		}
}
