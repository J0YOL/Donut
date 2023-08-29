using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//아이템의 활성화와 비활성화
public class Item : MonoBehaviour
{
    public GameObject item;
    public GameObject item2;

    void Start()
    {
        
    }

    void Update()
    {
        //3분,6분에 아이템을 생성(1R->3분/2R->3분, 6분)
        if (GameManager.instance.score == 2100 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            item.SetActive(true);
            Invoke("SetFalse", 1.28f);
        }

        if (GameManager.instance.score == 3800 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            item2.SetActive(true);
            Invoke("SetFalse2", 1.28f);

        }
    }

    void SetFalse()
    {
        item.SetActive(false);
    }

    void SetFalse2()
    {
        item2.SetActive(false);
    }
}
