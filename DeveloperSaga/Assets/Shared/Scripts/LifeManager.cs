using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public enum LifeType {Player, Boss};

	private int playerLife;
	public int maxLife = 6;

	private int bossLife;
	public int bossMaxLife = 6;

	public int PlayerLife {get { return playerLife; } set {playerLife = value;}}
	public int BossLife {get { return bossLife; }}

	void Awake(){
		playerLife = maxLife;
		bossLife = bossMaxLife;
	}

	void Update(){
		if(playerLife <= 0){
			StartCoroutine(ChangeToGameOver());
		}
	}

	public void LifeUp(int value, LifeType type){
		if(type.Equals(LifeType.Player)){
			if(playerLife < maxLife){
				playerLife += value;
			}
			if(playerLife > maxLife){
				playerLife = maxLife;
			}
		}
		if(type.Equals(LifeType.Boss)){
			if(bossLife < bossMaxLife){
				bossLife += value;
			}
			if(bossLife > bossMaxLife){
				bossLife = bossMaxLife;
			}
		}
	}

	public void LifeDown(int value, LifeType type){
		if(type.Equals(LifeType.Player)){
			if(playerLife > 0){
				playerLife -= value;
			}
			if(playerLife < 0){
				playerLife = 0;
			}
		}
		if(type.Equals(LifeType.Boss)){
			if(bossLife > 0){
				bossLife -= value;
			}
			if(bossLife < 0){
				bossLife = 0;
			}
		}
	}

	public void FillLife(LifeType type){
		if(type.Equals(LifeType.Player)){
			playerLife = maxLife;
		}
		if(type.Equals(LifeType.Boss)){
			bossLife = bossMaxLife;
		}
	}

	public void EmptyLife(LifeType type){
		if(type.Equals(LifeType.Player)){
			playerLife = 0;
		}
		if(type.Equals(LifeType.Boss)){
			bossLife = 0;
		}
	}

	IEnumerator ChangeToGameOver(){
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("GameOver");
	}
}
