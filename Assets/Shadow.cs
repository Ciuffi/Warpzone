using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {
	private GameObject floor;
	private GameObject player;

	// Use this for initialization
	void Start () {
		floor = GameObject.FindGameObjectWithTag("Floor");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float playerHeight = player.transform.position.y;
//		if (player.GetComponent<movement>().Upsidedown) {
//			playerHeight -= player.GetComponent<SpriteRenderer>().bounds.size.y / 2;
//		}
		gameObject.transform.position = new Vector3(player.transform.position.x, playerHeight * -1, player.transform.position.z);
	}
}
