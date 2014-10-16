using UnityEngine;
using System.Collections;

public class GroundedEnemy : MonoBehaviour
{
		
		public Transform ponto;
		public GameObject bullet;
		private bool playerNear = false;
		private bool isShooting = false;
		public Transform player;
		public bool lookingLeft = true;
		public bool shouldRotate = false;
		public EnemyHealthController enemyHealth;
		private AbilitiesManager abilitiesManager;
		

		// Use this for initialization
		void Start ()
		{
				GameObject gameManager = GameObject.Find ("GameManager");
				abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();
	
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (Time.timeScale > 0.75 && !abilitiesManager.IsAbilityActive ("Halt.cs")) {
						if (enemyHealth != null && enemyHealth.enemyHealth > 0) {
								if (playerNear) {
						
										if (player.position.x <= transform.position.x - 40f || player.position.x > transform.position.x + 40f) {
												this.transform.Translate (new Vector3 (0, 0, 0.25f));								
										}

										Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
										transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 2f);

										if (!isShooting) {
												GameObject instance = (GameObject)Instantiate (bullet, ponto.position, ponto.rotation);
												BulletController cont = (BulletController)instance.GetComponent <BulletController> ();
												cont.damage = 1;
												instance.rigidbody.velocity = ponto.up * 100f;
												Destroy (instance, 5f);
												StartCoroutine (WaitToFireAgain ());
										}
								}
						}
				}
		}

		void PlayerNear ()
		{
				playerNear = true;
		}

		void PlayerNotNear ()
		{
				playerNear = false;
		}

		IEnumerator WaitToFireAgain ()
		{
				isShooting = true;
				yield return new WaitForSeconds (2f);
				isShooting = false;

		}		

		
}
