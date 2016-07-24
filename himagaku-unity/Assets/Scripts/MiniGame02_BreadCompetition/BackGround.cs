using UnityEngine;
using System.Collections;
using minigame02;

public class BackGround : MonoBehaviour {
	
	float time;
	float speed;
	float posY;

	// Use this for initialization
	void Start() {
		time = 0;
		posY = this.transform.position.y;
	}

	// Update is called once per frame
	void Update() {
		time += Time.deltaTime;
		Move();
	}

	private void Move() {
		speed = (time / 100) + 0.1f;
		posY -= (speed * Time.deltaTime * 60);
		if(posY < Constants.BACKGROUND_END_POSITION_Y) {
			float diffY = posY - Constants.BACKGROUND_END_POSITION_Y;
			posY = Constants.BACKGROUND_INIT_POSITION_Y + diffY;
		}
		Vector3 pos = this.transform.position;
		pos.y = posY;
		this.transform.position = pos;
	}
}
