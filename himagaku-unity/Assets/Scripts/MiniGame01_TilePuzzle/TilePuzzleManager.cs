using UnityEngine;
using System.Collections;
using System.Linq;

public class TilePuzzleManager : MonoBehaviour {

    [SerializeField]
	private GameObject tile;
    [SerializeField]
    tileSpriteList[] tileSprites;

    [System.Serializable]
    public class tileSpriteList {
        public Sprite[] sprites;
    }
 //   [System.Serializable]
	//public struct TileSprite {
	//	public Sprite sprite;
	//	public int col;
 //       public int row;
	//}

    // Use this for initialization
    void Start() {
        // Create Tile Sprite
        int rows = tileSprites.GetLength(0);
        int cols = tileSprites[0].sprites.GetLength(0);
        int[] ary = Enumerable.Range(0, rows*cols).Select(i => (int)i).ToArray();
        Shuffle(ary);
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
                int rdm = ary[i * cols + j];
                GameObject newTile = Instantiate<GameObject>(tile);
                newTile.GetComponent<PuzzleTile>().Initialize(i, j, rdm/cols, rdm%cols, tileSprites[rdm/cols].sprites[rdm%cols]);
                newTile.transform.SetParent(this.transform);
			}
		}

        // Set Position
        Sprite tmpSprite = tileSprites[0].sprites[0];
        this.transform.position = -1 * new Vector2((cols-0.5f) * tmpSprite.rect.width, (rows-0.5f) * tmpSprite.rect.height) / 2 / tmpSprite.pixelsPerUnit;
	}

	void Shuffle(int[] ary) {
		System.Random rng = new System.Random();
		int n = ary.Length;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			int tmp = ary[k];
			ary[k] = ary[n];
			ary[n] = tmp;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
