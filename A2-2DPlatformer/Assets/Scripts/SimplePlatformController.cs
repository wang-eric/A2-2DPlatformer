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
	private float groundRadius = 0.5f;
	public LayerMask whatIsGround;
	private Animator anim;
	private Rigidbody2D rb2d;
	private AudioSource[] _audioSources;
	private AudioSource _coinSound;
	private AudioSource _jumpSound;
	private AudioSource _walkSound;

	
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._coinSound = this._audioSources[0];
		this._jumpSound = this._audioSources [1];
		this._walkSound = this._audioSources [2];
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
		Debug.Log ("grounded");
		Debug.Log (grounded);
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
			grounded = false;
			Debug.Log ("jump1");
			Debug.Log (jump);
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		//if (h != 0 || rb2d.velocity.x !=0 ) {
		if (h != 0) {
			anim.SetInteger ("AnimState", 1);
			if (grounded && !this._walkSound.isPlaying)
				this._walkSound.Play();
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);
			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		} else{
			anim.SetInteger ("AnimState", 0);
		}
		if (h>0 && !facingRight)
			Flip ();
		else if (h<0 && facingRight)
			Flip ();
		if (jump){
			this._jumpSound.Play();
			rb2d.AddForce (new Vector2(0f,jumpForce));
			jump = false;
			grounded = true;
			anim.SetInteger ("AnimState", 2);
			Debug.Log ("grounded");
			Debug.Log (grounded);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
