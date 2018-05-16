﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {

	public GameObject ObstaclePrefab;
	public List<GameObject> LiveObstacles;
	public float Speed = 3;
	private float _speedtimer = 15;

	private double _spawntimer = 0.1;
	public float SpawnFrequency = 5;
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

			GameObject obstacle = Instantiate(ObstaclePrefab, new Vector3(10, rando, 0), Quaternion.identity);
			obstacle.GetComponent<ObstacleHandler>().Spawn = this;
			LiveObstacles.Add(obstacle);
		}
	}

	public void GameOver() {
		//Destroy all the obstacles
		foreach (GameObject o in LiveObstacles.ToList()) {
			LiveObstacles.Remove(o);
			Destroy(o);
		}
		SpawnFrequency = 5;
		_spawntimer = 0.1;
		Speed = 3;
	}
	// Update is called once per frame
	void Update () {
		//Spawn them more often as the game progresses
		_speedtimer -= Time.deltaTime;
		if (_speedtimer <= 0) {
			_speedtimer = 15;
			Speed++;
			SpawnFrequency--;
		}
		_spawntimer -= Time.deltaTime;
		if (!(_spawntimer <= 0)) return;
		Spawn();
		_spawntimer = SpawnFrequency;

	}
}
