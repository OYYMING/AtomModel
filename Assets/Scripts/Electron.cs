using System;
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

    public void SetElectronLocation (Vector3 worldPos) {
        model.transform.position = worldPos;
    }

    public void SetRotation (Vector3 center, Vector3 normal, float speed) {
        this.center = center;
        this.normal = normal;
        this.speed = speed;

        DrawOrbit(center, model.transform.position, normal);
    }

    void DrawOrbit (Vector3 center, Vector3 rotatingObj, Vector3 normal) {
        float radius = Vector3.Distance(rotatingObj, center);
        orbit.useWorldSpace = true;
        orbit.numPositions = 51;

        CreatePoints(50, center, normal, radius);
    }

     void CreatePoints (int segments, Vector3 center, Vector3 normal, float radius)
    {
        double x;
        double y;
        double z;

        double xn = normal.x;
        double yn = normal.y;
        double zn = normal.z;
        double xn2 = normal.x * normal.x;
        double yn2 = normal.y * normal.y;
        double zn2 = normal.z * normal.z;
        double t = Math.Sqrt(xn2 + yn2);
        double t2 = xn2 + yn2;
        double sqrtR = radius;
        double paramC = Math.Sqrt(yn2 + zn2 - xn2 * zn2 / t2);

        double xa = 1 / t * normal.y * sqrtR;
        double xa2 = - xn*zn * yn * sqrtR /(t2 * paramC);
        double xc = center.x;
        double ya1 = -xn * sqrtR / t;
        double ya2 = (xn2/t2 - 1) * zn*sqrtR / paramC;
        double yc = center.y;
        double za = yn * sqrtR / paramC;
        double zc = center.z;

        // 看看line的frame

        float angle = 0f;
        float per = 360f / segments;
        for (int i = 0; i < (segments + 1); i++)
        {
            angle = per * i;
            double sin = Math.Sin(Mathf.Deg2Rad * angle);
            double cos = Math.Cos(Mathf.Deg2Rad * angle);

            x = xa * sin + xa2 * cos + xc;
            y = ya1 * sin + ya2 * cos + yc;
            z = za * cos + zc;

            orbit.SetPosition(i, new Vector3((float)x, (float)y, (float)z));
        }
    }
}
