using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public Button ExitButton;
    public GameObject OptionScene;

    private void Start()
    {
        ExitButton.onClick.AddListener(() =>
        {
            OptionScene.SetActive(false);
        });
    }
}
