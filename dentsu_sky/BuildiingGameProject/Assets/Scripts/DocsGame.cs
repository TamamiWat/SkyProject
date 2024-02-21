using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTracker : MonoBehaviour
{

    [SerializeField] Image[] images;
    private int currentIndex = 0;

    public OSCReceiverExample oscReceiver;
    public Vector3 currentRightPos = Vector3.zero;
    public Vector3 currentLeftPos = Vector3.zero;
    public Vector3 lastRightPos = Vector3.zero;
    public Vector3 lastLeftPos = Vector3.zero;
    public bool isRightOK = false;
    public bool isLeftOK = false;

    public bool nearbyRight = false;
    public bool nearbyLeft = false;
    private int rightCNT = 0;
    private int leftCNT = 0;

    public Generater generater;

    [SerializeField] float handThreshold = 10f;
    [SerializeField] float paperThreshold = 10f;
    void Start()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
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
        isRightOK = false;
        isLeftOK = false;
        if (e.keypointName == "wrist(L)")
        {
            lastLeftPos = currentLeftPos;
            currentLeftPos = new Vector3(e.xPos, e.yPos, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentLeftPos);
            worldPosition.z = 10f; // z値は適宜調整

            if(generater.paperNum == 0)
            {
                Debug.Log("nearby Left hand");
                nearbyLeft = RangeChecker(worldPosition, generater.paperPos);

                if(nearbyLeft)
                {
                    float userDelta = currentLeftPos.x - lastLeftPos.x;

                    if(userDelta < -handThreshold)
                    {
                        isLeftOK = true;
                        leftCNT++;
                        if(leftCNT >= 3)
                        {
                            images[1].gameObject.SetActive(true);

                        }
                    }
                }

            }      

            // ここでGameObjectをInstantiateする
            //Instantiate(oscReceiver.docArray[0], worldPosition, Quaternion.identity);
        }
        if (e.keypointName == "wrist(R)")
        {
            lastRightPos = currentRightPos;
            currentRightPos = new Vector3(e.xPos, e.yPos, 0);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentRightPos);
            worldPosition.z = 10f; // z値は適宜調整

            if(generater.paperNum == 1)
            {
                Debug.Log("nearby Right hand");
                nearbyRight = RangeChecker(worldPosition, generater.paperPos);

                if(nearbyRight)
                {
                    float userDelta = currentRightPos.x - lastRightPos.x;

                    if(userDelta > handThreshold)
                    {
                        isRightOK = true;
                        rightCNT++;
                        if(rightCNT >= 3)
                        {
                            images[0].gameObject.SetActive(true);

                        }
                    }
                }

            }

            // ここでGameObjectをInstantiateする
            //Instantiate(oscReceiver.docArray[1], worldPosition, Quaternion.identity);
        }
        
    }

    private bool RangeChecker(Vector3 hand, Vector3 paper)
    {
        float distance = Vector3.Distance(hand, paper);
        if(distance < paperThreshold)
        {
            
            return true;
        }
        return false;
    }
}
