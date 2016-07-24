using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Vector3 nextPosition;
	private int score = 0;
	private bool isDead = false;


	// Use this for initialization
	void Start() {
		this.nextPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.nextPosition, Time.deltaTime * 60);
	}

	public void MoveToX(float moveToX) {
		this.nextPosition.x = moveToX;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy")
			this.HitEnemy(other);
		else if(other.tag == "Item")
			this.HitBread(other);
	}

	private void HitEnemy(Collider2D enemy) {
		Debug.Log("hit");
		isDead = true;
	}

	private void HitBread(Collider2D bread) {
		if(!bread.GetComponent<Bread>().GetIsActive())
			return;
		
		this.score++;
		bread.GetComponent<Bread>().SetIsActive(false);
	}

	public int GetScore() {
		return this.score;
	}

	public bool GetIsDead() {
		return isDead;
	}
}
