using NUnit.Framework.Constraints;
using UnityEngine;

public class WriteInfinityLine : MonoBehaviour
{
    [SerializeField] float size = 0.1f;

    [SerializeField] int pointNum = 200;
    [SerializeField] float width = 3f;
    [SerializeField] float height = 2f;

    LineRenderer lineRenderer;

    // Update is called once per frame
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;

        lineRenderer.positionCount = pointNum;

        for (int i = 0; i < pointNum; i++)
        {
            float t = (float)i / (pointNum - 1) * 2 * Mathf.PI;

            float x = Mathf.Sin(t) * width;
            float y = Mathf.Sin(t * 2) * height;

            lineRenderer.SetPosition(i, new Vector3(x + transform.position.x, y + transform.position.y, 0));
        }
    }
}
