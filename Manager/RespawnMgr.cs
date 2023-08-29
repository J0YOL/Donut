using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageMob
{
    public List<GameObject> mobs = new List<GameObject>();
}

public class RespawnMgr : MonoBehaviour
{
    public List<StageMob> MobPool = new List<StageMob>();

    public int objCnt = 1;
    GameManager gm;

    void Awake()
    {
        gm = GameManager.instance;

        for (int i = 0; i < gm.stage.Length; i++)
        {
            StageMob stage = new StageMob();
            for(int x = 0; x < gm.stage[i].mobs.Length; x++)
            {
                for(int q = 0; q < objCnt; q++)
                {
                    stage.mobs.Add(CreateObj(gm.stage[i].mobs[x], transform));
                }
            }
            MobPool.Add(stage);
        }
    }

    private void Start()
    {
        //StartCoroutine(CreateMob());
    }

    //게임이 시작이 되면 몹이 재생되도록
    public void PlayGame()
    {
        if (true)
        {
            /*
            for (int i = 0; i < MobPool.Count; i++)
            {
                for (int x = 0; x < MobPool[i].mobs.Count; x++)
                {
                    if (MobPool[i].mobs[x].activeSelf)
                        MobPool[i].mobs[x].SetActive(false);
                }
            }
            */
            StartCoroutine(CreateMob());
        }
        else
            StopAllCoroutines();
    }

    IEnumerator CreateMob()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            MobPool[gm.curStage].mobs[DeactiveMob(MobPool[gm.curStage].mobs)].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }    
    }

    //중복 활성화를 막기위해서
    int DeactiveMob(List<GameObject> mobs)
    {
        List<int> num = new List<int>();
        for(int i = 0; i < mobs.Count; i++)
        {
            if (!mobs[i].activeSelf)
                num.Add(i);
        }
        int x = 0;
        if (num.Count > 0)
            x = num[Random.Range(0, num.Count)];
        return x;
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
