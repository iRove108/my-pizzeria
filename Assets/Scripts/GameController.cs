using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum PizzaLayers {
	Tomato,
	Onion,
	Pepperoni,
	Carrot,
	Pepper,
	Sushi,
	Default
}

public class GameController : MonoBehaviour {

	public List<GameObject> IngriedientLayers = new List<GameObject>();
	public static GameController gameController;
	public static List<string> IngredientNames = new List<string>();
	public static bool GameOver;
	public GameObject MicrowaveDoor;
	public int MoneyMadePerPizza;
	public int TimeToCookMins;
	[HideInInspector]public GameObject pizzaGameobjectToEnable;
	public int StartingMoney;
	public GameObject CurrentPizza;
	public Order CurrentOrder;
	[HideInInspector]public int MoneyLeft;
	public int MinLayersRequired;
	public MinClicksRequired minimumClicksRequired;
	public GameObject UIController;

	private UIController uiController;
	private bool GameOverSet;

	// Use this for initialization
	void Start () {
		GameOverSet = false;
		foreach(GameObject ingredient in IngriedientLayers) {
			IngredientNames.Add (ingredient.name);
		}

		uiController = UIController.GetComponent<UIController>();
		 /* Test Getting the name for ingredients 
		 * for(int i = 0; i < IngredientNames.ToArray ().Length; i++) {
		 * Debug.Log (IngredientNames[i]);
		} */

		KeepSingleton();
		MoneyLeft = 0;
		CurrentOrder = new Order(MinLayersRequired,
		                         minimumClicksRequired.minToppingClicksRequired,
		                         minimumClicksRequired.maxToppingClicksRequired);
	}

	void KeepSingleton() {
		if(GameController.gameController != this && gameController != null) {
			Destroy (gameObject);
		} else {
			GameController.gameController = this;
		}
	}

	// Update is called once per frame
	void Update () {
		if(GameOver == true) {
			ShowGameOverScreen();
		}
	}

	void ShowGameOverScreen() {
		if(GameOverSet == false) {
			uiController.GameOverText.text = "GAME OVER!!!";
			uiController.GameOverText.gameObject.SetActive (true);
			uiController.ShowRestart ();
			GameOverSet = true;
		}
	}

	public void RestartGame() {
		GameOver = false;
		uiController.ClearOrderProgress ();
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void AddMoney(int amt) {
		MoneyLeft += amt;
	}

	public void ClearCurrentPizza() {
		foreach(GameObject ingredientLayer in gameController.IngriedientLayers) {
			ingredientLayer.SetActive (false);
		}
	}

	public static void AddPizzaLayer(PizzaLayers layerToAdd) {
		foreach (GameObject ingredientLayer in gameController.IngriedientLayers) {
			IngredientLayer ingredientLayerComponent = ingredientLayer.GetComponent<IngredientLayer>();
			ingredientLayerComponent.EnableLayerWithTypeMatch (layerToAdd);
		}
	}
	
}

[Serializable]
public class MinClicksRequired {
	public int minToppingClicksRequired;
	public int maxToppingClicksRequired;
}

