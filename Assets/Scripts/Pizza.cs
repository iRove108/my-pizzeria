using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pizza : MonoBehaviour {

	[HideInInspector]public int ToppingsAdded;
	[HideInInspector]public List<PizzaLayers> layersAdded = new List<PizzaLayers>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
