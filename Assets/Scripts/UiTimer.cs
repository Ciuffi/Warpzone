using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTimer : MonoBehaviour {

	public float Ticker;
	public bool Pause;

	public void Reset() {
		Ticker = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Pause) return;
		Ticker += Time.deltaTime;
		GetComponent<Text>().text = "Timer: " + (int) Ticker;
		GameObject.FindGameObjectWithTag("highscore").GetComponent<Text>().text = "Highscore: " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().Highscore.ToString();
	}
}
