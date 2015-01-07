using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability
{
		public string abilityName;
		public AbilityType type;
		public string description;
		public string iconName;
		public int totalTimeActive;
		public int totalTimeCooldown;
		public int timeRunning;
		public bool abilityActive;
		public int timeCooldown;
		public bool cooldown;
		private Texture2D iconNormal;
		private Texture2D iconHighlighted;
		private Texture2D iconClicked;

		public enum AbilityType
		{
				WeaponAbility,
				PowerAbility,
				SpecialAbility
		};

	public Ability (string abilityName, AbilityType type, string iconName, string description)
		{
				this.abilityName = abilityName;
				this.type = type;
				this.iconName = iconName;
				this.description = description;
		}

	public Ability (string abilityName, AbilityType type, string iconName, string description, int totalTimeActive, int totalTimeCooldown)
		{
				this.abilityName = abilityName;
				this.type = type;
				this.iconName = iconName;
				this.description = description;
				this.totalTimeActive = totalTimeActive;
				this.totalTimeCooldown = totalTimeCooldown;
		}

		public Texture2D GetIcon() {
			if (iconNormal == null) {
				iconNormal = Resources.Load<Texture2D>("Shared/GUI/" + iconName);
			}
			return iconNormal;
		}

		public Texture2D GetHighlightedIcon() {
			if (iconHighlighted == null) {
				iconHighlighted = Resources.Load<Texture2D>("Shared/GUI/" + iconName + "_highlighted");
			}
			return iconHighlighted;
		}

		public Texture2D GetClickedIcon() {
			if (iconClicked == null) {
				iconClicked = Resources.Load<Texture2D>("Shared/GUI/" + iconName + "_clicked");
			}
			return iconClicked;
		}
}
