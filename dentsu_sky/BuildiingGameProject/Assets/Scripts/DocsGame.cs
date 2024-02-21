using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTracker : MonoBehaviour
{

    [SerializeField] Image[] images;
    [SerializeField, Range(0f, 5f)] private float interval = 2.0f;
    private int currentIndex = 0;
    private float timer = 0f;

    public OSCReceiverExample oscReceiver;
    private Vector3 currentRightPos = Vector3.zero;
    private Vector3 currentLeftPos = Vector3.zero;
    private Vector3 lastRightPos = Vector3.zero;
    private Vector3 lastLeftPos = Vector3.zero;

    void Start()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }

        if(images.Length > 0)
        {
            images[currentIndex].gameObject.SetActive(true);
        }
        
    }

    

    void OnEnable()
    {
        if (oscReceiver != null)
        {
            oscReceiver.KeypointUpdated += HandleKeypointUpdated;
        }
    }

    void OnDisable()
    {
        if (oscReceiver != null)
        {
            oscReceiver.KeypointUpdated -= HandleKeypointUpdated;
        }
    }

    // Update is called once per frame
    private void HandleKeypointUpdated(object sender, OSCReceiverExample.KeypointEventArgs e)
    {
        if (e.keypointName == "wrist(L)")
        {
            lastLeftPos = currentLeftPos;
            currentLeftPos = new Vector3(e.xPos, e.yPos, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentLeftPos);
            worldPosition.z = 10f; // z値は適宜調整

            // ここでGameObjectをInstantiateする
            Instantiate(oscReceiver.docArray[0], worldPosition, Quaternion.identity);
        }
        if (e.keypointName == "wrist(R)")
        {
            lastRightPos = currentRightPos;
            currentRightPos = new Vector3(e.xPos, e.yPos, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentRightPos);
            worldPosition.z = 10f; // z値は適宜調整

            // ここでGameObjectをInstantiateする
            Instantiate(oscReceiver.docArray[1], worldPosition, Quaternion.identity);
        }
        
    }

    
}
