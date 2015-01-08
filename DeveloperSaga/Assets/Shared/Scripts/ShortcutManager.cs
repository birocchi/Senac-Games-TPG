using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShortcutManager : MonoBehaviour
{		
	
		private List<Shortcut> shortcutList;
		//AbilitiesManager
		AbilitiesManager abilitiesManager;

		// Use this for initialization
		void Start ()
		{
			GameObject gameManager = GameObject.Find ("GameManager");
			abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();	

			//Carregamento de texturas das teclas do teclado
			shortcutList = new List<Shortcut> ();
			
			Shortcut shortcut1 = new Shortcut (1, "Shared/GUI/xbox_buttons/min_button_a", "Shared/GUI/keys/min_key_1", "Shortcut 1", "button");
			shortcutList.Add (shortcut1);
			Shortcut shortcut2 = new Shortcut (2, "Shared/GUI/xbox_buttons/min_button_x", "Shared/GUI/keys/min_key_2", "Shortcut 2", "button");
			shortcutList.Add (shortcut2);
			Shortcut shortcut3 = new Shortcut (3, "Shared/GUI/xbox_buttons/min_button_y", "Shared/GUI/keys/min_key_3", "Shortcut 3", "button");
			shortcutList.Add (shortcut3);
			Shortcut shortcut4 = new Shortcut (4, "Shared/GUI/xbox_buttons/min_button_b", "Shared/GUI/keys/min_key_4", "Shortcut 4", "button");
			shortcutList.Add (shortcut4);
			Shortcut shortcut5 = new Shortcut (5, "Shared/GUI/xbox_buttons/min_rstick_down", "Shared/GUI/keys/min_key_5", "Shortcut 5", "axis");
			shortcutList.Add (shortcut5);
			Shortcut shortcut6 = new Shortcut (6, "Shared/GUI/xbox_buttons/min_rstick_left", "Shared/GUI/keys/min_key_6", "Shortcut 6", "axis");
			shortcutList.Add (shortcut6);
			Shortcut shortcut7 = new Shortcut (7, "Shared/GUI/xbox_buttons/min_rstick_up", "Shared/GUI/keys/min_key_7", "Shortcut 7", "axis");
			shortcutList.Add (shortcut7);
			Shortcut shortcut8 = new Shortcut (8, "Shared/GUI/xbox_buttons/min_rstick_right", "Shared/GUI/keys/min_key_8", "Shortcut 8", "axis");
			shortcutList.Add (shortcut8);
			Shortcut shortcut9 = new Shortcut (9, "Shared/GUI/xbox_buttons/min_button_lt", "Shared/GUI/keys/min_key_9", "Shortcut 9", "axis");
			shortcutList.Add (shortcut9);
			Shortcut shortcut0 = new Shortcut (11, "Shared/GUI/xbox_buttons/min_button_rt", "Shared/GUI/keys/min_key_0", "Shortcut 0", "axis");
			shortcutList.Add (shortcut0);
		}

		public Shortcut GetShortcut(int shortcutNumber) {
			foreach (Shortcut shortcut in shortcutList) {
				if (shortcut != null && shortcut.shortcutNumber == shortcutNumber) {
					return shortcut;
				}
			}
			return null;
		}

		private void HandleAbilitiesShortcuts() {
				//Separa as habilidades em tipos
				List<Ability> abilitiesList = abilitiesManager.abilitiesList;
			
				for (int i = 0; i < shortcutList.Count; i++) {
			if ((Input.GetButton (shortcutList[i].shortcutButton + " Key")) || (shortcutList[i].buttonType == "button" && Input.GetButton ("Abilities") && Input.GetButton (shortcutList[i].shortcutButton + " Button")) || (shortcutList[i].buttonType == "axis" && Input.GetButton ("Abilities") && Input.GetAxis (shortcutList[i].shortcutButton + " Button") == 1)) {
						if (abilitiesList.Count > i) {
							if (!abilitiesList[i].abilityActive && !abilitiesList[i].cooldown) {
								abilitiesList[i].abilityActive = true;
								abilitiesList[i].timeRunning = abilitiesList[i].totalTimeActive;
							}
						}
					}
				}
	
		}
		// Update is called once per frame
		void Update ()
		{
		HandleAbilitiesShortcuts ();
		}
}
