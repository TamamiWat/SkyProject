using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    public OSCReceiverExample oscReceiver;
    private Vector3 currentRightPos = Vector3.zero;
    private Vector3 currentLeftPos = Vector3.zero;
    private Vector3 lastRightPos = Vector3.zero;
    private Vector3 lastLeftPos = Vector3.zero;

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
