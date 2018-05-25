using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class charMenuHandler : MonoBehaviour {

	private float _timer;
	public GameObject WarpParticlein;
	public GameObject WarpParticleout;

	private float _warptimer = 2;
	// Use this for initialization
	void Start () {
		_timer = _warptimer;

	}

	public void telein() {
		transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-2, 2));
		GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
		Instantiate(WarpParticleout, transform.position, Quaternion.identity);
		_timer = _warptimer;
		Debug.Log("telein");
	}
	
	// Update is called once per frame
	void Update () {
		if (_timer < 0) {
			Camera.main.backgroundColor = Camera.main.backgroundColor == Color.black ? Color.white : Color.black;
			FindObjectOfType<UiInverter>().Invert();
			GameObject p = Instantiate(WarpParticlein, transform.position, Quaternion.identity);
			_timer = 0;
			Debug.Log("timerup");
		}
		else if (_timer > 0){
			_timer -= Time.deltaTime;
		}

	}
}
