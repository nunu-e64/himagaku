using UnityEngine;
using System.Collections;

public class HomeManager : MonoBehaviour {

    [SerializeField] private GameObject filter; 

    // Use this for initialization
    void Start () {
        iTweenExt.ColorTo(this.filter, iTween.Hash("from", new Color(0, 0, 0, 1.0f), "to", new Color(0, 0, 0, 0.0f), "time", 0.2f, "oncomplete", "FinishOpening"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
