using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
		public static int energiaTotal = Constants.ENERGIA_TOTAL_INICIAL;
		public static int moedasTotal = Constants.MOEDAS_TOTAL_INICIAL;
		public static float energia = energiaTotal;
		public static int moedas = Constants.MOEDAS_INICIAL;
		public static List<Ability> listaHabilidades = new List<Ability> ();

		public static Ability GetAbilityByName (string name)
		{
				Ability abilityToReturn = null;
				if (listaHabilidades != null && listaHabilidades.Count > 0) {
						foreach (Ability ab in listaHabilidades) {
								if (ab.name.Equals (name)) {
										abilityToReturn = ab;
										break;
								}
						}
				}
				return abilityToReturn;
		}

		public static bool IsAbilityActive (string name)
		{
				bool result = false;
				if (listaHabilidades != null && listaHabilidades.Count > 0) {
						foreach (Ability ab in listaHabilidades) {
								if (ab.name.Equals (name) && ab.active) {
										result = true;
										break;
								}
						}
				}
				return result;
		}
}
