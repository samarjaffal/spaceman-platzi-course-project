using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour {

	public Text coinsText, scoreText, maxScoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//set the value with the collectable object variable created in the class Game Manager
		int coins = GameManager.sharedInstance.collectedObject; 
		float score = 0.0f;
		if(GameManager.sharedInstance.currentGameState == GameState.inGame){
		
			score = PlayerController.sharedInstance.GetTravelledDistance();
			float maxScore = PlayerPrefs.GetFloat("maxscore", 0);
			
			maxScoreText.text ="Max Score: " + maxScore.ToString("f1");

		}else if(GameManager.sharedInstance.currentGameState == GameState.gameOver){

			score = PlayerPrefs.GetFloat("score", 0);
		}

		coinsText.text = coins.ToString();
		scoreText.text = "Score: " + score.ToString("f1");
	}
}
