using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    [SerializeField] private GameObject filter; 
    [SerializeField] private GameObject StartLabel;
    private bool canGoNext;

    // Use this for initialization
	void Start () {
        canGoNext = false;
        StartLabel.SetActive(false);
        iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 2.0f, "oncomplete", "FinishOpening", "oncompletetarget", this.gameObject));
	}

    void FinishOpening () {
        canGoNext = true;
        StartLabel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (canGoNext && InputManager.Instance.IsTouchBegan()) {
            iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 0.0f), "to", new Color(0, 0, 0, 1.0f), "time", 0.5f, "oncomplete", "GoNextScene", "oncompletetarget", this.gameObject));
        }
	}

    void GoNextScene () {
        SceneManager.LoadScene("Home");
    }
}
