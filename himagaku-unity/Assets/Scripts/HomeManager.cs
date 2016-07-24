using UnityEngine;
using System.Collections;

public class HomeManager : MonoBehaviour {

    [SerializeField] private GameObject filter; 
    private GameObject text;

    void Start () {
        text = GameObject.FindObjectOfType<UnityEngine.UI.Text>().gameObject;
//        text.SetActive(false);
        iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 0.2f, "oncomplete", "FinishOpening", "oncompletetarget", this.gameObject));
    }
	
    private void FinishOpening() {
        text.SetActive(true);
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
