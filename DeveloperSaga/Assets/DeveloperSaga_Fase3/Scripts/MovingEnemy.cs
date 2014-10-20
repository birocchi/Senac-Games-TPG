using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour
{
	
		public static bool shouldMove = true;
		public Transform ponto;
		public GameObject bullet;
		private bool playerNear = false;
		private bool isShooting = false;
		public Transform player;
		public bool lookingLeft = true;
		public bool shouldRotate = false;
		public EnemyHealthController enemyHealth;
		private AbilitiesManager abilitiesManager;
		public int damage = 1;
		

		// Use this for initialization
		void Start ()
		{
				GameObject gameManager = GameObject.Find ("GameManager");
				abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();
	
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (Time.timeScale > 0.75 && !abilitiesManager.IsAbilityActive ("Parar.cs") && shouldMove) {
						if (enemyHealth != null && enemyHealth.enemyHealth > 0) {
								if (playerNear) {
						
										if (player.position.x <= transform.position.x - 40f || player.position.x > transform.position.x + 40f) {
												this.transform.Translate (new Vector3 (0, 0, 0.25f));								
										}

										if (player.position.y + 30f > transform.position.y) {
												this.transform.Translate (new Vector3 (0, 0.25f, 0));								
										}

										Quaternion rotation = Quaternion.LookRotation (new Vector3 (player.position.x, player.position.y + 10f, player.position.z) - transform.position);
										transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 2f);

										if (!isShooting) {
												GameObject instance = (GameObject)Instantiate (bullet, ponto.position, ponto.rotation);
												BulletController cont = (BulletController)instance.GetComponent <BulletController> ();
												cont.damage = damage;
												instance.rigidbody.velocity = ponto.up * -100;
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
				yield return new WaitForSeconds (1f);
				isShooting = false;

		}		

		
}
