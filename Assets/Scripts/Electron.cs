using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour {

    public GameObject model;
    public LineRenderer orbit;
    private Vector3 center;
    private Vector3 normal = Vector3.zero;
    private float speed = 5f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (normal == Vector3.zero)
            return;

        transform.RotateAround(center, normal, speed * Time.deltaTime);
    }

    public void SetRotation (Vector3 center, Vector3 normal, float speed) {
        this.center = center;
        this.normal = normal;
        this.speed = speed;

        DrawOrbit();
    }

    void DrawOrbit () {
        float radius = Vector3.Distance(transform.position, center);
        orbit.useWorldSpace = true;

    }

     void CreatePoints (int segments, float radius)
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        // for (int i = 0; i < (segments + 1); i++)
        // {
        //     x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
        //     z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

        //     line.SetPosition (i,new Vector3(x,0,z) );

        //     angle += (360f / segments);
        // }
    }
}
