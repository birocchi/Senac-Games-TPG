using UnityEngine;
using System.Collections;

public class GetCoin : MonoBehaviour
{
	
		private CoinManager coinManager;

		void Start ()
		{
				coinManager = GameObject.Find ("GameManager").GetComponent<CoinManager> ();
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag.Equals ("Player")) {
						coinManager.numberOfCoins++;
						AudioSource.PlayClipAtPoint (audio.clip, transform.position);
						Destroy (gameObject);
				}
		}
}