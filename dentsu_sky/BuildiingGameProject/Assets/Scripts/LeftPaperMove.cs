using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaperMove : MonoBehaviour
{
    [SerializeField]private float speed = 0.1f;
    bool isOK = false;

    GameObject targetObject;

    void Start()
    {
        speed += Random.Range(-0.05f, 0.5f);
        targetObject = GameObject.Find("HandPosManager");
    }


    // Update is called once per frame
    void Update()
    {
        HandTracker docsgame = targetObject.GetComponent<HandTracker>();
        if(docsgame.isLeftOK)
        {
            isOK = true;
        }

        if(isOK){
            this.transform.position += new Vector3(speed, 0, 0);
        }
    }
}
