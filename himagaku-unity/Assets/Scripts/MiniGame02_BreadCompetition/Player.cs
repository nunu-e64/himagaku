using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private Vector3 nextPosition;
	int score = 0;

	[SerializeField] Text scoreText;

	// Use this for initialization
	void Start () {
		nextPosition = this.transform.position;
		scoreText.text = "集めたパン：" + score + " 個";
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, 1.0f);
	}

	public void MoveToX(float moveToX) {
		
		this.nextPosition.x = moveToX;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy")
			HitEnemy(other);
		else if(other.tag == "Item")
			HitBread(other);
	}

	private void HitEnemy(Collider2D enemy) {
		Debug.Log("hit");
	}

	private void HitBread(Collider2D bread) {
		if(!bread.GetComponent<Bread>().GetIsActive())
			return;
		
		score++;
		scoreText.text = "集めたパン：" + score + " 個";
		bread.GetComponent<Bread>().SetIsActive(false);
	}
}
