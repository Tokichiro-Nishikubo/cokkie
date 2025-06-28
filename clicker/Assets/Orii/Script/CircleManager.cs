using AIE2D;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] Vector3 position;
    [SerializeField] GameObject prefab;
    [SerializeField] List<Color> colorList = new List<Color>();
    List<GameObject> objects = new List<GameObject>(15);
    int num = 0;
    float speed = 1.0f;
    Color color = Color.white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddCircle();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RemoveCircle();
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            num++;
            ChangeColor(num);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            speed++;
            ChangeSpeed(speed);
        }
    }

    public void AddCircle()
    {
        //Circleの追加
        GameObject obj;
        obj = Instantiate(prefab, position, Quaternion.identity);

        //追加したオブジェクトの色の統一
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.color = color;
        //残像も同じように
        DynamicAfterImageEffect2DPlayer afterImage = obj.GetComponent<DynamicAfterImageEffect2DPlayer>();
        afterImage.SetColorIfneeded(new Color(color.r, color.g, color.b, 0.5f));
        //スピードも調節
        obj.gameObject.GetComponent<MoveInfinityMark>().Speed = speed;
        objects.Add(obj);
    }

    public void RemoveCircle()
    {
        if (objects.Count > 1)
        {
            GameObject obj = objects[objects.Count - 1];
            objects.Remove(obj);
            Destroy(obj);
        }
    }

    public void ChangeColor(int num)
    {
        switch (num  % 11)
        {
            case 0:
                color = colorList[0];
                break;
            case 1:
                color = colorList[1];
                break;
            case 2:
                color = colorList[2];
                break;
            case 3:
                color = colorList[3];
                break;
            case 4:
                color = colorList[4];
                break;
            case 5:
                color = colorList[5];
                break;
            case 6:
                color = colorList[6];
                break;
            case 7:
                color = colorList[7];
                break;
            case 8:
                color = colorList[8];
                break;
            case 9:
                color = colorList[9];
                break;
            case 10:
                color = colorList[10];
                break;
        }

        for (int i = 0; i < objects.Count; i++)
        {
            SpriteRenderer sr = objects[i].GetComponent<SpriteRenderer>();
            sr.color = color;

            DynamicAfterImageEffect2DPlayer afterImage = objects[i].GetComponent<DynamicAfterImageEffect2DPlayer>();
            afterImage.SetColorIfneeded(new Color(color.r, color.g, color.b, 0.5f));
        }
    }

    void ChangeSpeed(float speed)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].gameObject.GetComponent<MoveInfinityMark>().Speed = speed;
        }
    }
}
