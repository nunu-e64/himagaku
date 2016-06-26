using UnityEngine;
using System.Collections;

namespace MiniGame01_TilePuzzle {

    public class Tile : MonoBehaviour {

        public int col;
        public int row;
        private int trueCol, trueRow;
        private Sprite sprite;

        private Vector3 currentTransformPosition;
        private const float Z_HOVER = -1.0f;
        private bool iTweenMoving = true;
        public bool isCorrectPosition { get; private set; }

        public Tile Clone() {
            Tile tile = new Tile();
            tile.col = this.col;
            tile.row = this.row;
            tile.trueCol = this.trueCol;
            tile.trueRow = this.trueRow;
            tile.sprite = this.sprite;
            tile.isCorrectPosition = this.isCorrectPosition;
            return tile;
        }

        public void Initialize(int col, int row, int trueRow, int trueCol, Sprite sprite) {
            this.col = col;
            this.row = row;
            this.trueCol = trueCol;
            this.trueRow = trueRow;
            this.sprite = sprite;
            this.isCorrectPosition = (col == trueCol && row == trueRow);

            this.GetComponent<SpriteRenderer>().sprite = sprite;
            this.currentTransformPosition = this.GetTransformPosition(this.col, this.row, this.sprite);
            this.transform.localPosition = this.currentTransformPosition;
        }

        public void ChangePosition(Vector2 tilePos) {
            this.col = (int) tilePos.x;
            this.row = (int) tilePos.y;
            this.isCorrectPosition = (this.col == this.trueCol && this.row == this.trueRow);
            this.currentTransformPosition = this.GetTransformPosition(this.col, this.row, this.sprite);

            iTweenExtention.SerialPlay(
                this.gameObject
                , (iTweenAction)iTween.MoveTo, iTween.Hash("position", this.GetHoverPosition(), "time", 0.5f, "isLocal", true)
                , (iTweenAction)iTween.MoveTo, iTween.Hash("position", this.currentTransformPosition, "time", 0.2f, "isLocal", true)
            );
        }

        public void Selected() {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", this.GetHoverPosition(), "time", 0.3f, "isLocal", true));
        }

        public void Canceled() {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", this.currentTransformPosition, "time", 0.3f, "isLocal", true));
        }

        Vector2 GetTransformPosition(int tileCol, int tileRow, Sprite tileSprite) {
            float x = tileCol * sprite.rect.width / tileSprite.pixelsPerUnit;
            float y = tileRow * sprite.rect.height / tileSprite.pixelsPerUnit;
            Debug.Log(tileCol + ":" + tileRow + " x:" + x + " y:" + y);
            return new Vector2(x, y);
        }

        Vector3 GetHoverPosition() {
            Vector3 hoverTargetPos = this.currentTransformPosition;
            hoverTargetPos.z = Z_HOVER;
            return hoverTargetPos;
        }

        public Vector2 GetTilePos() {
            return new Vector2(this.col, this.row);
        }
    }
}
