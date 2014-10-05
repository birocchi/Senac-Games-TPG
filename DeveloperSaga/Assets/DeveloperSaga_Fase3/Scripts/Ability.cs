public class Ability {
	public string name;
	public AbilityType type;
	public string iconPath;

	public enum AbilityType {
		WeaponAbility,
		PowerAbility,
		SpecialAbility
	};

	public Ability(string name, AbilityType type, string iconPath) {
		this.name = name;
		this.type = type;
		this.iconPath = iconPath;
	}
}
