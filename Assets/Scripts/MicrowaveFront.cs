using UnityEngine;
using System.Collections;

public class MicrowaveFront : MonoBehaviour {

	public void MicrowaveOn() {
		GetComponent<Renderer>().material.SetColor ("_Color", Color.magenta);
	}

	public void MicrowaveOff() {
		GetComponent<Renderer>().material.SetColor ("_Color", Color.gray);
	}
}
