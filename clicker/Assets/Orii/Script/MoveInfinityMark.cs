using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoveInfinityMark : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float width = 3.0f;
    [SerializeField] private float height = 2.0f;

    //スピードのプロパティ
    public float Speed
    {
        set { speed = value; }
        get { return  speed; }
    }

    //初期位置
    Vector3 pos;

    //無限を描くsinの値
    float sin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期位置の登録
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 8の字の軌道
        sin += Time.deltaTime * speed;
        float x = Mathf.Sin(sin) * width;
        float y = Mathf.Sin(sin * 2) * height;

        transform.localPosition = new Vector3(x + pos.x, y + pos.y, 0);
    }
}
