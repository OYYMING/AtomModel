using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class Drawing : MonoBehaviour {
    [Range(0,50)]
    public int segments = 50;
    [Range(0,5)]
    public float xradius = 0.5f;
    [Range(0,5)]
    public float yradius = 0.5f;
    LineRenderer line;
	
    void Start ()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.numPositions = segments + 1;
        line.useWorldSpace = false;
        CreatePoints ();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CreatePoints ();
    }

    void CreatePoints ()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i,new Vector3(x,0,z) );

            angle += (360f / segments);
        }
    }
}