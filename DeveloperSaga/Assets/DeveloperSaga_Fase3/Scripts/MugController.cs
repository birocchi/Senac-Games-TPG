using UnityEngine;
using System.Collections;

public class MugController : MonoBehaviour
{
		private bool fadeAlpha;
	
		private LifeManager lifeManager;
		
		private bool runned = false;

		// Use this for initialization
		void Start ()
		{
		
				GameObject gameManager = GameObject.Find ("GameManager");
				lifeManager = gameManager.GetComponent<LifeManager> ();
				fadeAlpha = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				this.transform.Rotate (new Vector3 (0, 1 * Time.timeScale, 0));
				if (fadeAlpha && renderer.material.color.a > 0) {
						Color color = renderer.material.color;
						color.a -= 5f * Time.deltaTime;
						renderer.material.color = color;
				}
		}

		public void GetMug ()
		{
				if (!runned) {
						lifeManager.maxLife += 2;
						StartCoroutine (DestroyThis ());
						runned = true;
				}
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
