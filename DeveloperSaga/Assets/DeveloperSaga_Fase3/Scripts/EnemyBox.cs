using UnityEngine;
using System.Collections;

public class EnemyBox : MonoBehaviour
{
		public ParticleSystem[] damageParticles;
		private ParticleSystem[] instances = new ParticleSystem[2];

		// Use this for initialization
		void Awake ()
		{
				for (int i = 0; i <= damageParticles.Length - 1; i++) {
						instances [i] = (ParticleSystem)Instantiate (damageParticles [i], transform.position, transform.rotation);
						instances [i].particleSystem.enableEmission = true;						
						instances [i].particleSystem.Play ();
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				for (int i = 0; i <= instances.Length - 1; i++) {
						instances [i].transform.position = transform.position;
				}

		}
}
 