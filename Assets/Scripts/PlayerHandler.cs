using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {
	
	//Gameobjects
	private Rigidbody2D _rb2D;
	private GameObject[] _backgrounds;
	private GameObject _shadow;
	public GameObject WarpParticles;
	//jumping
	private bool _jumpable;
	public float Height = 6;
	private bool _shorthop = false;
	public float GravDelay = 0.05f;	
	private float _gravtimer;
	private bool _falldelay;
	private float _startgrav;
	private float _maxjumpheight;
	private float _maxShortHopHeight;
	private float _maxjumpbuffer = 0.2f;
	private Vector2 _jumpvelocity;
	private Vector2 _startPosition;
	//Warping
	private float _warpcooldown;
	//Score/game settings
	public int Highscore;
	private bool _reset;
	private bool _upsidedown;
	private bool _newscore;
	// Use this for initialization
	void Start () {
		_rb2D = GetComponent<Rigidbody2D>();
		_backgrounds = GameObject.FindGameObjectsWithTag("Background");
		_shadow = GameObject.FindGameObjectWithTag("Shadow");
		Highscore = PlayerPrefs.GetInt("Highscore", Highscore);
		_startgrav = _rb2D.gravityScale;
		_upsidedown = false;
		Physics2D.gravity = new Vector2(0, -10);
		_startPosition = transform.position;
	}

	//Inverts the map and warps the player.
	private void Invert() {
		if (_warpcooldown > 0f) return;
		_warpcooldown = 0.1f;
		//invert UI.
		FindObjectOfType<Canvas>().GetComponent<UiInverter>().Invert();
		//Shake Camera.
		Camera.main.GetComponent<CameraShake>().StartShake(0.1f, 6);
		
		//Change gravity for flip.
		if (_upsidedown) {
			_upsidedown = false;
			Physics2D.gravity = new Vector2(0, -10);
		}
		else {
			_upsidedown = true;
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
		//Create warp particles in both shadow and current position
		Instantiate(WarpParticles, transform.position, Quaternion.identity);
		Instantiate(WarpParticles, _shadow.transform.position, Quaternion.identity);
		//swap positions.
		transform.position = _shadow.transform.position;
		//Flip the sprites.
		GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
		_shadow.GetComponent<SpriteRenderer>().flipY = !_shadow.GetComponent<SpriteRenderer>().flipY;
	}

	private void Jump() {
		//if you can't jump, return
		if (!_jumpable) {
			return;
		}
		float floatheight = Height;
		//sets the minimum jump height for a short jump
		_maxShortHopHeight = transform.position.y + 1;
		//Invert height if upsidedown
		if (_upsidedown) {
			_maxShortHopHeight = transform.position.y - 1;
			floatheight *= -1;
		}
		GetComponent<Animator>().SetBool("Jumping", true);
		_shorthop = false;
		//allow a small delay before falling
		_falldelay = true;
		//calculate the max height of a max power jump
		_maxjumpheight = JumpMax(Height);
		//jump
		_rb2D.AddForce(new Vector2(0,floatheight), ForceMode2D.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
		//0.1 cooldown for warp to avoid bugs
		if (_warpcooldown > 0) {
			_warpcooldown -= Time.deltaTime;
		}
		else {
			_warpcooldown = 0;
		}
		UpdateTrail();
		//if the character is touching the ground, you can jump
		_jumpable = CheckIfGrounded();
		if (_jumpable) {
			GetComponent<Animator>().SetBool("Falling", false);
		}

		//updates highscore
		if (GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Ticker > Highscore) {
			_newscore = true;
			Highscore = (int) GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Ticker;
		}

		//adds a small delay if you've reached your max jump height
		if ( !_upsidedown && transform.position.y > _maxjumpheight || _upsidedown && transform.position.y < _maxjumpheight) {
			FallDelay();
		}
		//Reset gravity and velocity after falldelay is over
		if (_gravtimer < 0) {
			_gravtimer = 0;
			Fall();
		}
		else if (_gravtimer > 0){
			_gravtimer -= Time.deltaTime;
		}
		//Jump
		#if UNITY_WEBGL
		if (_reset) {
			if (Input.anyKey) {
				_reset = false;
				ResetGame();
				return;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump();
		}
		
		if (Input.GetKeyDown(KeyCode.X)) {
			Invert();
		}
		
	if ((!(transform.position.y > _maxShortHopHeight) && !(transform.position.y < -_maxShortHopHeight)) || Input.GetKey(KeyCode.Space)) return;
		if (_rb2D.velocity.y > 0 && !_upsidedown || _rb2D.velocity.y < 0 && _upsidedown)
		{
			FallDelay();
		}


		#else
		//Tap to reset game on end screen
		if (_reset) {
			if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
				_reset = false;
				ResetGame();
				return;
			}
		}
		//check all inputs for multitouch, left side of screen for jump, right for warp.
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

		var topchar = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y - 0.5;
		var bottomchar = transform.position.y + 0.75;
		var topscreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
		if (!_upsidedown &&  topchar>=topscreen || _upsidedown && bottomchar <= -topscreen) {
			FallDelay();
			return;
		}

		//stop jump short if jump isn't held
		if (!_shorthop) return;
		//don't stop jumping if you haven't reached the minimum height
		if (_upsidedown && !(transform.position.y < _maxShortHopHeight)) return;
		if (!_upsidedown && !(transform.position.y > _maxShortHopHeight)) return;
		//make sure you're moving up when you quit jumping
		if ((!(_rb2D.velocity.y > 0) || _upsidedown) && (!(_rb2D.velocity.y < 0) || !_upsidedown)) return;
		FallDelay();
		#endif
	}

	//Calculates the max jump height from an impulse
	private float JumpMax(float height) {
		float g = _rb2D.gravityScale * Physics2D.gravity.magnitude;
		float v0 = height / _rb2D.mass; // converts the jumpForce to an initial velocity
		if (!_upsidedown) return transform.position.y + (v0 * v0)/(2*g) -_maxjumpbuffer;
			return transform.position.y - (v0 * v0)/(2*g) + _maxjumpbuffer;
	}
	//Saves velocity and sets gravity to 0 for a small delay before falling
	private void FallDelay() {
		if (!_falldelay) return;
		_jumpvelocity = _rb2D.velocity * new Vector2(1, 0);
		_rb2D.gravityScale = 0;
		_rb2D.velocity = Vector2.zero;
		_gravtimer = GravDelay;
		_falldelay = false;
	}

	private void Fall() {
		_rb2D.gravityScale = _startgrav;
		_rb2D.velocity = _jumpvelocity;
		GetComponent<Animator>().SetBool("Jumping", false);
		GetComponent<Animator>().SetBool("Falling", true);
	}

	private void UpdateTrail() {
		ParticleSystem.MainModule p = GetComponentsInChildren<ParticleSystem>()[1].main;
		p.startSpeed = new ParticleSystem.MinMaxCurve(FindObjectOfType<SpawnHandler>().Speed);
 	}
	
	private void OnCollisionEnter2D(Collision2D other) {
		//Collision code which allows you to jump on top of obstacles
		if (other.gameObject.CompareTag("Floor")) return;
		if (other.gameObject.CompareTag("Obstacle")) {
			if (other.transform.position.x + other.gameObject.GetComponent<SpriteRenderer>().bounds.size.x - 0.05 < transform.position.x) return;
		if ((other.transform.position.y + other.gameObject.GetComponent<SpriteRenderer>().bounds.size.y - 0.03f< transform.position.y && !_upsidedown) 
		    || (other.transform.position.y + 0.03f > transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2 && _upsidedown)) 
			return;
		}
		//If it hasn't returned, you died.
		Death();
	}
	private bool CheckIfGrounded() {
		//We raycast down 1 pixel from this position to check for a collider
		Vector2 backcharacter = new Vector2(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2,
			transform.position.y);
		var hits = Physics2D.RaycastAll(transform.position, _upsidedown ? new Vector2(0, 1) : new Vector2(0, -1), 1f);
		var backhits = Physics2D.RaycastAll(backcharacter, _upsidedown ? new Vector2(0, 1) : new Vector2(0, -1), 1f);
		//if a collider was hit, we are grounded
		return hits.Length > 1 || backhits.Length > 1;
	}

	private void Death() {
		//play death particles
		GetComponentInChildren<ParticleSystem>().Play();
		GetComponentsInChildren<ParticleSystem>()[1].Pause();
		GetComponentsInChildren<ParticleSystem>()[1].Clear();
		//save new high score
		PlayerPrefs.SetInt("Highscore", Highscore);
		//open up the end card
		GameObject.FindGameObjectWithTag("endcard").
			GetComponent<EndCard>().
			On(GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Ticker, 
				Highscore, _newscore);
		_newscore = false;
		//reset other componenets and shake camera.
		GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnHandler>().GameOver();
		Camera.main.GetComponent<CameraShake>().StartShake(0.5f, 3);
		GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Pause = true;
		GetComponent<SpriteRenderer>().enabled = false;
		GameObject.FindGameObjectWithTag("Shadow").GetComponent<SpriteRenderer>().enabled = false;
		_reset = true;
	}
	public void ResetGame() {
		//resets components
		if (_upsidedown) {
			Invert();
		}
		GetComponentsInChildren<ParticleSystem>()[1].Play();
		GetComponent<SpriteRenderer>().enabled = true;
		GameObject.FindGameObjectWithTag("Shadow").GetComponent<SpriteRenderer>().enabled = true;
		//saves highscore permanently 
		GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Reset();
		ResetPlayer();
		GameObject.FindGameObjectWithTag("Timer").GetComponent<UiTimer>().Pause = false;
		GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnHandler>().Pause = false;
		GameObject.FindGameObjectWithTag("endcard").GetComponent<EndCard>().Off();
	}
	//resets player position
	private void ResetPlayer() {
		transform.position = _startPosition;
		_jumpable = false;
		_rb2D.gravityScale = _startgrav;
	}
}
