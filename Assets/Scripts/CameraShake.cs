using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	private Camera _maincam;
	public float Shake = 0f;
	public float Shakeamount = 0.7f;
	public float Shakedecrease = 1.0f;

	private Vector3 _camstart;
	// Use this for initialization
	void Start () {
		_maincam = Camera.main;
		_camstart = _maincam.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Shake > 0) {
			_maincam.transform.localPosition = Random.insideUnitCircle * Shakeamount;
			_maincam.transform.localPosition -= new Vector3(0, 0, 10);
			Shake -= Time.deltaTime * Shakedecrease;
		}
		else {
			Shake = 0.0f;
			_maincam.transform.localPosition = _camstart;
		}
	}

	public void StartShake(float shakeAmount, float shakeDecrease) {
		Shake = 1;
		Shakeamount = shakeAmount;
		Shakedecrease = shakeDecrease;
	}
}
