using UnityEngine;
using System.Collections;

public class HomeManager : MonoBehaviour {

    [SerializeField] private GameObject filter; 

    void Start () {
        iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 0.2f, "oncomplete", "FinishOpening"));
    }
	
	void Update () {
        // タップ処理
        Vector3 touchDownPos, touchUpPos;
        bool isTouchDown, isTouchUp;
        isTouchDown = InputManager.Instance.GetTouchBeganPosition(out touchDownPos);
        isTouchUp = InputManager.Instance.GetTouchEndPosition(out touchUpPos);
        if (!isTouchDown && !isTouchUp) {
            return;
        }

        var touchedCollider = Physics2D.OverlapPoint(isTouchDown ? touchDownPos : touchUpPos);
        if (touchedCollider == null) {
            return;
        }
        BasicButton button = touchedCollider.GetComponent<BasicButton>();
        if (button != null) {
            if (isTouchDown) {
                button.OnTouch();
            }
            if (isTouchUp) {
                button.OnRelease();
            }
        }
	}
}
