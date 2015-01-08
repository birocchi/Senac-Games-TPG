using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shortcut
{
	public int shortcutNumber;
	public string shortcutButtonTexturePath;
	public Texture2D shortcutButtonTexture;
	public string shortcutKeyTexturePath;
	public Texture2D shortcutKeyTexture;
	public string shortcutButton;
	public string buttonType;
	
	public Shortcut (int shortcutNumber, string shortcutButtonTexturePath, string shortcutKeyTexturePath, string shortcutButton, string buttonType)
	{
		this.shortcutNumber = shortcutNumber;
		this.shortcutButtonTexturePath = shortcutButtonTexturePath;
		this.shortcutKeyTexturePath = shortcutKeyTexturePath;
		this.shortcutButton = shortcutButton;
		this.buttonType = buttonType;
	}
		
	public Texture2D GetButtonTexture() {
		if (shortcutButtonTexture == null) {
			shortcutButtonTexture = Resources.Load<Texture2D>(shortcutButtonTexturePath);
		}
		return shortcutButtonTexture;
	}

	public Texture2D GetKeyTexture() {
		if (shortcutKeyTexture == null) {
			shortcutKeyTexture = Resources.Load<Texture2D>(shortcutKeyTexturePath);
		}
		return shortcutKeyTexture;
	}
}
