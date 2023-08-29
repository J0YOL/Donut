using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEff : MonoBehaviour
{
    public GameObject heartPrefab;
    float spawnsTime;
    public float defaultTime = 0.05f;

    void Update()
    {
        if (Input.GetMouseButton(0)&&spawnsTime>=defaultTime)
        {
            HeartCreate();
            spawnsTime = 0;
        }
        spawnsTime += Time.deltaTime;
    }

    void HeartCreate()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(heartPrefab, mPosition, Quaternion.identity);
    }
}
