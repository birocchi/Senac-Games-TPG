public class Ability
{
		public string name;
		public AbilityType type;
		public string description;
		public string iconPath;
		public int totalTimeActive;
		public int totalTimeCooldown;
		public int timeRunning;
		public bool active;
		public int timeCooldown;
		public bool cooldown;

		public enum AbilityType
		{
				WeaponAbility,
				PowerAbility,
				SpecialAbility
	}
		;

		public Ability (string name, AbilityType type, string iconPath, string description)
		{
				this.name = name;
				this.type = type;
				this.iconPath = iconPath;
				this.description = description;
		}

		public Ability (string name, AbilityType type, string iconPath, string description, int totalTimeActive, int totalTimeCooldown)
		{
				this.name = name;
				this.type = type;
				this.iconPath = iconPath;
				this.description = description;
				this.totalTimeActive = totalTimeActive;
				this.totalTimeCooldown = totalTimeCooldown;
		}
}
