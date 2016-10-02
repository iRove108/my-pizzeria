using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order {

	public List<PizzaLayers> layersRequired = new List<PizzaLayers>();
	public Dictionary<PizzaLayers, int> amtRequired = new Dictionary<PizzaLayers, int>();

	private int maxPizzaLayersGenerated;
	private int minToppingClicksRequired;
	private int maxToppingClicksRequired;
	private int numLayersRequired;

	public Order(int minLayersRequired, int minToppingTypeRequired, int maxToppingTypesRequired) {
		minToppingClicksRequired = minToppingTypeRequired;
		maxToppingClicksRequired = maxToppingTypesRequired;
		maxPizzaLayersGenerated = GameController.gameController.IngriedientLayers.ToArray().Length;
		GenerateRandomOrder(minLayersRequired);
	}

	public void GenerateRandomOrder(int minimumLayersGeneratedPerOrder) {

		numLayersRequired = Random.Range (minimumLayersGeneratedPerOrder, maxPizzaLayersGenerated);

		for(int i = 0; i < numLayersRequired; i++) {
			int layerIsUsed = Random.Range (1, 2);
			bool layerUsed = false;

			if (layerIsUsed == 1) {
				layerUsed = true;
			}


			if(layerUsed) {
				layersRequired.Add (GameController.gameController.IngriedientLayers[i].GetComponent<IngredientLayer>().pizzaLayerType);
				amtRequired.Add (GameController.gameController.IngriedientLayers[i].GetComponent<IngredientLayer>().pizzaLayerType,
				                 Random.Range (minToppingClicksRequired, maxToppingClicksRequired));
				//Debug.Log ("layer used");
			}
		}
	}

	public bool CheckIfClicksMatching(List<PizzaLayers> layersClicked, Dictionary<PizzaLayers, int> clicksPerTopping) {
		int pairNumbers = 0;

		foreach(KeyValuePair<PizzaLayers, int> pair in clicksPerTopping) {

			if(amtRequired.ContainsKey (pair.Key) && amtRequired.ContainsValue (pair.Value)) {
				pairNumbers++;
			}
		}

		if(pairNumbers == numLayersRequired) {
			return true;
		} else {
			return false;
		}
	}

	public string TransferOrderToString() {
		string orderString = "";
		orderString += "Order (In Layers): ";
		foreach(KeyValuePair<PizzaLayers, int> order in amtRequired) {
			orderString += order.Value + " " + order.Key;

			orderString += ", ";

			//Debug.Log ("Value: " + order.Value + " Key: " + order.Key);
		}
		//Remove last comma
		orderString = orderString.Remove(orderString.Length - 2);

		return orderString;
	}
	
}
