using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreMenuItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
