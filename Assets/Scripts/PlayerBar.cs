using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BarType{
	healthBar,
	manaBar 
}
public class PlayerBar : MonoBehaviour {

	public BarType type;
	private Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();		
		switch (type)
		{
			
			case BarType.healthBar:
				slider.maxValue = PlayerController.MAX_HEALTH;
			break;

			case BarType.manaBar:
				slider.maxValue = PlayerController.MAX_MANA;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		switch (type)
		{
			case BarType.healthBar:
				slider.value = PlayerController.sharedInstance.GetHealth();
			break;

			case BarType.manaBar:
				slider.value = PlayerController.sharedInstance.GetMana();
			break;
		}
	}
}
