using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

namespace MiniGame01_TilePuzzle {
    public class TilePuzzleManager : MonoBehaviour {

        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Text timeText;
        [SerializeField] private Text clearText;
        [SerializeField] private Text startText;
        [SerializeField] private GameObject backGround;
        [SerializeField] private GameObject filter;
        [SerializeField] private GameObject frame;

        [SerializeField] private tileSpriteList[] tileSprites;
        [System.Serializable] public class tileSpriteList {
            public Sprite[] sprites;
        }

        private Tile[,] tiles;
        private Tile firstTouchTile;
        private bool isClear;
        private bool isPlaying;
        private bool didClearAnimation;
        private float time;

        // タイルのスプライト作成とランダム初期配置
        void Start() {
            this.firstTouchTile = null;
            this.isClear = false;
            this.isPlaying = false;
            this.didClearAnimation = false;
            this.time = 0.0f;
            this.clearText.gameObject.SetActive(false);
            this.timeText.gameObject.SetActive(false);
            this.startText.gameObject.SetActive(false);

            this.InitializeTiles();
            this.Opening();
        }

        void InitializeTiles() {
            int rows = this.tileSprites.GetLength(0);
            int cols = this.tileSprites[0].sprites.GetLength(0);
            tiles = new Tile[rows, cols];
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    GameObject newTile = Instantiate<GameObject>(this.tilePrefab);
                    newTile.transform.SetParent(this.transform);
                    newTile.transform.localScale = Vector3.one;
                    newTile.GetComponent<Tile>().Initialize(j, i, j, i, this.tileSprites[i].sprites[j]);
                    tiles[i, j] = newTile.GetComponent<Tile>();
                }
            }
        }

        void Opening() {
            iTweenExtention.SerialPlay(
                this.gameObject,
                (iTweenAction)iTweenExt.ColorTo, this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 1.0f),
                (iTweenAction)iTween.MoveTo, this.frame, iTween.Hash("x", -10.0f, "time", 1.0f, "oncomplete", "ShuffleTiles", "oncompletetarget", this.gameObject)
            );
        }

        // パネルをシャッフルして配置
        [ContextMenu("ShuffleTiles")]
        void ShuffleTiles() {
            int rows = this.tileSprites.GetLength(0);
            int cols = this.tileSprites[0].sprites.GetLength(0);
            int[] ary = Enumerable.Range(0, rows * cols).Select(i => (int)i).ToArray();
            Util.ShuffleIntArray(ary);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    int rdm = ary[i * cols + j];
                    GameObject newTile = tiles[i, j].gameObject;
                    newTile.transform.SetParent(this.transform);
                    newTile.transform.localScale = Vector3.one;
                    newTile.GetComponent<Tile>().Initialize(j, i, rdm % cols, rdm / cols, this.tileSprites[rdm / cols].sprites[rdm % cols]);
                }
            }

            // Animation
            iTweenExtention.SerialPlay(
                this.gameObject,
                (iTweenAction)iTween.MoveTo, this.frame, iTween.Hash("position", Vector3.zero, "time", 0.0f),
                (iTweenAction)iTween.ScaleFrom, this.frame, iTween.Hash("scale", new Vector3(7, 7, 7), "time", 1.0f, "oncomplete", "StartAnimation", "oncompletetarget", this.gameObject)
            );
            this.timeText.gameObject.SetActive(true);
            iTween.MoveFrom(this.timeText.gameObject, iTween.Hash("y", 300, "time", 1.0f, "islocal", true));
        }

        void StartAnimation() {
            this.startText.gameObject.SetActive(true);
            iTweenExtention.SerialPlay(
                this.gameObject,
                (iTweenAction)iTweenExt.FadeToWithChildren, this.startText.gameObject, iTween.Hash("from", 0, "to", 1, "time", 0.5f),
                (iTweenAction)iTweenExt.FadeToWithChildren, this.startText.gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.5f, "delay", 0.5f, "oncomplete", "GameStart", "oncompletetarget", this.gameObject)
            );
            iTweenExtention.SerialPlay(
                this.gameObject,
                (iTweenAction)iTween.MoveFrom, this.startText.gameObject, iTween.Hash("x", -200, "time", 0.5f, "islocal", true),
                (iTweenAction)iTween.MoveTo, this.startText.gameObject, iTween.Hash("x", 200, "time", 0.5f, "islocal", true, "delay", 0.5f)
            );
        }

        void GameStart() {
            this.isPlaying = true;
            this.CheckClear();
        }

        void Update() {
            if (this.isClear && !this.didClearAnimation) {
                // Clear Animation
                this.didClearAnimation = true;;
                iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(1, 1, 1, 1), "to", new Color(1, 1, 1, 0), "time", 0.5f));
                iTweenExt.ColorTo(this.backGround, iTween.Hash("from", this.backGround.GetComponent<SpriteRenderer>().color, "to", new Color(1, 1, 1, 1), "time", 0f, "oncomplete", "ClearAnimation", "oncompletetarget", this.gameObject));
            }
            if (this.isClear || !this.isPlaying) {
                return;
            }


            // UpdateTimer
            this.time += Time.deltaTime;
            this.timeText.text = string.Format("{0:00}:{1:00}", time / 60, time % 60);

            // Check Tap
            Vector3 touchPos;
            if (!InputManager.Instance.GetTouchBeganPosition(out touchPos)) {
                return;
            }
            var touchedCollider = Physics2D.OverlapPoint(touchPos);
            if (touchedCollider == null) {
                return;
            }
            Tile touchedTile = touchedCollider.GetComponent<Tile>();
            if (touchedTile != null) {
                if (this.firstTouchTile == null) {
                    this.SelectFirstTouchTile(touchedTile);
                } else if (firstTouchTile != touchedTile) {
                    this.ExchangeTile(firstTouchTile, touchedTile);
                    this.firstTouchTile = null;
                } else {
                    this.CancelFirstTile();
                }
            }
        }

        // 一枚目選択
        void SelectFirstTouchTile(Tile tile) {
            this.firstTouchTile = tile;
            this.firstTouchTile.Selected();
        }

        // 入れ替え動作
        void ExchangeTile(Tile firstTile, Tile secondTile) {            
            Debug.Assert(firstTile != null);
            Debug.Assert(secondTile != null);
            Vector2 temp = firstTile.GetTilePos();
            firstTile.ChangePosition(secondTile.GetTilePos(), this.gameObject);
            secondTile.ChangePosition(temp, this.gameObject);
        }

        // 一枚目選択解除
        void CancelFirstTile() {
            this.firstTouchTile.Canceled();
            this.firstTouchTile = null;
        }

        // クリアチェック
        void CheckClear() {
            foreach (var tile in this.GetComponentsInChildren<Tile>()) {
                if (tile.IsCorrectPosition() == false) {
                    this.isClear = false;
                    Debug.Log("CheckClear: false");
                    return;
                }
            }

            this.isClear = true;
            this.didClearAnimation = false;
            Debug.Log("CheckClear: true");
        }

        void ClearAnimation() {
            this.clearText.gameObject.SetActive(true);
            iTween.MoveFrom(this.clearText.gameObject, iTween.Hash("y", -200, "islocal", true, "time", 1.0f));
            iTweenExt.FadeTo(this.clearText.gameObject, iTween.Hash("from", 0, "to", 1, "time", 1.0f));
        }
    }
}