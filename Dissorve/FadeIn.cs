﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();    
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        Color color = image.color;

        if (color.a > 0)
        {
            color.a -= Time.deltaTime;
        }

        image.color = color;
    }
}
