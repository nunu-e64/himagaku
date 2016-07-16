using UnityEngine;
using System.Collections;

public class InputManager : SingletonMonoBehaviour<InputManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // タップ押下有無を返し、クリック位置のワールド座標を取得
    public bool GetTouchBeganPosition(out Vector3 touchPos) {
        touchPos = new Vector3();
        if (Input.GetMouseButtonDown(0)) {
            touchPos = Input.mousePosition;
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            touchPos = Input.GetTouch(0).position;
        } else {
            return false;

        }
        touchPos.z = -Camera.main.transform.position.z;
        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        return true;
    }

    public bool GetTouchBegan() {
        Vector3 dummy;
        return GetTouchBeganPosition(out dummy);
    }
}
