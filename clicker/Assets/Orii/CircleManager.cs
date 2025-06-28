using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] Vector3 position;
    [SerializeField] GameObject prefab;
    [SerializeField]List<GameObject> objects = new List<GameObject>();

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
    }

    void AddCircle()
    {
        GameObject obj;
        obj = Instantiate(prefab, position, Quaternion.identity);
        objects.Add(obj);
    }

    void RemoveCircle()
    {
        if (objects.Count > 1)
        {
            GameObject obj = objects[objects.Count - 1];
            objects.Remove(obj);
            Destroy(obj);
        }
    }
}
