using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoveInfinityMark : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float width = 3.0f;
    [SerializeField] private float height = 2.0f;

    //�X�s�[�h�̃v���p�e�B
    public float Speed
    {
        set { speed = value; }
        get { return  speed; }
    }

    //�����ʒu
    Vector3 pos;

    //������`��sin�̒l
    float sin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�����ʒu�̓o�^
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 8�̎��̋O��
        sin += Time.deltaTime * speed;
        float x = Mathf.Sin(sin) * width;
        float y = Mathf.Sin(sin * 2) * height;

        transform.localPosition = new Vector3(x + pos.x, y + pos.y, 0);
    }
}
