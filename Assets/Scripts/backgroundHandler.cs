using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundHandler : MonoBehaviour {
	private float _t;
	private Color _currentColor;
	private Color _start;
	private Color _end;
	private bool _lerping;
	//Inverts the backgrounds color
	public void Start() {
		_currentColor = GetComponent<SpriteRenderer>().color;
	}
	//Inverts the backgrounds color
	public void Invert() {
		if (_currentColor == Color.black) {
			_start = Color.black;
			_end = Color.white;
			_currentColor = Color.white;
			_t = 0;
			_lerping = true;
		}
		else if (_currentColor == Color.white){
			_start = Color.white;
			_end = Color.black;
			_currentColor = Color.black;
			_t = 0;
			_lerping = true;
		}
	}

	public void Update() {
		_t += Time.deltaTime * 5;
		if (_lerping) {
			colorlerp(_start, _end);
		}
	}

	public void colorlerp(Color start, Color end) {
		GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, _t);
		if (_t > 1) {
			_lerping = false;
		}
	}
}
