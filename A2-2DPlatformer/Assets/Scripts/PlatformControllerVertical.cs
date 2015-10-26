using UnityEngine;
using System.Collections;

public class PlatformControllerVertical : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.1f;
	public bool moveUp = true;

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
			moveUp = !moveUp;
			counter = 0;
		}

		if (moveUp) {
			this._transform.position += new Vector3(0,speed,0);
		} else {
			this._transform.position -= new Vector3(0,speed,0);
		}
		counter += 1;
	}
}
