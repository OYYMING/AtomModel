using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlanetaryManagement : MonoBehaviour {

    public GameObject sphere;
    public GameObject electronContainer;
    public GameObject electronTemp;
    public int electronCount = 10;

	public float speed = 30f;

    private List<GameObject> electronList = new List<GameObject>();
    // Use this for initialization
    void Start () {
        // SpawnElectrons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnElectrons () {
		for (int i = 0; i < electronCount; i++)
		{
            SpawnElectron();
        }
	}

	void SpawnElectron () {
        GameObject elecObj = GameObject.Instantiate(electronTemp);
        elecObj.transform.parent = electronContainer.transform;
        elecObj.transform.localPosition = Random.insideUnitSphere * 5f;
        Electron electron = elecObj.GetComponent<Electron>();

        Vector3 normal = RandomNormal(elecObj.transform);
        electron.SetRotation(transform.position, normal, speed);
    }

	Vector3 RandomNormal (Transform electron) {
		Vector2 randomV2 = Random.insideUnitCircle;
        Vector3 delta = transform.position - electron.position;
        float z = (delta.x * randomV2.x + delta.y * randomV2.y) / -delta.z;
        Debug.Log("z:" + z);
        return new Vector3(randomV2.x, randomV2.y, z).normalized;
	}
}
