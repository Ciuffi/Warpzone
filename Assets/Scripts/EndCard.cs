using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour {
	private Text t;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
		Off();
	}

	public void On(float score, int highscore, bool higher) {
		t.enabled = true;
		if (higher) {
			t.text = "nNew highscore\nScore " + (int) score;
		}
		else {
			t.text = "Score: " + (int)score + "\nHighscore: " + highscore;
		}
	}

	public void Off() {
		t.enabled = false;
	}
	
}
