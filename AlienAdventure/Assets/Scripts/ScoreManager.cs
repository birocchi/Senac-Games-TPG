using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score = 0;
	public GUIText scoreText;

	public void AddScore(int points){
		score += points;
		scoreText.text = "Score: " + score;
	}

	public void SubtractScore(int points){
		score -= points;
		scoreText.text = "Score: " + score;
	}
}
