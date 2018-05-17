using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {
	
	private Rigidbody2D _rb2D;
	public bool Upsidedown;
	private GameObject[] _backgrounds;
	private GameObject _shadow;
	private bool _jumpable;
	public float Height = 6;

	private bool _shorthop = false;
	// Use this for initialization
	void Start () {
		_rb2D = GetComponent<Rigidbody2D>();
		_backgrounds = GameObject.FindGameObjectsWithTag("Background");
		_shadow = GameObject.FindGameObjectWithTag("Shadow");
	}

	//Inverts the map and warps the player.
	private void Invert() {
		//invert UI.
		FindObjectOfType<Canvas>().GetComponent<UiInverter>().Invert();
		//Shake Camera.
		Camera.main.GetComponent<CameraShake>().StartShake(0.1f, 6);
		
		//Change gravity for flip.
		if (Upsidedown) {
			Upsidedown = false;
			Physics2D.gravity = new Vector2(0, -10);
		}
		else {
			Upsidedown = true;
			Physics2D.gravity = new Vector2(0, 10);
		}
		//Invert background colour.
		foreach (GameObject background in _backgrounds) {
			background.GetComponent<backgroundHandler>().Invert();
		}
		
		Warp();
	}
	private void Warp() {
		//Reverse the velocity with the gravity change.
		_rb2D.velocity *= Vector2.down;
		//swap positions.
		transform.position = _shadow.transform.position;
		//Flip the sprites.
		GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
		_shadow.GetComponent<SpriteRenderer>().flipY = !_shadow.GetComponent<SpriteRenderer>().flipY;
	}

	private void Jump() {
		if (!_jumpable) return;
		float floatheight = Height;
		//Inviert height if upsidedown
		if (Upsidedown) {
			floatheight *= -1;
		}
		_shorthop = false;
		_rb2D.AddForce(new Vector2(0,floatheight), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		//Jump
#if UNITY_WEBGL	
		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump();
		}
		
		if (Input.GetKeyDown(KeyCode.X)) {
			Invert();
		}
		
	if ((!(transform.position.y > 2) && !(transform.position.y < -2)) || Input.GetKey(KeyCode.Space)) return;
		if (_rb2D.velocity.y > 0 && !Upsidedown || _rb2D.velocity.y < 0 && Upsidedown)
		{
			_rb2D.velocity *= new Vector2(1, 0);
		}


#else
		if (Input.touches.Length > 0) {
			foreach (Touch T in Input.touches) {
				if (T.phase == TouchPhase.Began) {
					if (T.position.x < Screen.width / 2f) {
						Jump();
					}
					else {
						Invert();
					}
				}
				//checks if jump is being held.
				if (T.phase == TouchPhase.Ended && T.position.x < Screen.width / 2f) {
					_shorthop = true;
				}
			}
		}

		//stop jump short if jump isn't held
		if (!_shorthop) return;
		if (!(transform.position.y > 2) && !(transform.position.y < -2))return;
		if ((!(_rb2D.velocity.y > 0) || Upsidedown) && (!(_rb2D.velocity.y < 0) || !Upsidedown)) return;
		_rb2D.velocity *= new Vector2(1, 0);
#endif
	}

	//resets player position
	private void ResetPlayer() {
		transform.position = new Vector3(-7, 1, -01);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		//lets you jump if you're touching something
		_jumpable = true;
		//Collision code which allows you to jump on top of obstacles
		if (!other.gameObject.CompareTag("Obstacle")) return;
		if ((other.transform.position.y + other.gameObject.GetComponent<SpriteRenderer>().bounds.size.y < transform.position.y && !Upsidedown) 
		    || (other.transform.position.y > transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2 && Upsidedown)) 
			return;
		//If it hasn't returned, you died.
		ResetGame();
	}

	private void ResetGame() {
		GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnHandler>().GameOver();
		if (Upsidedown) {
			Invert();
		}
		GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Reset();
		ResetPlayer();
		Camera.main.GetComponent<CameraShake>().StartShake(0.5f, 3);
	}

	private void OnCollisionExit2D(Collision2D other) {
		//stops you from jumping while in the air.
		_jumpable = false;
	}
}
