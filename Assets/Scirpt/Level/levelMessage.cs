using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    xiao,
    zhong,
    da
}

public class levelMessage : UnitySingleton<levelMessage>
{
    [Header("--------------每一组合的飞机类型数量--------------------")]
    public List<int> Round1Count = new List<int>();
    public List<int> Round2Count = new List<int>();
    public List<int> Round3Count = new List<int>();
    public List<int> Round4Count = new List<int>();
    public List<int> Round5Count = new List<int>();
    public List<int> Round6Count = new List<int>();
    public List<int> Round7Count = new List<int>();
    public List<int> Round8Count = new List<int>();
    public List<int> Round9Count = new List<int>();
    public List<int> Round10Count = new List<int>();
    public List<int> Round11Count = new List<int>();
    public List<int> Round12Count = new List<int>();

    [Header("--------------每一组合的飞机类型--------------------")]
    public List<EnemyType> Round1Type = new List<EnemyType>();
    public List<EnemyType> Round2Type = new List<EnemyType>();
    public List<EnemyType> Round3Type = new List<EnemyType>();
    public List<EnemyType> Round4Type = new List<EnemyType>();
    public List<EnemyType> Round5Type = new List<EnemyType>();
    public List<EnemyType> Round6Type = new List<EnemyType>();
    public List<EnemyType> Round7Type = new List<EnemyType>();
    public List<EnemyType> Round8Type = new List<EnemyType>();
    public List<EnemyType> Round9Type = new List<EnemyType>();
    public List<EnemyType> Round10Type = new List<EnemyType>();
    public List<EnemyType> Round11Type = new List<EnemyType>();
    public List<EnemyType> Round12Type = new List<EnemyType>();



    [Header("--------------当前章节从0开始--------------------")]
    public int chapterMessage;//当前章节
    [Header("-------------- 当前章节选的关卡--------------------")]
    public int levelNumber;//当前章节的关卡



    //  [Header("--------------每一章的关卡数据--------------------")]
    //--------------每一章的关卡数据--------------------
    List<int> Chapter1Level = new List<int> { 1, 2, 3, 4, 5, 6 };//
    List<int> Chapter2Level = new List<int> { 2, 2, 3, 4, 5 };//
    List<int> Chapter3Level = new List<int> { 3, 2, 3, 4 };//
    List<int> Chapter4Level = new List<int> { 4, 2, 3 };//
    List<int> Chapter5Level = new List<int> { 5, 2, 3, 4, 5 };//
    List<int> Chapter6Level = new List<int> { 6, 2, 3, 4, 5, 6 };//
    public Dictionary<int, List<int>> chapterLevel = new Dictionary<int, List<int>>();
    private void Start()
    {
        for (int i = 1; i < 13; i++)
        {
            switch (i)
            {
                case 1:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round1Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round1Type);
                    break;
                case 2:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round2Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round2Type);
                    break;
                case 3:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round3Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round3Type);
                    break;
                case 4:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round4Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round4Type);
                    break;
                case 5:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round5Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round5Type);
                    break;
                case 6:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round6Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round6Type);
                    break;
                case 7:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round7Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round7Type);
                    break;
                case 8:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round8Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round8Type);
                    break;
                case 9:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round9Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round9Type);
                    break;
                case 10:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round10Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round10Type);
                    break;
                case 11:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round11Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round11Type);
                    break;
                case 12:
                    EnemyManager.Instance.levelEnemyCount_dic.Add(i, Round12Count);
                    EnemyManager.Instance.levelEnemyType_dic.Add(i, Round12Type);
                    break;
                default:
                    break;
            }

        }

        chapterLevel.Add(1, Chapter1Level);
        chapterLevel.Add(2, Chapter2Level);
        chapterLevel.Add(3, Chapter3Level);
        chapterLevel.Add(4, Chapter4Level);
        chapterLevel.Add(5, Chapter5Level);
        chapterLevel.Add(6, Chapter6Level);
    }
}
