using UnityEngine;
using System.Collections;

public class SceneChangeButton : BasicButton {
    [SerializeField]private string nextSceneName;

    protected override void DoEvent() {
        Debug.Log(nextSceneName);
    }
}
