using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    [SerializeField] private GameObject filter; 
    private bool canGoNext;

    // Use this for initialization
	void Start () {
        canGoNext = false;
        iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 2.0f, "oncomplete", "FinishOpening"));
	}

    void FinishOpening () {
        canGoNext = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (canGoNext && InputManager.Instance.GetTouchBegan()) {
            //NEXT SCENE
        }
	}
}
