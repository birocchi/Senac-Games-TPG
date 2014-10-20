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

		public bool HetAbility (string name)
		{
				bool result = false;
				if (abilitiesList != null && abilitiesList.Count > 0) {
						foreach (Ability ab in abilitiesList) {
								if (ab.name.Equals (name)) {
										result = true;
										break;
								}
						}
				}
				return result;
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

						if (PlayerPrefs.GetInt ("Pistola.cs") == 1) {
								abilitiesList.Add (new Ability ("Pistola.cs", Ability.AbilityType.WeaponAbility, "iconPath", "Equipa/Remove a arma"));				
						}

						if (PlayerPrefs.GetInt ("Parar.cs") == 1) {
								abilitiesList.Add (new Ability ("Parar.cs", Ability.AbilityType.PowerAbility, "test", "Temporariamente pausa os inimigos", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("SuperTiro.cs") == 1) {
								abilitiesList.Add (new Ability ("SuperTiro.cs", Ability.AbilityType.PowerAbility, "test", "Aumenta o poder da pistola", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("Escudos.cs") == 1) {
								abilitiesList.Add (new Ability ("Escudos.cs", Ability.AbilityType.PowerAbility, "test", "Diminui o dano pela metade", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("Forca.cs") == 1) {
								abilitiesList.Add (new Ability ("Forca.cs", Ability.AbilityType.PowerAbility, "test", "Permite manipular objetos", 1000, 2000));
						}

						if (PlayerPrefs.GetInt ("ZerarVariaveis.cs") == 1) {
								abilitiesList.Add (new Ability ("ZerarVariaveis.cs", Ability.AbilityType.SpecialAbility, "test", "Destroi todos os inimigos visiveis"));
						}

						if (PlayerPrefs.GetInt ("CafeExpresso.cs") == 1) {
								abilitiesList.Add (new Ability ("CafeExpresso.cs", Ability.AbilityType.SpecialAbility, "test", "Recupera dois cafes", 100, 5000));
						}


						if (abilitiesList.Count <= 0) {
								abilitiesList.Add (new Ability ("Parar.cs", Ability.AbilityType.PowerAbility, "test", "Temporariamente pausa os inimigos", 1000, 2000));
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
