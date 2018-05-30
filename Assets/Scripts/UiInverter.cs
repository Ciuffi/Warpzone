using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInverter : MonoBehaviour {


	//Invert all the canvas UI elements colours
	public void Invert() {
		foreach (Text t in GetComponentsInChildren<Text>()) {
			t.color = t.color == Color.black ? Color.white : Color.black;
		}
	}
}
