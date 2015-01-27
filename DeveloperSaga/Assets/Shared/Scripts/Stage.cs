using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage
{
	public string stageName;
	public int stageNumber;
	public int stageTheme;

	public Stage (string stageName, int stageNumber, int stageTheme)
	{
		this.stageName = stageName;
		this.stageNumber = stageNumber;
		this.stageTheme = stageTheme;
	}
}
