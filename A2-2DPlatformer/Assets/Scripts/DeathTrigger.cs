using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

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
	/* Method 2 for collision detection, replaced by groundCheck in playerController
	void OnTriggerExit2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			gameController.RemoveLife();
			if (gameController.GetLife() == 0) {
				//Destroy(other.gameObject);
				gameController.GameOver();
			}
			else
			{
				gameController.RespawnTrigger();
			}
		}
	}
	*/
}
