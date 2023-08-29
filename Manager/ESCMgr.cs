using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESCMgr : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject ESC_Button;
    public GameObject escMenu;
    public Button continueButton;
    public Button restartButton;

    void Start()
    {
        escMenu.SetActive(false);

        gameManager = GameManager.instance;
        gameManager.OnGameEsc += () =>
        {            
            ESC_Button.SetActive(false);
            escMenu.SetActive(true);

        };

        continueButton.onClick.AddListener(() =>
        {
            gameManager.ResumeGame();
            escMenu.SetActive(false);
            ESC_Button.SetActive(true);
        });

        restartButton.onClick.AddListener(() =>
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                gameManager.RestartGame();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                gameManager.Restart2RGame();
            }
            escMenu.SetActive(false);
            ESC_Button.SetActive(true);
        });
    }
}
