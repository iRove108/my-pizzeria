using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {

	public Text TimeText;
	public Text OrderText;
	public Text Money;
	public Text GameOverText;
	public Button RestartButton;
	public Button DoneButton;
	public int StartTimerAmt;
	public Text IngredientsAdded;

	private float TimeRemaining;
	private bool TimerOn;
	private bool MicrowaveOn;
	private float MicrowaveTimeRemaining;
	private GameController gameController;
	private bool InCorrectTimerOn;

	// Use this for initialization
	void Start () {

		gameController = GameController.gameController;
		TimerOn = true;
		InCorrectTimerOn = true;
		MicrowaveOn = false;
		RestartButton.gameObject.SetActive (false);
		TimeRemaining = StartTimerAmt;

		OrderText.text = GameController.gameController.CurrentOrder.TransferOrderToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(TimerOn) {
		SubtractTimer();
		}

		if(MicrowaveOn) {
			MicrowaveTimeRemaining -= Time.deltaTime;

			if(MicrowaveTimeRemaining <= 0) {
				GameController.gameController.MicrowaveDoor.GetComponent<MicrowaveFront>().MicrowaveOff ();
				MicrowaveOn = false;
				Money.text = "Money: $" + gameController.MoneyLeft;
			}
		}

	}

	void SubtractTimer() {
		TimeRemaining -= Time.deltaTime;

		TimeText.text = "Time Remaining: " + TimeRemaining;

		if(TimeRemaining <= 0) {
			TimerOn = false;
			TimeRemaining = 0.0f;
			TimeText.text = "Time Remaining: " + TimeRemaining;
			GameController.GameOver = true;
		}
	}

	public IEnumerator TextTimer() {
		//Debug.Log ("Incorrect Timer Initiated");
		while(InCorrectTimerOn) {
			GameOverText.text = "INCORRECT!";
			GameOverText.gameObject.SetActive (true);
			yield return new WaitForSeconds(1);
			InCorrectTimerOn = false;
			GameOverText.gameObject.SetActive (false);
		}
	}

	public void DonePressed() {
		gameController.MicrowaveDoor.GetComponent<MicrowaveFront>().MicrowaveOn ();
		MicrowaveOn = true;
		MicrowaveTimeRemaining = gameController.TimeToCookMins;


		if(gameController.CurrentOrder.CheckIfClicksMatching (gameController.CurrentPizza.GetComponent<Pizza>().layersAdded, Ingredient.amtPizzaPressed)) {
			gameController.AddMoney (gameController.MoneyMadePerPizza);
		} else if (gameController.CurrentOrder.CheckIfClicksMatching (gameController.CurrentPizza.GetComponent<Pizza>().layersAdded, Ingredient.amtPizzaPressed) == false){
			InCorrectTimerOn = true;
			StartCoroutine(TextTimer ());
		}

		ClearOrderProgress();

		gameController.CurrentOrder.GenerateRandomOrder (gameController.minimumClicksRequired.minToppingClicksRequired);
		OrderText.text = GameController.gameController.CurrentOrder.TransferOrderToString();
	}

	public void ClearOrderProgress() {
		gameController.ClearCurrentPizza ();
		gameController.CurrentOrder.amtRequired = new Dictionary<PizzaLayers, int>();
		gameController.CurrentOrder.layersRequired = new List<PizzaLayers>();
		gameController.CurrentPizza.GetComponent<Pizza>().ToppingsAdded = 0;
		gameController.CurrentPizza.GetComponent<Pizza>().layersAdded = new List<PizzaLayers>();
		Ingredient.amtPizzaPressed = new Dictionary<PizzaLayers, int>();
		foreach (GameObject ingredientLayer in gameController.IngriedientLayers) {
			IngredientLayer ingredientLayerComponent = ingredientLayer.GetComponent<IngredientLayer>();
			IngredientsAdded.text = 
				"Tomato Layers: 0, Onion Layers: 0, Pepperoni Layers: 0," +
				" Carrot Layers: 0, Pepper Layers: 0, Sushi Layers: 0.";
		}
	}

	public void ShowRestart() {
		RestartButton.gameObject.SetActive (true);
		DoneButton.gameObject.SetActive (false);
	}

	public void DontShowRestart() {
		RestartButton.enabled = false;
		DoneButton.enabled = true;
	}
}
