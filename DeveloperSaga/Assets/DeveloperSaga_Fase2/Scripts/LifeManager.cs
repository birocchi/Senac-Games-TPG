using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {
	
	private int actualLife;
	private int maxLife = 6;

	public int ActualLife {get { return actualLife; }}

	void Start(){
		actualLife = maxLife;
	}

	void Update(){
		if(actualLife <= 0){
			StartCoroutine(ChangeToGameOver());
		}
	}

	public void LifeUp(int value){
		if(actualLife < maxLife){
			actualLife += value;
		}
		if(actualLife > maxLife){
			actualLife = maxLife;
		}
	}

	public void LifeDown(int value){
		if(actualLife > 0){
			actualLife -= value;
		}
		if(actualLife < 0){
			actualLife = 0;
		}
	}

	public void FillLife(){
		actualLife = maxLife;
	}

	public void EmptyLife(){
		actualLife = 0;
	}

	IEnumerator ChangeToGameOver(){
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("GameOver");
	}
}
