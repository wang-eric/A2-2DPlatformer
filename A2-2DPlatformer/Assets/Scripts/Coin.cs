using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	private GameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Destroy the coin if hit by player.
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			gameController.AddScore(10);
			GetComponent<AudioSource>().Play();
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			// Delay 1f for the destroy action to ensure the audio is played
			Destroy (gameObject,1f);
		}
	}
}
