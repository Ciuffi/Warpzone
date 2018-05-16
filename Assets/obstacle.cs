using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	private GameObject floor;

	public float speed;
	// Use this for initialization
	void Start () {
		floor = GameObject.FindGameObjectWithTag("Floor");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.left * Time.deltaTime * speed * (GameObject.FindGameObjectWithTag("Timer").GetComponent<timer>().ticker / 10));
		if (transform.position.x < floor.transform.position.x - floor.GetComponent<SpriteRenderer>().bounds.size.x - 1) {
			Destroy(gameObject);
		}
	}
}
