using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public SpriteRenderer[] tiles;
    public Sprite[] groundImg;
    public float speed;

    SpriteRenderer temp;
    void Start()
    {
        temp = tiles[0];
    }

    void Update()
    {
        //바닥이 끝이나면 옆에 생성시킴
        for (int i = 0; i < tiles.Length; i++)
        {
            if (-5 >= tiles[i].transform.position.x)
            {
                for (int q = 0; q < tiles.Length; q++)
                {
                    if (temp.transform.position.x < tiles[q].transform.position.x)
                        temp = tiles[q];
                }
                tiles[i].transform.position = new Vector2(temp.transform.position.x + 1, -2.43f);
                tiles[i].sprite = groundImg[Random.Range(0, groundImg.Length)];
            }
        }
        //옆으로 Ground가 전체적으로 이동
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * 4f);
        }
    }
}
