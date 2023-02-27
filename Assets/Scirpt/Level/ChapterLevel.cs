using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLevel : MonoBehaviour
{
    public GameObject guanka;
    public Transform father;
    List<GameObject> guankaList = new List<GameObject>();


    int levelcount;
    private void OnEnable()
    {
        levelcount = levelMessage.Instance.chapterLevel[levelMessage.Instance.chapterMessage + 1].Count;
        Init();
    }
    public void Init()
    {
        if (guankaList.Count == 0)
        {
            for (int i = 1; i < levelcount + 1; i++)
            {
                GameObject o = Instantiate(guanka, father);
                o.GetComponent<level>().Level = i;
                o.GetComponent<level>().Init();
                guankaList.Add(o);
            }
            return;
        }
        if (guankaList.Count <= levelcount)
        {
            for (int i = 0; i < levelcount - guankaList.Count; i++)
            {
                GameObject o = Instantiate(guanka, father);
                guankaList.Add(o);
            }
            for (int i = 0; i < guankaList.Count; i++)
            {
                guankaList[i].SetActive(true);
                guankaList[i].GetComponent<level>().Level = i + 1;
                guankaList[i].GetComponent<level>().Init();
            }
        }
        if (guankaList.Count > levelcount)
        {
            for (int i = 0; i < guankaList.Count; i++)
            {
                if (i < levelcount)
                {
                    guankaList[i].SetActive(true);
                    guankaList[i].GetComponent<level>().Level = i + 1;
                    guankaList[i].GetComponent<level>().Init();

                }
                else
                {
                    guankaList[i].SetActive(false);

                }
            }
        }

    }

}
