using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject obstacle;
	public List<GameObject> obstacles;

	public double timer = 0.1;
	public float number = 6;

	void start() {
		obstacles = new List<GameObject>();
	}
	void spawn() {
		for (int i = 0; i < number; i++) {
			float rando = Random.Range(-6, 6);
			if (rando == 0) {
				rando++;
			}
			obstacles.Add(Instantiate(obstacle, new Vector3(10, rando, 0), Quaternion.identity));
		}
	}

	public void gameOver() {
		foreach (GameObject o in obstacles) {
			Destroy(o);
		}
	}
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime * (GameObject.FindGameObjectWithTag("Timer").GetComponent<timer>().ticker) / 10;
		if (timer <= 0) {
			spawn();
			timer = 2;
		}

	}
}
