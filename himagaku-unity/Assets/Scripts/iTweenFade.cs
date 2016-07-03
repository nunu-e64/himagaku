using UnityEngine;
using System.Collections;

public class iTweenFade : MonoBehaviour {

    public static void Fade(GameObject target, float from, float to, float time) {
        iTween.ValueTo(target, iTween.Hash('onupdate', setColor, 'parameter', 'from', from, 'to', to, 'time', time));
                       
    }

    private void SetAlpha(float alpha) {
        
}
