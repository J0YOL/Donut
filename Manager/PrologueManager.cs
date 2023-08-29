using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    public Transform[] cartoons;
    public int page;

    public Button LeftBtn;
    public Button RightBtn;
    public Button InGameBtn;

    public GameObject StoryScene;

    void Start()
    {
        page = 0;
        LeftBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (page > 0)
        {
            LeftBtn.gameObject.SetActive(true);
        }
        if (page != 3)
        {
            RightBtn.gameObject.SetActive(true);
            InGameBtn.gameObject.SetActive(false);
        }
    }

    public void LeftBtnClick()
    {
        page--;
        cartoons[page].gameObject.SetActive(true);

        if (page == 0)
            LeftBtn.gameObject.SetActive(false);
        else
            LeftBtn.gameObject.SetActive(true);
    }

    public void RightBtnClick()
    {
        cartoons[page].gameObject.SetActive(false);
        page++;

        if (page == 3)
        {
            //GAME GO Btn Visible
            RightBtn.gameObject.SetActive(false);
            InGameBtn.gameObject.SetActive(true);
        }
    }

    public void InGameBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void GameStart2Click()
    {
        StoryScene.SetActive(false);
    }
}
