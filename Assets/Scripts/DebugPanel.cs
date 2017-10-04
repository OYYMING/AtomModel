using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    public Text textPanel;
    public RectTransform content;

    private static DebugPanel instance_;
    public static DebugPanel instance {
		get {
			if (instance_ == null) {
                instance_ = GameObject.FindObjectOfType<DebugPanel>();
				if (instance_ == null) {
                    Debug.LogError("Can not find DebugPanel");
                    return null;
                }
            }

			return instance_;
		}	
	}

	public void Log (string text) {
        textPanel.text += text + "\n";
        AdjustHeight();
    }

	public void Clear () {
        textPanel.text = "";
        AdjustHeight();
    }

	void AdjustHeight () {
        content.sizeDelta = new Vector2(content.sizeDelta.x, textPanel.preferredHeight);
    }
}
