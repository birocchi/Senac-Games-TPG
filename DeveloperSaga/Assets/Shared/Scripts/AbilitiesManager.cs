using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilitiesManager : MonoBehaviour
{

		public List<Ability> abilitiesList;
	
		public Ability GetAbilityByName (string name)
		{
				Ability abilityToReturn = null;
				if (abilitiesList != null && abilitiesList.Count > 0) {
						foreach (Ability ab in abilitiesList) {
								if (ab.name.Equals (name)) {
										abilityToReturn = ab;
										break;
								}
						}
				}
				return abilityToReturn;
		}
	
		public bool IsAbilityActive (string name)
		{
				bool result = false;
				if (abilitiesList != null && abilitiesList.Count > 0) {
						foreach (Ability ab in abilitiesList) {
								if (ab.name.Equals (name) && ab.active) {
										result = true;
										break;
								}
						}
				}
				return result;
		}

		// Use this for initialization
		void Start ()
		{
				if (abilitiesList == null) {

						abilitiesList = new List<Ability> ();

						if (PlayerPrefs.GetInt ("Pistol.cs") == 1) {
								abilitiesList.Add (new Ability ("Pistol.cs", Ability.AbilityType.WeaponAbility, "iconPath", "Equip/Unequip pistol"));				
						}

						if (PlayerPrefs.GetInt ("Halt.cs") == 1) {
								abilitiesList.Add (new Ability ("Halt.cs", Ability.AbilityType.PowerAbility, "test", "Temporarily halts all enemies", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("PowerShot.cs") == 1) {
								abilitiesList.Add (new Ability ("PowerShot.cs", Ability.AbilityType.PowerAbility, "test", "Enchances pistol power", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("Shield.cs") == 1) {
								abilitiesList.Add (new Ability ("Shield.cs", Ability.AbilityType.PowerAbility, "test", "Halves damage", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("Force.cs") == 1) {
								abilitiesList.Add (new Ability ("Force.cs", Ability.AbilityType.PowerAbility, "test", "Allow object manipulation", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("NullVariables.cs") == 1) {
								abilitiesList.Add (new Ability ("NullVariables.cs", Ability.AbilityType.SpecialAbility, "test", "Destroy all visible enemies"));
						}


						if (abilitiesList.Count <= 0) {
								abilitiesList.Add (new Ability ("Halt.cs", Ability.AbilityType.PowerAbility, "test", "Temporarily halts all enemies", 1000, 2000));
						}						
				}

				foreach (Ability ability in abilitiesList) {
						PlayerPrefs.SetInt (ability.name, 1);
				}			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
