using UnityEngine;

public class DestoryAfterImage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spriteRenderer.color = ChangeAlpha(spriteRenderer.color);
        transform.localScale = new Vector3(
            transform.localScale.x - 0.005f,
            transform.localScale.y - 0.005f,
            transform.localScale.z);


        if (spriteRenderer.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    Color SetAlpha(Color color)
    {
        Color changeColor = color;
        changeColor.a = 180.0f;
        return changeColor;
    }

    Color ChangeAlpha(Color color)
    {
        Color changeColor = color;
        changeColor.a -= 0.02f;
        return changeColor;
    }
}
