using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour
{
		public GameObject mainObj;
		public float enemyHealth = 3f;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (enemyHealth <= 0) {
						Destroy (mainObj);
				}
		}

		void OnCollisionEnter (Collision collision)
		{
		
				if (collision.gameObject.tag.Equals ("PlayerBullet"))
						enemyHealth--;
		
		}
}
