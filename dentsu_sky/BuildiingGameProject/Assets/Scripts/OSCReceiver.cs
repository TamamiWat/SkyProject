using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC; // extOSCライブラリを使用

public class OSCReceiverExample : MonoBehaviour
{
    [SerializeField] private int port = 10000; // OSCメッセージを受信するポート
    [SerializeField] private string address = "/keypoint"; // 受信したいOSCアドレス
    [SerializeField] private GameObject[] docArray = new GameObject[2];

    private OSCReceiver _receiver; // 正しいレシーバーコンポーネントの型を使用

    void Start()
    {
        // OSCレシーバーコンポーネントをGameObjectに追加
        _receiver = gameObject.AddComponent<OSCReceiver>(); // 正しい型に変更
        _receiver.LocalPort = port; // LocalPortを設定
        _receiver.Bind(address, OnMessageReceived); // メッセージ受信時のコールバックをバインド
    }

    private void OnMessageReceived(OSCMessage message)
    {
        // 受信したOSCメッセージを処理
        // Debug.Log($"Received OSC message: {message}");
        if (message.Address == address)
        {
            // OSCメッセージからデータを取得
            var data = message.Values;

            // データを整形して使用
            for (int i = 0; i < message.Values.Count; i += 4) // 4つの要素ごとに分ける（名前、X、Y、スコア）
            {
                var keypointName = message.Values[i].StringValue;
                float x = message.Values[i + 1].IntValue;
                float y = 1080 - message.Values[i + 2].IntValue;
                var score = message.Values[i + 3].FloatValue;

                // ここでデータを使用（例えば、キーポイントを表示）
                Debug.Log($"Keypoint: {keypointName}, X: {x}, Y: {y}, Score: {score}");
                if(keypointName == "wrist(L)"){
                    //Transform target = docArray[1].transform;
                    Vector3 locTrans = Vector3.zero;
                    locTrans.x = x;
                    locTrans.y = y;
                    Vector3 worTrans = Vector3.zero;
                    worTrans = Camera.main.ScreenToWorldPoint(locTrans);
                    worTrans.z = 10f;

                    Instantiate(docArray[0], worTrans, Quaternion.identity);
                }
                // if(keypointName == "wrist(R)"){

                // }
            }
        }
    }

    void OnDestroy()
    {
        // オブジェクトが破棄された時にレシーバーを閉じる
        if (_receiver != null)
        {
            _receiver.Close();
        }
    }
}