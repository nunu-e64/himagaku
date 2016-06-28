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

        public void Initialize(int col, int row, int trueCol, int trueRow, Sprite sprite) {
            this.col = col;
            this.row = row;
            this.trueCol = trueCol;
            this.trueRow = trueRow;
            this.sprite = sprite;

            this.GetComponent<SpriteRenderer>().sprite = sprite;
            this.currentTransformPosition = this.GetTransformPosition(this.col, this.row, this.sprite);
            this.transform.localPosition = this.currentTransformPosition;
        }

        public void ChangePosition(Vector2 tilePos) {
            this.col = (int)tilePos.x;
            this.row = (int)tilePos.y;
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
            y *= -1;
            Debug.Log(tileCol + ":" + tileRow + " trueCol:" + this.trueCol + "," + this.trueRow);
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

        public bool IsCorrectPosition() {
            return (this.col == this.trueCol && this.row == this.trueRow);
        }
    }
}
