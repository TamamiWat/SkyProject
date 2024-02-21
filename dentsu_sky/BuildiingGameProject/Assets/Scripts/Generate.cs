using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{
    public GameObject cloneObjects;
    [SerializeField] GameObject[] papers;
    [SerializeField] private float interval = 10f;
    public Vector3 paperPos = Vector3.zero;
    public int paperNum;

    void Start()
    {
        InvokeRepeating("SpawnRandomObject", 0f, interval);
    }

    void SpawnRandomObject()
    {

        if (cloneObjects != null)
        {
            Destroy(cloneObjects);
        }
        else
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition.z = 10;

            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

            paperNum = Random.Range(0, papers.Length);

            cloneObjects = Instantiate(papers[paperNum], paperPos, Quaternion.identity);
        }

    }
}