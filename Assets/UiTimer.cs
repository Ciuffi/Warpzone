using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTimer : MonoBehaviour {

	public float Ticker;

	public void Reset() {
		Ticker = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Ticker += Time.deltaTime;
		GetComponent<Text>().text = "Timer: " + (int) Ticker;
	}
}
