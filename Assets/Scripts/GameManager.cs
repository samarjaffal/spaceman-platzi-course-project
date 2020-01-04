using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a enum variable to indicate the actual status of the game, and it is outside the class because is more easy to access fro other components

public enum GameState{
	menu,
	inGame,
	gameOver 
}
public class GameManager : MonoBehaviour {
	public GameState currentGameState = GameState.menu;
	// Use this for initialization
	public static GameManager sharedInstance; //this is a singleton, the unique game manager that it will be used and shared for other scripts

	public int collectedObject = 0;
	private PlayerController controller;

	AudioSource gameOverAudio;
	void Awake() {
		if(sharedInstance == null){
			sharedInstance = this;
		}
	}
	void Start () {

		controller = GameObject.Find("Player").GetComponent<PlayerController>();
		gameOverAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame){
			StarGame();
		}else if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.M)){
			BackToMenu();
		}

	}

	public void StarGame(){
		SetGameState(GameState.inGame);
	}

	public void GameOver(){
		SetGameState(GameState.gameOver);
	}

	public void BackToMenu(){
		SetGameState(GameState.menu);
	}

	private void SetGameState(GameState newGameState){ //change the status of the game
		if(newGameState == GameState.menu){
			//TODO: set the logic of the menu
			MenuManager.sharedInstance.ShowMainMenu();
		}else if(newGameState == GameState.inGame){
			//TODO : prepare the scene to play
			LavelManager.sharedInstance.RemoveAllLevelBlokcs();
			LavelManager.sharedInstance.GenerateInitialBlocks();
			controller.StartGame(); // call function startGame in player controller
			collectedObject = 0;
			if(MenuManager.sharedInstance.exitCanvas.gameObject.activeSelf == true){
				MenuManager.sharedInstance.HideExitMenu();
			}
			MenuManager.sharedInstance.HideMainMenu();
		}else if(newGameState == GameState.gameOver){
			//TODO: prepare the game to finish
			gameOverAudio.Play();
			MenuManager.sharedInstance.showExitMenu();
		}

		this.currentGameState = newGameState;
	}

	public void CollectObject(Collectable collectable){
		collectedObject += collectable.value; //class Collectable and his variable value
		//increments the collectable object (coins,etc)
	}
}
