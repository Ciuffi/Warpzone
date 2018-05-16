using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	public float ticker;

	public void reset() {
		ticker = 0;
	}
	
	// Update is called once per frame
	void Update () {
		ticker += Time.deltaTime;
		GetComponent<Text>().text = "Timer: " + (int) ticker;
	}
}
