using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] Transform[] background = null;
    [SerializeField] float speed = 1f;

    float leftPosX = 0f;
    float rightPosX = 0f;

    void Start()
    {
        float length = background[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        leftPosX = -length;
        rightPosX = length * background.Length;
    }

    void Update()
    {
        for(int i = 0; i < background.Length; i++)
        {
            background[i].position += new Vector3(speed, 0, 0) * Time.deltaTime;

            if (background[i].position.x < leftPosX)
            {
                Vector3 selfPos = background[i].position;
                selfPos.Set(selfPos.x + rightPosX, selfPos.y, selfPos.z);
                background[i].position = selfPos;
            }
        }
    }
}
