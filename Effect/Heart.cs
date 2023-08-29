using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    Vector2 direction;
    public float moveSpeed = 0.1f;
    public float minSize = 0.1f;
    public float maxSize = 0.3f;
    public float sizeSpeed = 1;

    //Sprite투명, 색변화
    SpriteRenderer sprite;
    public Color[] colors;
    public float colorSpeed = 5;

    void Start()
    {
        direction = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);

        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * colorSpeed);
        sprite.color = color;

        if (sprite.color.a <= 0.01f)
            Destroy(gameObject);
    }
}
