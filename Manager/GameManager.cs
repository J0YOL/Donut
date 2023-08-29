using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameStateType
{
    Idle, Playing, Ready, Paused
}

//게임 스테이지를 위한 클래스
[System.Serializable]
public class Stage
{
    public GameObject[] mobs;
}

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        gameState = GameStateType.Playing;
    }
    #endregion
    public GameObject door;
    public GameObject MenuScene; 
    public GameObject ForJump;
    public GameObject OptionScene;

    public Player player;

    public Text scoreTxt;
    public float gameSpeed = 1;
    public Button escButton;

    public Button startButton;
    public Button optionButton;
    public Button ExitButton;

    private GameManager[] buttons;
    public RespawnMgr respawn;

    public event Action OnGameEsc;
    public event Action OnGameReady;
    public event Action OnGameResume;

    public AudioClip clip;

    public bool isPlay = false;

    //점수
    public int score = 0;

    public GameStateType gameState { get; private set; }

    public Stage[] stage;
    public int curStage;
    public int[] stageScore;

    public GameManager selectButton
    {
        get;
        private set;
    }

    //점수카운트하는 코루틴
    IEnumerator AddScore()
    {
        while (isPlay)
        {
            try
            {
                if (stageScore[curStage] <= score)
                    curStage++;
            }
            catch  { }
            
            score++;
            scoreTxt.text = score.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    //시작메뉴버튼의 기능화
    void Start()
    {
        Time.timeScale = 0;
        MenuScene.SetActive(true);
        ForJump.SetActive(false);

        //시작 메뉴버튼 설정
        startButton.onClick.AddListener(() =>
        {
            Sound.instance.SFXPlay("Click", clip);
            ForJump.SetActive(true);
            respawn.PlayGame();
            curStage = 0;
            MenuScene.SetActive(false);
            Time.timeScale = 1;
            isPlay = true;
            score = 0;
            scoreTxt.text = score.ToString();
            StartCoroutine(AddScore());
        });
        //옵션 메뉴버튼 설정
        optionButton.onClick.AddListener(() =>
        {
            //Sound.instance.SFXPlay("Click", clip);
            OptionScene.SetActive(true);
        });
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ExitButton.onClick.AddListener(() =>
            {
                Sound.instance.SFXPlay("Click", clip);
                Application.Quit();
            });
        }
        
    }

    //중단버튼을 눌렀을 때
    private void Update()
    {
        //각 스테이지의 점수를 도달할 시의 이벤트
        if (score == 3500 && SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
        if (score == 6400 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            door.SetActive(true);          
        }
        

        switch (gameState)
        {
            case GameStateType.Playing:
                escButton.onClick.AddListener(() =>
                {
                    //Sound.instance.SFXPlay("Click", clip);
                    ForJump.SetActive(false);
                    Time.timeScale = 0;
                    OnGameEsc?.Invoke();
                    gameState = GameStateType.Paused;
                    isPlay = false;                  
                });
                break;
            case GameStateType.Paused:
                break;
        }
    }

    //중단 버튼의 재시작
    public void RestartGame()
    {
        Time.timeScale = 1;

        Sound.instance.SFXPlay("Click", clip);

        SceneManager.LoadScene(1);

        score = 0;
        scoreTxt.text = score.ToString();
        isPlay = true;
        StartCoroutine(AddScore());
    }

    public void Restart2RGame()
    {
        Time.timeScale = 1;

        Sound.instance.SFXPlay("Click", clip);

        SceneManager.LoadScene(2);

        score = 0;
        scoreTxt.text = score.ToString();
        isPlay = true;
        StartCoroutine(AddScore());
    }

    //중단버튼의 계속
    public void ResumeGame()
    {
        gameState = GameStateType.Ready;

        Sound.instance.SFXPlay("Click", clip);

        OnGameReady?.Invoke();
        StartCoroutine(ResumeReadyRoutine());
    }

    private IEnumerator ResumeReadyRoutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        ForJump.SetActive(true);
        gameState = GameStateType.Playing;

        scoreTxt.text = score.ToString();
        isPlay = true;
        StartCoroutine(AddScore());

        Time.timeScale = 1;
        OnGameResume?.Invoke();
    }

    //죽고 난 후의 재시작
    public void Retry()
    {
        gameState = GameStateType.Playing;

        Sound.instance.SFXPlay("Click", clip);

        Time.timeScale = 1;
        MenuScene.SetActive(false);
        SceneManager.LoadScene(1);
        
        score = 0;
        scoreTxt.text = score.ToString();
        //isPlay = true;
        StartCoroutine(AddScore());
    }

    public void Retry2R()
    {
        gameState = GameStateType.Playing;

        Sound.instance.SFXPlay("Click", clip);

        Time.timeScale = 1;
        MenuScene.SetActive(false);
        SceneManager.LoadScene(2);

        score = 0;
        scoreTxt.text = score.ToString();
        //isPlay = true;
        StartCoroutine(AddScore());
    }
}
