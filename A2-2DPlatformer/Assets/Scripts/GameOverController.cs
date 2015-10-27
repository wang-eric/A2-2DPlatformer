using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		this._SetScore ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void _SetScore() {
		this.scoreLabel.text = "Score: " + PlayerPrefs.GetInt("currentGameScore");
	}

	// Restart Button Event Handler
	public void OnRestartButtonClick() {
		Application.LoadLevel ("Main");
	}
}
