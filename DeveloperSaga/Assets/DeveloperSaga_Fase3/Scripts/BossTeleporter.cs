using UnityEngine;
using System.Collections;

public class BossTeleporter : MonoBehaviour
{
		public Transform player;
		public Boss3Controller boss;
		public GameObject bossGB;
		public Transform teleportPoint;
		public Transform camera;
		private bool moveCamera = false;
		private int i = 0;
		private bool executed = false;

		

		// Use this for initialization
		void Start ()
		{
				Renderer[] rs = boss.GetComponentsInChildren<Renderer> ();
				foreach (Renderer r in rs)
						r.enabled = false;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				if (moveCamera && i <= 100) {						
						camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 0.25f);
						i++;
				}
				
		}

		void OnCollisionEnter (Collision collision)
		{
				boss.StartBoss ();
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag.Equals ("Player") && !executed) {
						moveCamera = true;
						Renderer[] rs = boss.GetComponentsInChildren<Renderer> ();
						foreach (Renderer r in rs)
								r.enabled = true;
						boss.StartBoss ();
						Destroy (gameObject, 5);
						executed = true;
				}
		}
}
