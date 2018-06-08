using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour {

	private readonly Vector2 _reset = new Vector2(28.749f, 0.017f);
	private readonly Vector2 _endPosition = new Vector2(-29, 0.017f);
	
	// Update is called once per frame
	void Update () {
		Vector2 trans = Vector2.left * Time.deltaTime * FindObjectOfType<SpawnHandler>().Speed;
		transform.Translate(trans);
		if (transform.position.x < _endPosition.x) transform.position = _reset;
	}
}
