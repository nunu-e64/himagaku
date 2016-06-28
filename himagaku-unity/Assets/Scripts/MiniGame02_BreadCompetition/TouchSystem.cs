using UnityEngine;
using System.Collections;

public class TouchSystem : MonoBehaviour {

	Vector3 cursorPos;
	Vector2 playerPos;

	private int laneInterval = Screen.width / 3;

	GameObject player;

	// Use this for initialization
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if (isTouchEnter()) {
			cursorPos = getTouchPoint();
			movePlayer(cursorPos);
		}
	}

	void movePlayer(Vector3 cursorPos) {
		
		Vector3 movement = Vector3.zero;

		if(cursorPos.x < player.GetComponent<Player>().GetScreenPointLeftX())
			movement.x = -laneInterval;
		else if(cursorPos.x > player.GetComponent<Player>().GetScreenPointRightX())
			movement.x = laneInterval;
		
		player.GetComponent<Player>().Move(movement);
	}

	bool isTouchEnter() {
		return Input.GetMouseButtonDown(0) || Input.touchCount > 0;
	}

	Vector3 getTouchPoint() {

		if(Input.GetMouseButtonDown(0)) 
			return Input.mousePosition;
		
		if(Input.touchCount > 0)
			return Input.touches[0].position;
		
		return new Vector3(0, 0, 0);
	}
}
