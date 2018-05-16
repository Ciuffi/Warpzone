using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
	private Rigidbody2D rb2d;
	private GameObject floor;
	public bool Upsidedown;
	private GameObject[] backgrounds;
	private GameObject shadow;
	public float Speed = 40;
	public float Height = 10;

	private bool jumpable;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		floor = GameObject.FindGameObjectWithTag("Floor");
		backgrounds = GameObject.FindGameObjectsWithTag("Background");
		shadow = GameObject.FindGameObjectWithTag("Shadow");
	}

	private void invert() {
		FindObjectOfType<Canvas>().GetComponent<uiInverter>().invert();
		if (Upsidedown) {
			Upsidedown = false;
			Physics2D.gravity = new Vector2(0, -10);
		}
		else {
			Upsidedown = true;
			Physics2D.gravity = new Vector2(0, 10);
		}
		foreach (GameObject background in backgrounds) {
			background.GetComponent<invert>().Invert();
		}
		warp();
	}
	private void warp() {
		rb2d.velocity *= Vector2.down;
		transform.position = shadow.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && jumpable) {
			float floatheight = Height;
			if (Upsidedown) {
				floatheight *= -1;
			}
			rb2d.AddForce(new Vector2(0,floatheight), ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			invert();
		}
		rb2d.AddForce (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")) * Speed);
	}

	private void back() {
		transform.position = new Vector3(-7, 1, -01);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Floor")) {
			jumpable = true;
		}
		if (other.gameObject.CompareTag("Obstacle")) {
			GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().gameOver();
			if (Upsidedown) {
				invert();
			}

			GameObject.FindGameObjectWithTag("Timer").GetComponent<timer>().reset();
			back();
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.CompareTag("Floor")) {
			jumpable = false;
		}
	}
}
