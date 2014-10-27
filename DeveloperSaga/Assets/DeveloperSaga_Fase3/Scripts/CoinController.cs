using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
		private bool fadeAlpha;
	
		private CoinManager coinManager;

		// Use this for initialization
		void Start ()
		{
		
				GameObject gameManager = GameObject.Find ("GameManager");
				coinManager = gameManager.GetComponent<CoinManager> ();
				fadeAlpha = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				this.transform.Rotate (new Vector3 (0, 1 * Time.timeScale, 0));
				if (fadeAlpha && renderer.material.color.a > 0) {
						Color color = renderer.material.color;
						color.a -= 10f * Time.deltaTime;
						renderer.material.color = color;
				}
		}

		public void GetCoin ()
		{
				coinManager.numberOfCoins++;
				StartCoroutine (DestroyThis ());
		}


	
		IEnumerator DestroyThis ()
		{	
				fadeAlpha = true;

				this.particleSystem.enableEmission = false;
		
				this.collider.enabled = false;
				yield return new WaitForSeconds (1f);

				Destroy (this);
		}
}
