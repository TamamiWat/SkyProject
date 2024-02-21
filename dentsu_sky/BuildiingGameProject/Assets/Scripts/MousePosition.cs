using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    public float threshold; // 閾値
    private float lastMousePosition;
    [SerializeField] private float objectMoveSpeed = 0.1f; // �I�u�W�F�N�g�̈ړ����x
    [SerializeField] private float transparencyDecreaseRate = 0.01f; // �����x�������鑬�x
    private float alpha;
    public Generater generater;
    private GameObject byeObj;

    void Start()
    {
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }

    void Update()
    {
        GameObject nowobj = generater.cloneObjects;
        if (nowobj == null) return;

        string nowname = nowobj.name;//���ނ̖��O�擾

        Vector3 mousePosition = Input.mousePosition;// �J�[�\���ʒu���擾
        mousePosition.z = 10;// �J�[�\���ʒu��z���W��10��
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);// �J�[�\���ʒu�����[���h���W�ɕϊ�

        float mouseDelta = target.x - lastMousePosition;//�J�[�\�����x�̌v�Z
        Debug.Log(mouseDelta);
        lastMousePosition = Input.mousePosition.x; // �}�E�X�ʒu���X�V

        if (nowname == "paper_red(Clone)")
        {
            if (mouseDelta > threshold) 
            {
                GameObject byeObj = nowobj; 
                byeObj.transform.position += new Vector3(objectMoveSpeed, 0, 0);
            }
        }
        else
        {
            if (mouseDelta < -threshold) 
            {
                GameObject byeObj = nowobj;
                byeObj.transform.position += new Vector3(objectMoveSpeed, 0, 0);
            }
        }

        if (byeObj == null) return;
        // �����x��������
        alpha = byeObj.GetComponent<SpriteRenderer>().color.a;
        alpha -= transparencyDecreaseRate;

        // �����x��0�ȉ��ɂȂ�����I�u�W�F�N�g���폜
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}

