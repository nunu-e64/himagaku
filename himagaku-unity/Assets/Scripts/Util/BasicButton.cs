using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BasicButton : MonoBehaviour {
    private Vector3 originalScale;

    void Start() {
        originalScale = this.transform.localScale;
    }

    public void OnTouch () {
        Debug.Log("BasicButton: OnTouch");
        iTween.ScaleTo(this.gameObject, originalScale + new Vector3(0.1f, 0.1f, 0), 0.2f);
    }

    public void OnRelease () {
        Debug.Log("BasicButton: OnRelease");
        iTween.ScaleTo(this.gameObject, originalScale, 0.2f);
        DoEvent();
	}

    protected virtual void DoEvent() {
        Debug.Log("BasicButton: DoEvent");
    }
}
