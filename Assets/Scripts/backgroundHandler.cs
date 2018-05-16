using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundHandler : MonoBehaviour {

	//Inverts the backgrounds color
	public void Invert() {
		GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color == Color.white ? Color.black : Color.white;
	}
}
