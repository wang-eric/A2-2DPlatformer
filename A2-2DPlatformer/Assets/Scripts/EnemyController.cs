using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.5f;
	public bool moveRight;

	// PRIVATE INSTANCE VARIABLES
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator;

	public Transform frontCheck;
	public float checkRadius = 0.1f;
	public LayerMask whatIsWall;
	private bool hittingWall;

	public Transform edgeCheck;
	private bool notAtEdge;

	public LayerMask whatIsPlayer;

	public Transform topCheck1;
	public Transform topCheck2;
	public Transform topCheck3;
	public Transform backCheck1;
	public Transform backCheck2;
	private bool hittingPlayerFront,hittingPlayerBack;
	private bool hittingPlayerTop;
	private bool enemyKilled = false;

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
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// check if enemy is grounded
		if (!enemyKilled) {
			hittingWall = Physics2D.OverlapCircle (frontCheck.position, checkRadius, whatIsWall);
			hittingPlayerFront = Physics2D.OverlapCircle (frontCheck.position, checkRadius, whatIsPlayer);
			hittingPlayerBack = (Physics2D.OverlapCircle (backCheck1.position, checkRadius, whatIsPlayer))
				|| Physics2D.OverlapCircle (backCheck2.position, checkRadius, whatIsPlayer);
			hittingPlayerTop = (Physics2D.OverlapCircle (topCheck1.position, checkRadius, whatIsPlayer))
				|| (Physics2D.OverlapCircle (topCheck2.position, checkRadius, whatIsPlayer))
				|| (Physics2D.OverlapCircle (topCheck3.position, checkRadius, whatIsPlayer));

			// Check for the edge
			notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, checkRadius, whatIsWall);
			// Move to one direct if the zombie is not hitting a wall or near the edge
			if (hittingWall || !notAtEdge) {
				moveRight = !moveRight;
			}

			// Player get killed when hit by a zombie
			if (hittingPlayerFront || hittingPlayerBack){
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

		// Kill zombies when the player jump on their heads
		if (hittingPlayerTop) {
			GetComponent<AudioSource> ().Play ();
			//this.gameObject.GetComponent<Renderer> ().enabled = false;
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			gameController.AddScore(50);
			Destroy (gameObject, 1f);
			hittingPlayerTop=false;
			enemyKilled = true;
			//Destroy (this.gameObject);
		}

		if (moveRight) {
			this._transform.localScale = new Vector3(-2.8f, 2.8f, 1f); 
			this._rigidbody2D.velocity = new Vector2 (speed, this._rigidbody2D.velocity.y);
		} else {
			this._transform.localScale = new Vector3(2.8f, 2.8f, 1f); 
			this._rigidbody2D.velocity = new Vector2 (-speed, this._rigidbody2D.velocity.y);
		}
	}
}
