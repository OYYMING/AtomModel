using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumPuddingModel : BaseModel {

    Light environmentLight;

    public override void Init()
    {
        environmentLight = GameObject.FindObjectOfType<Light>();
    }

    public override void Show() {
        // Adjust the light
        base.Show();
        environmentLight.intensity = 0f;
    }
}