using UnityEngine;
using System.Collections;

public class HoverEnemy : MonoBehaviour
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
				if (Time.timeScale > 0.75 && !abilitiesManager.IsAbilityActive ("Parar.cs")) {
						if (enemyHealth != null && enemyHealth.enemyHealth > 0) {
								if (playerNear) {
						
										if (player.position.x < transform.position.x) {
												this.transform.Translate (new Vector3 (-0.25f, 0, 0));								
										}

										if (player.position.x > transform.position.x) {
												this.transform.Translate (new Vector3 (0.25f, 0, 0));								
										}

										if (player.position.y + 30f > transform.position.y) {
												this.transform.Translate (new Vector3 (0, 0.25f, 0));								
										}

										if (player.position.y + 30f < transform.position.y) {
												this.transform.Translate (new Vector3 (0, -0.25f, 0));								
										}

										if (!isShooting) {
												GameObject instance = (GameObject)Instantiate (bullet, ponto.position, ponto.rotation);
												BulletController cont = (BulletController)instance.GetComponent <BulletController> ();
												cont.damage = 1;
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
