using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using extOSC; // extOSCライブラリを使用

public class OSCReceiverExample : MonoBehaviour
{
    
    public class KeypointEventArgs : EventArgs
    {
        public string keypointName;
        public float xPos;
        public float yPos;
        public float score;
    }

    public event EventHandler<KeypointEventArgs> KeypointUpdated;

    [SerializeField] private int port = 10000; // OSCメッセージを受信するポート
    [SerializeField] private string address = "/keypoint"; // 受信したいOSCアドレス
    public GameObject[] docArray = new GameObject[2];

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

                var keypointEventArgs = new KeypointEventArgs
                {
                    keypointName = message.Values[i].StringValue,
                    xPos = message.Values[i + 1].IntValue,
                    yPos = 1080 - message.Values[i + 2].IntValue,
                    score = message.Values[i + 3].FloatValue
                };

                // イベントを発火させる
                KeypointUpdated?.Invoke(this, keypointEventArgs);
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