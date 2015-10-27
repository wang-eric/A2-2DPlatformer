using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private bool fallOff = false;
	private bool win = false;
	private float groundRadius = 0.5f;
	public LayerMask whatIsGround;
	public LayerMask whatIsDeathTrigger;
	public LayerMask whatIsFinish;
	private Animator anim;
	private Rigidbody2D rb2d;
	private AudioSource[] _audioSources;
	private AudioSource _coinSound;
	private AudioSource _jumpSound;
	private AudioSource _walkSound;

	private GameController gameController;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._coinSound = this._audioSources[0];
		this._jumpSound = this._audioSources [1];
		this._walkSound = this._audioSources [2];
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame

	/*
	void OnTriggerEnter2D(Collision2D otherCollider) {
		/*
		if (otherCollider.gameObject.CompareTag ("Platform")) {
			grounded = true;

		if (otherCollider.gameObject.CompareTag ("Coin")){
			this._coinSound.Play();
		}

	}
*/



	void Update () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		fallOff = Physics2D.OverlapCircle (groundCheck.position, 10f, whatIsDeathTrigger);
		win = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsFinish);
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
			grounded = false;
		}
		// When the player fall off the edge, life -1, respawn the player at the last checkpoint, 
		// or end the game when the player runs out of life.
		if (fallOff) {
			gameController.RemoveLife ();
			if (gameController.GetLife () == 0) {
				//Destroy(other.gameObject);
				gameController.GameOver();
			} else {
				gameController.RespawnTrigger ();
				transform.position = gameController.GetSpawnPoint();
				gameController.RespawnTrigger();
			}
		}
		if (win) {
			gameController.Win();
		}
	}

	void FixedUpdate()
	{
		// Player movement
		float h = Input.GetAxis ("Horizontal");
		if (h != 0) {
			// Play Walk animation
			anim.SetInteger ("AnimState", 1);
			if (grounded && !this._walkSound.isPlaying)
				this._walkSound.Play();
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);
			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		} else{
			// Play Idle animation
			anim.SetInteger ("AnimState", 0);
		}
		// Flip the player sprite base on the facing direction
		if (h>0 && !facingRight)
			Flip ();
		else if (h<0 && facingRight)
			Flip ();
		if (jump){
			// Play jump sound
			this._jumpSound.Play();
			// Apply jump force
			rb2d.AddForce (new Vector2(0f,jumpForce));
			jump = false;
			// Set grounded back to true to avoid double jump
			grounded = true;
			// Play jump animation
			anim.SetInteger ("AnimState", 2);

		}
		if (gameController.respawn) {
			// Check if the player needs to be respawned (after getting hit by zombie.
			transform.position = gameController.GetSpawnPoint();
			gameController.RespawnTrigger();
		}
	}

	// Flips the player sprite
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
