using System.Collections;
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
		GetComponent<Text>().text = "Device Name: " + SystemInfo.deviceName +
		                            "\nDevice OS: " + SystemInfo.operatingSystem +
		                            "\nSpeed: " + spawn.Speed +
		                            "\nFrequency: " + spawn.SpawnFrequency +
		                            "\nVersion Number: " + Application.version;
	}
}
