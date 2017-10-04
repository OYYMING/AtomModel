using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentumConservationModel : BaseModel {

    public Slider playerWeightSlider;
    public Slider playerSpeedSlider;
    public Slider targetWeightSlider;
    public InputField playerWeightInput;
    public InputField playerSpeedInput;
    public InputField targetWeightInput;


    public float minWeight = 0.5f;
    public float maxWeight = 5f;
    public float minSpeed = 0.5f;
    public float maxSpeed = 100f;

    public GameObject player;
    public GameObject target;

    float playerWeight;
    float playerSpeed;
    float targetWeight;

    Rigidbody playerRigidbody;
    Rigidbody targetRigidbody;

    Light environmentLight;

    #region original data

    Vector3 playerOriginPosition;
    Vector3 targetOriginPosition;
    float playerOriginWeight;
    float targetOriginWeight;

    #endregion

    public override void Init()
    {
        playerOriginPosition = player.transform.position;
        targetOriginPosition = target.transform.position;
        playerRigidbody = player.GetComponent<Rigidbody>();
        targetRigidbody = target.GetComponent<Rigidbody>();
        playerOriginWeight = playerRigidbody.mass;
        targetOriginWeight = targetRigidbody.mass;

        playerWeightSlider.onValueChanged.AddListener(v => {
            playerWeight = minWeight + v * (maxWeight - minWeight);
            playerWeightInput.text = playerWeight + "";
            SetWeight(player, playerWeight);
        });
        
        playerSpeedSlider.onValueChanged.AddListener(v => {
            playerSpeed = minSpeed + v * (maxSpeed - minSpeed);
            playerSpeedInput.text = playerSpeed + "";
        });

        targetWeightSlider.onValueChanged.AddListener(v => {
            targetWeight = minWeight + v * (maxWeight - minWeight);
            targetWeightInput.text = targetWeight + "";
            SetWeight(target, targetWeight);
        });

        playerWeightInput.onEndEdit.AddListener(v =>
        {
            double val = Convert.ToDouble(v);
            float clampVal = Mathf.Clamp((float)val, minWeight, maxWeight);
            SetPlayerWeight(clampVal);
        });

        playerSpeedInput.onEndEdit.AddListener(v =>
        {
            double val = Convert.ToDouble(v);
            float clampVal = Mathf.Clamp((float)val, minSpeed, maxSpeed);
            SetPlayerSpeed(clampVal);
        });

        targetWeightInput.onEndEdit.AddListener(v =>
        {
            double val = Convert.ToDouble(v);
            float clampVal = Mathf.Clamp((float)val, minWeight, maxWeight);
            SetTargetWeight(clampVal);
        });

        SetPlayerWeight(1);
        SetTargetWeight(2.75f);
        playerSpeedSlider.value = 0.5f;

        environmentLight = GameObject.FindObjectOfType<Light>();
    }

    public override void Show() {
        // Adjust the light
        base.Show();
        environmentLight.intensity = 0.66f;
    }

    public override void Hide() {
        SetPlayerWeight(1);
        SetTargetWeight(2.75f);
        playerSpeedSlider.value = 0.5f;
        playerRigidbody.velocity = Vector3.zero;
        targetRigidbody.velocity = Vector3.zero;
        ResetPosition();

        base.Hide();
    }

    void SetPlayerWeight (float weight) {
        playerWeightSlider.value = (weight - minWeight) / (maxWeight - minWeight);
    }

    void SetPlayerSpeed (float speed) {
        playerSpeedSlider.value = (speed - minSpeed) / (maxSpeed - minSpeed);
    }

    void SetTargetWeight (float weight) {
        targetWeightSlider.value = (weight - minWeight) / (maxWeight - minWeight);
    }
    
    void SetWeight (GameObject targetObj, float weight) {
        targetObj.transform.localScale = Vector3.one * weight;
        Rigidbody rigidbody = targetObj.GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.mass = weight;
        }
    }

    public void Shoot () {
        Vector3 direction = target.transform.position - player.transform.position;
        Vector3 speed = playerSpeed * direction.normalized;

        player.GetComponent<MomentumBall>().Shoot(target.transform, speed);
    }

    public void ResetPosition () {
        playerRigidbody.velocity = Vector3.zero;
        targetRigidbody.velocity = Vector3.zero;

        player.transform.position = playerOriginPosition;
        target.transform.position = targetOriginPosition;

        CameraManager.instance.ResetCamera();
    }

    void Update () {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastInfo;
            if (Physics.Raycast(ray, out raycastInfo, 1000f)) {
                if (raycastInfo.collider.gameObject == player) {
                    Shoot();
                }
            }
        }
    }
}