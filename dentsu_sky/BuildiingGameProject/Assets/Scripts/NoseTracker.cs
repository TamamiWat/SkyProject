using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseTracker : MonoBehaviour
{
    public OSCReceiverExample oscReceiver;

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
        if (e.keypointName == "nose")
        {
            Vector3 locTrans = new Vector3(e.xPos, e.yPos, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(locTrans);
            worldPosition.z = 10f; // z値は適宜調整

            // ここでGameObjectをInstantiateする
            Instantiate(oscReceiver.docArray[0], worldPosition, Quaternion.identity);
        }
    }
}
