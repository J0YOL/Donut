using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
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
    public Slider BGMVolume;
    public Slider SfxVolume;
    public AudioSource musicsource;
    public AudioSource Effsource;

    public float BVol = 1f;
    public float SVol = 1f;

    private void Start()
    {
        BVol = PlayerPrefs.GetFloat("BVol", 1f);
        BGMVolume.value = BVol;
        musicsource.volume = BGMVolume.value;

        SVol = PlayerPrefs.GetFloat("SVol", 1f);
        SfxVolume.value = SVol;
        Effsource.volume = SfxVolume.value;
    }

    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        musicsource.volume = BGMVolume.value;
        BVol = BGMVolume.value;
        PlayerPrefs.SetFloat("BVol", BVol);

        Effsource.volume = SfxVolume.value;
        SVol = SfxVolume.value;
        PlayerPrefs.SetFloat("SVol", SVol);
    }

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    public void SetJumpVolume(float volume)
    {
        Effsource.volume = volume;
    }

    public void OnSfx()
    {
        Effsource.Play();
    }
}
