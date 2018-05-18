using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCard : MonoBehaviour {
	private Text t;
	private Text t2;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
		t2 = GameObject.FindGameObjectWithTag("continue").GetComponent<Text>();
		Off();
	}

	public void On(float score, int highscore, bool higher) {
		t.enabled = true;
		t2.enabled = true;
		if (higher) {
			t.text = "New highscore\nScore " + (int) score;
		}
		else {
			t.text = "Score: " + (int)score + "\nHighscore: " + highscore;
		}

		#if UNITY_WEBGL
		t2.text = "Press any key to continue";
		#else
		t2.text = "Tap anywhere to continue";
		#endif
	}

	public void Off() {
		t.enabled = false;
		t2.enabled = false;
	}
	
}
