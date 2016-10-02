using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingredient : MonoBehaviour {

	[HideInInspector]public PizzaLayers pizzaLayerType;
	public static Dictionary<PizzaLayers, int> amtPizzaPressed = new Dictionary<PizzaLayers, int>();

	private int timesIngredientAdded;


	void OnMouseDown() {

		//Add the key-value pairs to tell how many times the ingredient has been pressed
		timesIngredientAdded++;


		pizzaLayerType = MatchPizzaLayer ();
		if(amtPizzaPressed.ContainsKey (pizzaLayerType)) {
			amtPizzaPressed.Remove (pizzaLayerType);
		}

		amtPizzaPressed.Add (pizzaLayerType, timesIngredientAdded);

		Pizza currentPizza = GameController.gameController.CurrentPizza.GetComponent<Pizza>();

		//Add to the list to tell what ingredients have been added
		if(!currentPizza.layersAdded.Contains (pizzaLayerType)) {
			currentPizza.layersAdded.Add (pizzaLayerType);
		} 


		GameController.AddPizzaLayer (pizzaLayerType);

		UpdateIngredientNumberText();
	}

	PizzaLayers MatchPizzaLayer() {

		PizzaLayers matchingPizzaLayer = PizzaLayers.Default;

		switch(gameObject.name) {
			case "Food Tomato Prefab":
				matchingPizzaLayer = PizzaLayers.Tomato;
				break;
			case "Pepperoni_Ingredients":
				matchingPizzaLayer = PizzaLayers.Pepperoni;
				break;
			case "Food Onion Prefab":
				matchingPizzaLayer = PizzaLayers.Onion;
				break;
			case "Food Carrot Prefab":
				matchingPizzaLayer = PizzaLayers.Carrot;
				break;
			case "Food Pepper Ingredient":
				matchingPizzaLayer = PizzaLayers.Pepper;
				break;
			case "Food Sushi Prefab":
				matchingPizzaLayer = PizzaLayers.Sushi;
				break;
			default:
				Debug.Log ("You Have clicked on a GameObject that has the AddOnClick script on it but does not match with one of the found names for the pizzaLayers." +
					"Make sure to update the switch statement in the MatchPizzaLayer() function in AddOnClick and the PizzaLayers enum in GameController if you have added a new ingredient you would like to" +
					"implement");
				break;
		}

		return matchingPizzaLayer;
	}

	public void ResetTimesPressed() {
		timesIngredientAdded = 0;
	}


	public void UpdateIngredientNumberText() {
		//Declare variables to store number pressed for each item
		int tomatoNumPressed = 0;
		int onionNumPressed = 0;
		int pepperoniNumPressed = 0;
		int carrotNumPressed = 0;
		int pepperNumPressed = 0;
		int sushiNumPressed = 0;

		//Put correct values in variables defined right abouve
		amtPizzaPressed.TryGetValue(PizzaLayers.Tomato, out tomatoNumPressed);
		amtPizzaPressed.TryGetValue(PizzaLayers.Onion, out onionNumPressed);
		amtPizzaPressed.TryGetValue(PizzaLayers.Pepperoni, out pepperoniNumPressed);
		amtPizzaPressed.TryGetValue(PizzaLayers.Carrot, out carrotNumPressed);
		amtPizzaPressed.TryGetValue(PizzaLayers.Pepper, out pepperNumPressed);
		amtPizzaPressed.TryGetValue(PizzaLayers.Sushi, out sushiNumPressed);

		GameController.gameController.UIController.GetComponent<UIController>().IngredientsAdded.text =
			"Tomato Layers: " + tomatoNumPressed + ", Onion Layers: " + onionNumPressed + ", Pepperoni Layers: " +
			pepperoniNumPressed + ", Carrot Layers: " + carrotNumPressed + ", Pepper Layers: " + pepperNumPressed +
			", Sushi Layers: " + sushiNumPressed + ".";
	}
}
