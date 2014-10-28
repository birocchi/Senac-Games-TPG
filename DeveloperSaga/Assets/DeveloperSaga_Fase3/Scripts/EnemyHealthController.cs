using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour
{
		public GameObject mainObj;
		public float enemyHealth = 3f;
		public float demageFlashTimes = 3f;
		public GameObject[] blinking;
		public ParticleSystem[] particleSystems;
		public ParticleSystem explosionParticles0;
		private bool enemyDead = false;
		public Transform explosionPoint;
		public Transform center;
		public Light light;
		private ParticleSystem instance0;
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{					
				if (!enemyDead) {
						if (enemyHealth <= 0) {	
								instance0 = (ParticleSystem)Instantiate (explosionParticles0, new Vector3 (explosionPoint.position.x, explosionPoint.position.y, explosionPoint.position.z), transform.rotation);

								foreach (ParticleSystem ps in particleSystems) {
										ps.enableEmission = false;
										ps.Stop ();
								}

								light.intensity = 0f;								

								StartCoroutine (DisableRenderer ());
								enemyDead = true;				
								collider.enabled = false;
								Destroy (mainObj, 4f);
						}
				}
		}

		public void DoDamage (float damage)
		{
				enemyHealth -= damage;
				StartCoroutine (CollideFlash ());
		}

		IEnumerator DisableRenderer ()
		{						
				yield return new WaitForSeconds (0.15f);
		
				foreach (GameObject go in blinking) {
						go.renderer.enabled = false;
				}
				
		}

		IEnumerator CollideFlash ()
		{		
				if (!enemyDead) {
						for (int i = 0; i <= demageFlashTimes; i++) {
								foreach (GameObject go in blinking) {
										go.renderer.enabled = false;
								}

								yield return new WaitForSeconds (0.025f);

								if (!enemyDead) {
										foreach (GameObject go in blinking) {
												go.renderer.enabled = true;
										}
								}
								
								yield return new WaitForSeconds (0.025f);
						}
				}
		}

}
