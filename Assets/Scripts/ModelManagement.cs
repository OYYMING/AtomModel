using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManagement : MonoBehaviour {

    public List<BaseModel> models;
    public GameObject menuPanel;
    public Vector3 menuDesiredPosition;
    public ColorBlock normalColorBlock;
    public ColorBlock focusedColorBlock;

    BaseModel currentModel_;
    Button currentFocus_;

    private static ModelManagement instance_;
    public static ModelManagement instance {
		get {
			if (instance_ == null) {
                instance_ = GameObject.FindObjectOfType<ModelManagement>();
				if (instance_ == null) {
                    Debug.LogError("Can not find ModelManagement");
                    return null;
                }
            }

			return instance_;
		}	
	}

	public void SwitchModel (BaseModel model) {
		if (currentModel_ == null) {
            AnimateMenu(() => {
                currentModel_ = model;
                currentModel_.Show();        
            });
        } else {
            currentModel_.Hide();

            CameraManager.instance.ResetCamera();
            currentModel_ = model;
            currentModel_.Show();
        }
    }

    public void FocusOn (Button button) {
        if (button != null) {
            FocusOnInternal(button);
        }
    }

    void FocusOnInternal (Button button) {
        if (currentFocus_ != null)
            currentFocus_.colors = normalColorBlock;

        currentFocus_ = button;
        button.colors = focusedColorBlock;
    } 
    
    void AnimateMenu (Action callback) {
        StartCoroutine(AnimateMenuCo(callback));
    }

    IEnumerator AnimateMenuCo(Action callback) {
        float speed = 5f;
        RectTransform rectTransform = menuPanel.GetComponent<RectTransform>();
        menuDesiredPosition = new Vector3(-Screen.width / 2 + 50, Screen.height / 2 - 50, 0);

        Debug.Log("rectTransform.position : " + rectTransform.anchoredPosition3D + ", position : " + menuPanel.transform.position);
        Vector3 direction = (menuDesiredPosition - rectTransform.anchoredPosition3D).normalized;
        while (Vector3.Distance (menuDesiredPosition, rectTransform.anchoredPosition3D) > 30) {
            rectTransform.anchoredPosition3D = Vector3.Slerp (rectTransform.anchoredPosition3D, menuDesiredPosition, Time.deltaTime * 2);
            yield return null;
        }

        if (callback != null)
            callback();
    }

}
