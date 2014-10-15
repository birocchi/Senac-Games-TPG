using UnityEngine;
using System.Collections;

public class BossTeleporter : MonoBehaviour
{
		public Transform player;
		public Boss3Controller boss;
		public Transform teleportPoint;
		public Transform camera;
		

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
				player.transform.position = teleportPoint.position;
				boss.StartBoss ();
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag.Equals ("Player")) {
						player.transform.position = teleportPoint.position;
						camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 20f);
						boss.StartBoss ();
				}
		}
}
