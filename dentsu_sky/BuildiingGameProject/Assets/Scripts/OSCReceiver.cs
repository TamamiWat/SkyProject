using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC; // extOSCライブラリを使用

public class OSCReceiverExample : MonoBehaviour
{
    [SerializeField] private int port = 10000; // OSCメッセージを受信するポート
    [SerializeField] private string address = "/keypoint"; // 受信したいOSCアドレス

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
        Debug.Log($"Received OSC message: {message}");
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