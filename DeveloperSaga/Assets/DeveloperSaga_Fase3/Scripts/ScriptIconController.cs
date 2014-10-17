using UnityEngine;
using System.Collections;

public class ScriptIconController : MonoBehaviour
{
		private bool fadeAlpha;
		private AbilitiesManager abilitiesManager;
		public string abilityName;
		public Ability.AbilityType abilityType;
		public string abilityDescription;
		public int abilityDuration;
		public int abilityCooldown;
	
		// Use this for initialization
		void Start ()
		{
		
				GameObject gameManager = GameObject.Find ("GameManager");
				abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();
				fadeAlpha = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				this.transform.Rotate (new Vector3 (0, 0, 1 * Time.timeScale));
				if (fadeAlpha && renderer.material.color.a > 0) {
						Color color = renderer.material.color;
						color.a -= 5f * Time.deltaTime;
						renderer.material.color = color;
				}
		}
	
		public void GetAbility ()
		{
				abilitiesManager.abilitiesList.Add (new Ability (abilityName, abilityType, null, abilityDescription, abilityDuration, abilityCooldown));
				StartCoroutine (DestroyThis ());
		}
	
	
	
		IEnumerator DestroyThis ()
		{	
				fadeAlpha = true;
		
				this.particleSystem.enableEmission = false;
		
				this.collider.enabled = false;
				yield return new WaitForSeconds (1f);
		
				Destroy (this, 7f);
		}
}
