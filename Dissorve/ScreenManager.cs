using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager m_instance;
    public static ScreenManager _instance
    {
        get
        {
            return m_instance;
        }
        set
        {
            m_instance = value;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Texture2D ScreenTexture;

    IEnumerator CaptureScreen()
    {
        //텍스쳐 자료형 변수를 생성
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        //화면의 픽셀 데이터를 읽어서 텍스쳐화 하는 과정
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();
        ScreenTexture = texture;
    }

    public void LoadScreenTextrue()
    {
        StartCoroutine(CaptureScreen());
    }
}
