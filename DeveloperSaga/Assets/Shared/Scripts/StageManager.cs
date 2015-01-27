using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour {
	public List<Stage> stageList;
	public int stageNumber;
	public static Stage currentStage;

	// Use this for initialization
	void Start () {
		if (stageList == null) {			
			stageList = new List<Stage> ();

			Stage stage1 = new Stage("Stage 01", 1, 0);
			stageList.Add(stage1);
			Stage stage2 = new Stage("Stage 02", 2, 1);
			stageList.Add(stage2);
			Stage stage3 = new Stage("Stage 03", 3, 2);
			stageList.Add(stage3);
		}

		if (stageNumber > 0 && stageNumber <= stageList.Count) {
			currentStage = stageList[stageNumber - 1];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
