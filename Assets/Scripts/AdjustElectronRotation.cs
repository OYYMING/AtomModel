using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustElectronRotation : MonoBehaviour {

    private Canvas[] mCanvas;
    // Use this for initialization
    void Start () {
        mCanvas = this.GetComponentsInChildren<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {
        RotateElectron();
    }

	void RotateElectron () {
		for (int i = 0; i < mCanvas.Length; i++)
		{
            mCanvas[i].transform.LookAt(Camera.main.transform);
        }
	}
}
