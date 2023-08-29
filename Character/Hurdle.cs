using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    public float HurdleSpeed = 0;

    //우측 활성화, 좌측 비활성화
    public Vector2 StartPosition;

    void Start()
    {
        transform.position = StartPosition;
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 4f);

        if (transform.position.x < -6)
        {
            gameObject.SetActive(false);
        }
    }
}
