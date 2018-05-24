using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour {


	public SpawnHandler Spawn;

	public GameObject SpawnParticle;
	// Use this for initialization
	void Start () {
		//Flip the Obstacle if its upsidedown
		if (transform.position.y < 0) {
			GetComponent<SpriteRenderer>().flipY = true;
		}

		Instantiate(SpawnParticle, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		//move faster as the game progresses
		transform.Translate(Vector2.left * Time.deltaTime * Spawn.Speed);
		//destroy off camera
		if (transform.position.x < -12) {
			Spawn.LiveObstacles.Remove(gameObject);
			Instantiate(SpawnParticle, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
