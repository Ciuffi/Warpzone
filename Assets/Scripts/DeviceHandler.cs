using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_WEBGL
		#else
		GetComponent<Text>().text = "Tap left to jump\nTap right to warp";
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
