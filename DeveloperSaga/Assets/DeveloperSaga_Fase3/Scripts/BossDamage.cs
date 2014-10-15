using UnityEngine;
using System.Collections;

public class BossDamage : MonoBehaviour
{
		public float damage = 1f;
		public GameObject boss;
	
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
				if (collision.gameObject.tag.Equals ("Player")) {
						if (Player.IsAbilityActive ("Shield.cs")) {
								collision.gameObject.SendMessage ("DoDamage", 1f / 2f);
						} else {
								collision.gameObject.SendMessage ("DoDamage", 1f);
						}
				}
		}


		public void DoDamage (float damage)
		{
				boss.SendMessage ("DoDamage", damage);
		}

}
