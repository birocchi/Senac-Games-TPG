using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private bool fadeAlpha;

	// Use this for initialization
	void Start () {
		fadeAlpha = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 1 * Time.timeScale, 0));
		if (fadeAlpha && renderer.material.color.a > 0) {
			Color color = renderer.material.color;
			color.a -= 0.1f;
			renderer.material.color = color;
		}
	}

	public void GetCoin() {
		Player.moedas++;
		StartCoroutine (DestroyThis ());
	}


	
	IEnumerator DestroyThis()
	{	
		fadeAlpha = true;

		this.particleSystem.enableEmission = false;
		
		this.collider.enabled = false;
		yield return new WaitForSeconds (1f);

		Destroy (this);
	}
}
