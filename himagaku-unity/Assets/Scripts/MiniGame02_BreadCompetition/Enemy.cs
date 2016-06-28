using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int laneLeftX = Screen.width / 6;
	private int laneCenterX = Screen.width / 2;
	private int laneRightX = Screen.width * 5 / 6;

	[SerializeField] float delayTime;
	float time;
	float speed;

	Vector3 nextPosition;

	// Use this for initialization
	void Start () {
		time = 0;
		nextPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, speed);

		if(this.transform.position == nextPosition && time > delayTime)
			InitPosition();
	}

	void InitPosition() {
		float rand = Random.value;

		this.transform.position = new Vector3(0, 8, 0);
		
		if(rand < 0.3f)
			this.transform.position = new Vector3(-2, 8, 0);
		else if(rand < 0.6f)
			this.transform.position = new Vector3(2, 8, 0);
		else
			this.transform.position = new Vector3(0, 8, 0);

		delayTime = Random.value;
		speed = (time / 100) + 0.1f;
		nextPosition = this.transform.position;
		nextPosition.y = -8;
	}
}
