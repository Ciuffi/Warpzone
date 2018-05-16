﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugHandler : MonoBehaviour {
	private SpawnHandler spawn;
	// Use this for initialization
	void Start () {
		if (!Debug.isDebugBuild) {
			gameObject.SetActive(false);
		}

		spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = "Dev Info: \nSpeed: " + spawn.Speed + "\nFrequency: " + spawn.SpawnFrequency;
	}
}