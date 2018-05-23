using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {
	private int _levelindex;
	private int _upsidedownlevelindex;
	public GameObject ObstaclePrefab;
	public List<GameObject> LiveObstacles;
	public float Speed = 6;
	public float TimePerSpeedup = 2;
	private float _speedtimer;
	private double _spawntimer = 0.1;
	public float SpawnFrequency = 0;
	public bool Pause;
	private LevelObject _up;
	private LevelObject _down;
	private Levels _levelbook;
	public bool Tutorial;

	
	void Start() {
		LiveObstacles = new List<GameObject>();
		_speedtimer = TimePerSpeedup;
		_levelbook = new Levels();
		Tutorial = true;
		_up = _levelbook.Beginner;
		_down = _levelbook.Beginnerdown;
	}
	

	public void GameOver() {
		//Destroy all the obstacles
		foreach (GameObject o in LiveObstacles.ToList()) {
			LiveObstacles.Remove(o);
			Destroy(o);
		}

		if (Tutorial) {
			_up = _levelbook.Beginner;
			_down = _levelbook.Beginnerdown;
		}
		else {
			_up = _levelbook.BuildRandomLevel(30, 1);
			_down = _levelbook.BuildRandomLevel(30, 1);
		}
		_spawntimer = 0.1;
		Speed = 3;
		Pause = true;
		_levelindex = 0;
		_upsidedownlevelindex = 0;
	}
	// Update is called once per frame
	void Update () {
		if (Pause) return;
		_speedtimer -= Time.deltaTime;
		if (_speedtimer <= 0 && Speed < 10) {
			Speed += 0.2f;
			_speedtimer = TimePerSpeedup;
		}
		_spawntimer -= Time.deltaTime;
		if (!(_spawntimer <= 0)) return;
		Spawn(_up);
		SpawnUpsideDown(_down);
		_spawntimer = SpawnFrequency;

	}
	
	private void Spawn(LevelObject l) {
		if (_levelindex >= l.GetLevel().Count) {
			Tutorial = false;
			_levelindex = 0;
			_up = _levelbook.BuildRandomLevel(30, 1);
		}
		foreach (float f in l.GetLevel()[_levelindex]) {
			GameObject obstacle = Instantiate(ObstaclePrefab, new Vector3(12, f, 0), Quaternion.identity);
			obstacle.GetComponent<ObstacleHandler>().Spawn = this;
			LiveObstacles.Add(obstacle);
		}
		_levelindex++;
	}
	private void SpawnUpsideDown(LevelObject l) {
		if (_upsidedownlevelindex >= l.GetLevel().Count) {
			_upsidedownlevelindex = 0;
			_down = _levelbook.BuildRandomLevel(30, 1);
		}
		foreach (float f in l.GetLevel()[_upsidedownlevelindex]) {
			GameObject obstacle = Instantiate(ObstaclePrefab, new Vector3(12, -f, 0), Quaternion.identity);
			obstacle.GetComponent<ObstacleHandler>().Spawn = this;
			LiveObstacles.Add(obstacle);
		}
		_upsidedownlevelindex++;
	}
}
