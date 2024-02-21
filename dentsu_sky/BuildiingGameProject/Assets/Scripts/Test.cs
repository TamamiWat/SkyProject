using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Transform target = this.transform;

        // ターゲットオブジェクトのワールド座標をスクリーン座標に変換
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.position);

        // スクリーン座標をログに出力
        Debug.Log("Screen Position: " + screenPoint);
        
    }
}
