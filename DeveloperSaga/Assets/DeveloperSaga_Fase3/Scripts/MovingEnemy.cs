using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour
{
		
		public Transform ponto;
		public GameObject bullet;
		private bool playerNear = false;
		private bool isShooting = false;
		public Transform player;
		public bool lookingLeft = true;
		public bool shouldRotate = false;
		
		

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				
				
				if (playerNear) {
						
						if (player.position.x <= transform.position.x - 40f || player.position.x > transform.position.x + 40f) {
								this.transform.Translate (new Vector3 (0, 0.25f, 0.25f));								
						}
						this.transform.LookAt (player);

						if (!isShooting) {
								GameObject instance = (GameObject)Instantiate (bullet, ponto.position, ponto.rotation);
								instance.rigidbody.velocity = ponto.up * -100;
								Destroy (instance, 10f);
								StartCoroutine (WaitToFireAgain ());
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
