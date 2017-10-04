using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryModel : BaseModel {

    public GameObject sphere;
    public GameObject electronContainer;
    public GameObject electronTemp;
    public int electronCount = 10;

    public float speed = 30f;

    private List<GameObject> electronList = new List<GameObject> ();

    private Vector3 firstNormal = Vector3.zero;
    private Vector3 firstNormalAxis = Vector3.zero;
    private Vector3 firstPosition;

    Light environmentLight;

    public override void Show () {
        // Adjust the light
        base.Show ();
        environmentLight.intensity = 0f;
    }

    public override void Init () {
        InitVectors ();
        SpawnElectrons ();
        environmentLight = GameObject.FindObjectOfType<Light> ();
    }

    void InitVectors () {
        Vector3 position = Random.insideUnitSphere.normalized;

        Vector2 randomV2 = Random.insideUnitCircle;
        Vector3 delta = transform.position - position;
        float z = (delta.x * randomV2.x + delta.y * randomV2.y) / -delta.z;

        firstNormal = new Vector3 (randomV2.x, randomV2.y, z).normalized;
        firstNormalAxis = delta;
        firstPosition = position;
    }

    void SpawnElectrons () {
        for (int i = 0; i < electronCount; i++) {
            SpawnElectron (i);
        }
    }

    void SpawnElectron (int index) {
        GameObject elecObj = GameObject.Instantiate (electronTemp);
        elecObj.transform.parent = electronContainer.transform;
        elecObj.transform.localPosition = Vector3.zero;
        Electron electron = elecObj.GetComponent<Electron> ();

        ElectronData data = CalculateElectronData (index);
        electron.SetElectronLocation (data.position);
        electron.SetRotation (sphere.transform.position, data.normal, speed);
        elecObj.SetActive (true);
    }

    ElectronData CalculateElectronData (int index) {
        ElectronData data = new ElectronData ();

        float angle = 180f / electronCount * index;
        Quaternion q = Quaternion.AngleAxis (angle, firstNormalAxis);

        data.normal = q * firstNormal;
        Vector3 pos = firstPosition;
        q = Quaternion.AngleAxis (Random.Range (0, 360f), data.normal);

        float distance = Vector3.Distance (pos, sphere.transform.position);
        float scale = sphere.transform.localScale.x / distance;
        pos = sphere.transform.position + (q * pos) * scale;
        data.position = pos;

        return data;
    }

    Vector3 RandomNormal (Vector3 position) {
        Vector3 normal = Vector3.zero;
        if (firstNormal == Vector3.zero) {
            Vector2 randomV2 = Random.insideUnitCircle;
            Vector3 delta = transform.position - position;
            float z = (delta.x * randomV2.x + delta.y * randomV2.y) / -delta.z;

            firstNormal = new Vector3 (randomV2.x, randomV2.y, z).normalized;
            firstNormalAxis = delta;
            firstPosition = position;

            normal = firstNormal;
        } else {
            float angle = 180f / electronCount;
            Quaternion q = Quaternion.AngleAxis (angle, firstNormal);
            normal = q * firstNormal;
        }

        return normal;
    }
}

public class ElectronData {
    public Vector3 position;
    public Vector3 normal;
}