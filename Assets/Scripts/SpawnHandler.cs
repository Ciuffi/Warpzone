using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {

	public GameObject ObstaclePrefab;
	public List<GameObject> LiveObstacles;

	private double _timer = 0.1;
	public float SpawnedPerTick = 6;

	void Start() {
		LiveObstacles = new List<GameObject>();
	}
	private void Spawn() {
		//Spawn the Obstacles, making sure not to spawn any in the floor.
		for (int i = 0; i < SpawnedPerTick; i++) {
			float rando = Random.Range(-6, 6);
			if (rando == 0) {
				rando++;
			}
			LiveObstacles.Add(Instantiate(ObstaclePrefab, new Vector3(10, rando, 0), Quaternion.identity));
		}
	}

	public void gameOver() {
		//Destroy all the obstacles
		foreach (GameObject o in LiveObstacles) {
			Destroy(o);
		}
	}
	// Update is called once per frame
	void Update () {
		//Spawn them more often as the game progresses
		_timer -= Time.deltaTime * (GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Ticker) / 10;
		if (!(_timer <= 0)) return;
		Spawn();
		_timer = 2;

	}
}
