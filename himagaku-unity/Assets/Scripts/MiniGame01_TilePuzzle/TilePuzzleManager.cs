﻿using UnityEngine;
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
        private Tile firstTouchTile;
        private bool isClear;

        // タイルのスプライト作成とランダム初期配置
        void Start() {
            this.firstTouchTile = null;
            this.isClear = false;
            this.clearText.gameObject.SetActive (false);
            this.timeText.gameObject.SetActive (false);
            this.startText.gameObject.SetActive (false);
            this.Opening ();
        }

        // パネルをシャッフルして配置
        [ContextMenu("InitializeTile")]
        void InitializePanels() {
            int rows = this.tileSprites.GetLength(0);
            int cols = this.tileSprites[0].sprites.GetLength(0);
            int[] ary = Enumerable.Range(0, rows * cols).Select(i => (int)i).ToArray();
            Util.ShuffleIntArray(ary);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    int rdm = ary[i * cols + j];
                    GameObject newTile = Instantiate<GameObject>(this.tilePrefab);
                    newTile.transform.SetParent(this.transform);
                    newTile.transform.localScale = Vector3.one;
                    newTile.GetComponent<Tile>().Initialize(j, i, rdm % cols, rdm / cols, this.tileSprites[rdm / cols].sprites[rdm % cols]);
                }
            }

            // Set Panel Position
            //Sprite tmpSprite = this.tileSprites[0].sprites[0];
            //this.transform.position = new Vector2(-1 * (cols - 1) * tmpSprite.rect.width, (rows - 1) * tmpSprite.rect.height) / 2 / tmpSprite.pixelsPerUnit;

            // Animation
            iTweenExtention.SerialPlay (
                this.gameObject,
                (iTweenAction)iTween.MoveTo, this.frame, iTween.Hash("position", Vector3.zero, "time", 0.0f),
                (iTweenAction)iTween.ScaleFrom, this.frame, iTween.Hash("scale", new Vector3 (7, 7, 7), "time",  1.0f, "oncomplete", "StartAnimation", "oncompletetarget", this.gameObject)
            );
            this.timeText.gameObject.SetActive (true);
            iTween.MoveFrom(this.timeText.gameObject, iTween.Hash("y", 300, "time", 1.0f, "islocal", true));
        }

        void Opening() {
            iTweenExtention.SerialPlay (
                this.gameObject,
                (iTweenAction) iTweenExt.FadeTo,  this.filter, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 1.0f),
                (iTweenAction) iTween.MoveTo,        this.frame,  iTween.Hash ("x", -10.0f, "time", 1.0f, "oncomplete", "InitializePanels", "oncompletetarget", this.gameObject)
            );
        }

        void StartAnimation() {
            this.startText.gameObject.SetActive (true);
            iTweenExtention.SerialPlay (
                this.gameObject,
                (iTweenAction)iTweenExt.FadeTo, this.startText, iTween.Hash ("from", 0, "to", 1, "time", 0.5f),
                (iTweenAction)iTweenExt.FadeTo, this.startText, iTween.Hash ("from", 1, "to", 0, "time", 0.5f)
            );
            iTweenExtention.SerialPlay (
                this.gameObject,
                (iTweenAction)iTween.MoveFrom, this.startText, iTween.Hash ("x", -100, "time", 0.5f),
                (iTweenAction)iTween.MoveTo, this.startText, iTween.Hash ("x", 100, "time", 0.5f)
            );
            this.CheckClear();
        }

        // タップ判定
        void Update() {
            if (this.isClear) {
                return;
            }

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
                    this.CheckClear();
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
            firstTile.ChangePosition(secondTile.GetTilePos());
            secondTile.ChangePosition(temp);
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

            this.clearText.gameObject.SetActive(true);
            this.isClear = true;
            Debug.Log("CheckClear: true");
        }
    }
}