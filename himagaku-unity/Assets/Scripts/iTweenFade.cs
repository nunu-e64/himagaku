using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class iTweenFade : MonoBehaviour {

    public static void Fade(GameObject target, float from, float to, float time) {
        if (target.GetComponent<iTweenFade>() == null) {
            target.AddComponent<iTweenFade>();
        }

        iTween.ValueTo(target, iTween.Hash("from", from, "to", to, "time", time, "onupdate", "SetAlpha"));
    }

    private void SetAlpha(float alpha) {
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = alpha;
        this.GetComponent<SpriteRenderer>().color = color;
	}
}
