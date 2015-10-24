using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			GetComponent<AudioSource>().Play();
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			Destroy (gameObject,1f);
		}
	}
}
