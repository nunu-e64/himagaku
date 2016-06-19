using UnityEngine;
using System.Collections;

public class PuzzleTile : MonoBehaviour {

	private Sprite sprite;
	private int col, row;
	private int trueCol, trueRow;

	// Use this for initialization
	void Start () {
	}

	public void Initialize(int row, int col, int trueRow, int trueCol, Sprite sprite) {
		this.col = col;
		this.row = row;
		this.trueCol = trueCol;
		this.trueRow = trueRow;
		this.sprite = sprite;

		this.GetComponent<SpriteRenderer>().sprite = sprite;
        float x = col * sprite.rect.width / sprite.pixelsPerUnit;
		float y = row * sprite.rect.height / sprite.pixelsPerUnit;
		this.transform.position = new Vector2(x, y);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
