using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Vector3 nextPosition;

	// Use this for initialization
	void Start () {
		nextPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, 1.0f);
	}

	public void MoveToX(float moveToX) {
		
		this.nextPosition.x = moveToX;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("hit");
	}
}
