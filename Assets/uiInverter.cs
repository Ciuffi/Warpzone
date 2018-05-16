using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiInverter : MonoBehaviour {


	public void invert() {
		foreach (Text t in GetComponentsInChildren<Text>()) {
			t.color = t.color == Color.black ? Color.white : Color.black;
		}
	}
}
