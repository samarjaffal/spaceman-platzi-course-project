using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	public Canvas menuCanvas;
	public Canvas gameCanvas;

	public Canvas exitCanvas;
	public static MenuManager sharedInstance;

	private void Awake() {
		if(sharedInstance == null){
			sharedInstance = this;
		}
	}

	public void ShowMainMenu(){
		menuCanvas.enabled = true;
		HideGameMenu();
	}

	public void HideMainMenu(){
		menuCanvas.enabled = false;
		ShowGameMenu();
	}


	public void ShowGameMenu(){
		//gameCanvas.enabled = true;
		gameCanvas.gameObject.SetActive(true);
	}

	public void HideGameMenu(){
		//gameCanvas.enabled = false;
		gameCanvas.gameObject.SetActive(false);
	}

	public void showExitMenu(){
		HideGameMenu();
		exitCanvas.gameObject.SetActive(true);
	}

	public void HideExitMenu(){
		//gameCanvas.enabled = false;
		exitCanvas.gameObject.SetActive(false);
	}

	public void ExitMenu(){

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
