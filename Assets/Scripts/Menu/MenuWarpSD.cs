using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWarpSD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<ParticleSystem>().IsAlive()) return;
		if (FindObjectOfType<charMenuHandler>()) FindObjectOfType<charMenuHandler>().telein();
		Destroy(gameObject);

	}
}
