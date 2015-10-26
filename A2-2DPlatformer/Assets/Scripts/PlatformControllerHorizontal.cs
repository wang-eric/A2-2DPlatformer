using UnityEngine;
using System.Collections;

public class PlatformControllerHorizontal : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.1f;
	public bool moveRight = true;

	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	
	public int moveRange = 20;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		this._transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == moveRange){
			moveRight = !moveRight;
			counter = 0;
		}

		if (moveRight) {
			this._transform.position += new Vector3(speed,0,0);
		} else {
			this._transform.position -= new Vector3(speed,0,0);
		}
		counter += 1;
	}
}
