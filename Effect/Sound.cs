using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    SoundManager soundManager = SoundManager.instance;

    public static Sound instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //효과음을 재생하는 메서드
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject start = new GameObject(sfxName + "Sound");
        AudioSource audiosource = start.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        //두번째 인자 값을 적어주면 해당 시간이 지난 후에 오브젝트를 파괴
        Destroy(start, clip.length);
    }
}
