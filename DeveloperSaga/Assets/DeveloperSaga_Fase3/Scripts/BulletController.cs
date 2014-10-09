using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
		public ParticleSystem traceParticles;
		public float damage = 0.25f;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnCollisionEnter (Collision collision)
		{
				Debug.Log ("Collided with: " + collision.gameObject.tag);
				if (collision.gameObject.tag.Equals ("Player") || collision.gameObject.tag.Equals ("Enemy")) {
						collision.gameObject.SendMessage ("DoDamage", damage);
				}
				this.traceParticles.enableEmission = false;
				this.traceParticles.Stop ();
				this.renderer.enabled = false;
				this.rigidbody.detectCollisions = false;
				this.collider.enabled = false;

				Destroy (gameObject, 2);
		}
}
