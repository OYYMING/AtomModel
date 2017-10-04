using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour {

    private Vector3 lastMousePosition;
    private float fingerDistance0;
    #if UNITY_ANDROID || UNITY_IPHONE
    public float flexity = 50f;
    #elif UNITY_EDITOR
    public float flexity = 5f;
    #endif

    private Camera sceneCamera;
    private Vector3 cameraOriginPosition;
    private Quaternion cameraOriginRotation;
    private bool validDrag = false;

    private static CameraManager instance_;
    public static CameraManager instance {
		get {
			if (instance_ == null) {
                instance_ = GameObject.FindObjectOfType<CameraManager>();
				if (instance_ == null) {
                    Debug.LogError("Can not find CameraManager");
                    return null;
                }
            }

			return instance_;
		}	
	}

    // Use this for initialization
    void Start () {
        sceneCamera = Camera.main;
        cameraOriginPosition = sceneCamera.transform.position;
        cameraOriginRotation = sceneCamera.transform.rotation;
        instance_ = this;
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        MobileInput();
#else
        DesktopInput();
#endif
    }


    public void ResetCamera () {
        sceneCamera.transform.position = cameraOriginPosition;
        sceneCamera.transform.rotation = cameraOriginRotation;
    }

    void DesktopInput () {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            if (EventSystem.current.IsPointerOverGameObject())
                validDrag = false;
            else
                validDrag = true;
        } else if (validDrag && Input.GetMouseButton(0)) {
            float deltaH = Input.mousePosition.x - lastMousePosition.x;
            float deltaV = Input.mousePosition.y - lastMousePosition.y;
            sceneCamera.transform.RotateAround(Vector3.zero, -sceneCamera.transform.right, Time.deltaTime * flexity * deltaV);
            sceneCamera.transform.RotateAround(Vector3.zero, sceneCamera.transform.up, Time.deltaTime * flexity * deltaH);

            lastMousePosition = Input.mousePosition;
        }
    }

    void MobileInput () {
        if (Input.touchCount > 0) {
            if (Input.touchCount == 1) {
                Touch touch0 = Input.GetTouch(0);
                if (touch0.phase == TouchPhase.Began) {
                    if (EventSystem.current.IsPointerOverGameObject(touch0.fingerId))
                        validDrag = false;
                    else
                        validDrag = true;
                } else if (validDrag && touch0.phase == TouchPhase.Moved) {
                    sceneCamera.transform.RotateAround(Vector3.zero, -sceneCamera.transform.right, Time.deltaTime * 50f * touch0.deltaPosition.y);
                    sceneCamera.transform.RotateAround(Vector3.zero, sceneCamera.transform.up, Time.deltaTime * 50f * touch0.deltaPosition.x);
                }
            } else if (Input.touchCount == 2) {     // Scale
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began) {
                    fingerDistance0 = Mathf.Abs(Vector2.Distance(touch0.position, touch1.position));
                } else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    float distance = Mathf.Abs(Vector2.Distance(touch0.position, touch1.position));
                    float delta = distance - fingerDistance0;
                    fingerDistance0 = distance;

                    sceneCamera.transform.Translate(Vector3.forward * Time.deltaTime * 5 * delta);
                }
            }
        }
    }
}
