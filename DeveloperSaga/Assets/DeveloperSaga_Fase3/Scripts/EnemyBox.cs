using UnityEngine;
using System.Collections;

public class EnemyBox : MonoBehaviour
{
		public ParticleSystem[] damageParticles;

		// Use this for initialization
		void Start ()
		{
				for (int i = 0; i <= damageParticles.Length - 1; i++) {
						Instantiate (damageParticles [i], transform.position, transform.rotation);
						damageParticles [i].particleSystem.enableEmission = true;						
						damageParticles [i].particleSystem.Play ();
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
