using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{
    public GameObject cloneObjects;
    [SerializeField] GameObject[] papers;
    [SerializeField] private float interval = 10f;
    public Vector3 paperPos = Vector3.zero; // これは目的地の位置
    public int paperNum;
    private bool isMoving = false;
    private float minMoveSpeed = 2f; // 移動速度の最小値
    private float maxMoveSpeed = 10f; // 移動速度の最大値
    private float moveSpeed; // 実際の移動速度

    void Start()
    {
        InvokeRepeating("SpawnRandomObject", 0f, interval);
    }

    void Update()
    {
        if (isMoving && cloneObjects != null)
        {
            float step = moveSpeed * Time.deltaTime;
            cloneObjects.transform.position = Vector3.MoveTowards(cloneObjects.transform.position, paperPos, step);

            if (Vector3.Distance(cloneObjects.transform.position, paperPos) < 0.001f)
            {
                isMoving = false; // 移動完了
            }
        }
    }

    void SpawnRandomObject()
    {
        // 以前に生成されたオブジェクトがあれば破棄
        if (cloneObjects != null)
        {
            Destroy(cloneObjects);
        }

        // 移動速度をランダムに設定
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        paperNum = Random.Range(0, papers.Length);
        Vector3 startPos = new Vector3(paperPos.x, paperPos.y - 10f, paperPos.z); // 下から湧いてくるアニメーションの開始位置

        // オブジェクトを開始位置にインスタンス化
        cloneObjects = Instantiate(papers[paperNum], startPos, Quaternion.identity);

        isMoving = true; // 移動開始フラグをtrueに
    }
}
