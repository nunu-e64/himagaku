using UnityEngine;
using System.Collections;

using minigame02;

public class TouchSystem : MonoBehaviour {

	private Vector3 cursorPos;
	private Vector2 playerPos;

	private GameObject player;

	// Use this for initialization
	void Start() {
		this.player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if (this.IsTouchEnter()) {
			this.cursorPos = this.GetTouchPoint();
			this.MovePlayer(this.cursorPos);
		}
	}

	void MovePlayer(Vector3 cursorPos) {

		float moveToX = Constants.LANE_CENTER_X;

		if(cursorPos.x < -1)
			moveToX = Constants.LANE_LEFT_X;
		else if(cursorPos.x > 1)
			moveToX = Constants.LANE_RIGHT_X;
		
		player.GetComponent<Player>().MoveToX(moveToX);
	}

	bool IsTouchEnter() {
		return Input.GetMouseButtonDown(0) || Input.touchCount > 0;
	}

	Vector3 GetTouchPoint() {
		if(Input.GetMouseButtonDown(0)) 
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if(Input.touchCount > 0)
			return Camera.main.ScreenToWorldPoint(Input.touches[0].position);
		
		return new Vector3(0, 0, 0);
	}
}
