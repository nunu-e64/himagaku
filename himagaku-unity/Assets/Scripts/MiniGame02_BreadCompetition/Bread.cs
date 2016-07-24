using UnityEngine;
using System.Collections;

using minigame02;

public class Bread : MonoBehaviour {
	
	[SerializeField] float delayTime;
	private float time;
	private float speed;
	private float posY;

	private bool isActive = true;

	// Use this for initialization
	void Start() {
		this.time = 0;
		this.posY = this.transform.position.y;
	}

	// Update is called once per frame
	void Update() {
		this.time += Time.deltaTime;
		if(this.delayTime < this.time) {
			this.Move();
		}
	}

	private void Move() {
		this.speed = (this.time / 100) + 0.1f;
		this.posY -= (this.speed * Time.deltaTime * 60);
		if(this.posY < Constants.BREAD_END_POSITION_Y) {
			this.InitPosition();
		}
		Vector3 pos = this.transform.position;
		pos.y = this.posY;
		this.transform.position = pos;
	}

	void InitPosition() {
		SetIsActive(true);
		float diffY = this.posY - Constants.BREAD_END_POSITION_Y;
		this.posY = Constants.BREAD_INIT_POSITION_Y + diffY;

		float rand = Random.value;
		if(rand < 0.3f)
			this.transform.position = new Vector3(Constants.LANE_LEFT_X, this.posY, 0);
		else if(rand < 0.6f)
			this.transform.position = new Vector3(Constants.LANE_RIGHT_X, this.posY, 0);
		else
			this.transform.position = new Vector3(Constants.LANE_CENTER_X, this.posY, 0);
	}
		
	public void SetIsActive(bool isActive) {
		this.isActive = isActive;
		this.GetComponent<Renderer>().enabled = isActive;			
	}

	public bool GetIsActive() {
		return this.isActive;
	}
}
