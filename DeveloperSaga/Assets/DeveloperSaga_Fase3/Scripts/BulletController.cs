using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
		public ParticleSystem traceParticles;
		public float damage = 0.5f;
		public Color originalColor;

		// Use this for initialization
		void Start ()
		{
				originalColor = renderer.material.GetColor ("_Emission");
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Player.IsAbilityActive ("PowerShot.cs")) {						
						renderer.material.SetColor ("_Emission", Color.red);
				} else {						
						renderer.material.SetColor ("_Emission", originalColor);
				}
		}

		void OnCollisionEnter (Collision collision)
		{
				Debug.Log ("Collided with: " + collision.gameObject.name);
				if (collision.gameObject.tag.Equals ("Enemy")) {						
						if (Player.IsAbilityActive ("PowerShot.cs")) {
								collision.gameObject.SendMessage ("DoDamage", damage * 3);
						} else {
								collision.gameObject.SendMessage ("DoDamage", damage);
						}
				} else if (collision.gameObject.tag.Equals ("Player")) {
						if (Player.IsAbilityActive ("Shield.cs")) {
								collision.gameObject.SendMessage ("DoDamage", damage / 2);
						} else {
								collision.gameObject.SendMessage ("DoDamage", damage);
						}
				} else if (!name.Equals ("Boss Bullet")) {						
						foreach (ContactPoint c in collision.contacts) {
								if (c.otherCollider.gameObject.name.Equals ("AreaDanoBoss")) {
										if (Player.IsAbilityActive ("PowerShot.cs")) {
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
}
