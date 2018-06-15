using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHandler : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Inverts player height (Only works with middle being 0)
		gameObject.transform.position = new Vector3(player.transform.position.x,  player.transform.position.y * -1, player.transform.position.z);
		GetComponent<Animator>().SetBool("Jumping", player.GetComponent<Animator>().GetBool("Jumping"));
		GetComponent<Animator>().SetBool("Falling", player.GetComponent<Animator>().GetBool("Falling"));
	}
}
