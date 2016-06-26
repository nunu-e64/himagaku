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

	public void Move(Vector3 movement) {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(nextPosition);
		screenPos += movement;
		this.nextPosition = Camera.main.ScreenToWorldPoint(screenPos);
	}


	public float GetScreenPointLeftX() {
		Vector3 worldLeft = this.transform.position; 
		worldLeft.x -= this.transform.localScale.x / 2;

		Vector3 screenLeft = Camera.main.WorldToScreenPoint(worldLeft);
		return screenLeft.x;
	}

	public float GetScreenPointRightX() {
		Vector3 worldRight = this.transform.position;
		worldRight.x += this.transform.localScale.x / 2;

		Vector3 screenRight = Camera.main.WorldToScreenPoint(worldRight);
		return screenRight.x;
	}
}
