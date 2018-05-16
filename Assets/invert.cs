using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invert : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Invert() {
		if (GetComponent<SpriteRenderer>().color == Color.white) {
			GetComponent<SpriteRenderer>().color = Color.black;
		}
		else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
}
