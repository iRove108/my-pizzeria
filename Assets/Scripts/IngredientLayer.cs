using UnityEngine;
using System.Collections;

public class IngredientLayer : MonoBehaviour {

	public PizzaLayers pizzaLayerType = PizzaLayers.Default;

	void Start() {
		if(pizzaLayerType == PizzaLayers.Default) {
			Debug.Log ("Please change the pizza layer type of the " + gameObject.name + " GameObject to something" +
				" other than default.");
		}
	}

	public void EnableLayerWithTypeMatch(PizzaLayers matchingLayer) {
		
		switch(matchingLayer) {
			case PizzaLayers.Tomato:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Tomato)) {
					gameObject.SetActive (true);
				}
				break;
			case PizzaLayers.Pepperoni:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Pepperoni)) {
					gameObject.SetActive (true);
				}
				break;
			case PizzaLayers.Onion:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Onion)) {
					gameObject.SetActive (true);
				}
				break;
			case PizzaLayers.Carrot:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Carrot)) {
					gameObject.SetActive (true);
				}
				break;
			case PizzaLayers.Pepper:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Pepper)) {
					gameObject.SetActive (true);
				}
				break;
			case PizzaLayers.Sushi:
				if(PizzaTypeMatchesPizzaLayer (PizzaLayers.Sushi)) {
					gameObject.SetActive (true);
				}
				break;
			default:
				break;
		}

	}

	bool PizzaTypeMatchesPizzaLayer(PizzaLayers layer) {
		if(pizzaLayerType == layer) {
			return true;
		}else {
			return false;
		}
	}

}
