using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BreadCompetitionManager : MonoBehaviour {

	[SerializeField] Text scoreText;
	[SerializeField] GameObject player;

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
		this.UpdateScore();
	}

	void UpdateScore() {
		this.scoreText.text = "集めたパン：" + this.player.GetComponent<Player>().GetScore() + " 個";
	}
}
