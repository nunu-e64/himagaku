using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChangeButton : BasicButton {
    [SerializeField]private string nextSceneName;

    protected override void DoEvent() {
        Debug.Log(nextSceneName);
        GameObject filter = GameObject.FindWithTag("Filter");
        iTweenExt.ColorTo(filter, iTween.Hash("from", new Color(0, 0, 0, 0.0f), "to", new Color(0, 0, 0, 1.0f), "time", 0.2f, "oncomplete", "ChangeScene", "oncompletetarget", this.gameObject));
    }

    private void ChangeScene() {
        SceneManager.LoadScene(nextSceneName);
    }
}
