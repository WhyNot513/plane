using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    ready,
    playing, //游戏中
    Paused, //游戏暂停
    GameOver, //游戏结束
}

public class GameManager : UnitySingleton<GameManager>
{
    public static GameState GameState { get => Instance.gameState; set => Instance.gameState = value; }
    [SerializeField] GameState gameState = GameState.playing;
    public GameObject Player;
    public int coin;
    public float player_damage;//飞机伤害
    //public int levelMessage; // 每一章节的关卡信息
    //public int chapterMessage;//章节信息
    /// <summary>
    ///  0：普通子弹 1：固定子弹 ：2追钟子弹
    /// </summary>
    [Header("0：普通子弹 1：固定子弹 ：2追钟子弹")] public List<GameObject> EnemeyProjecte = new List<GameObject> { };

    public void GameStart(GameObject gameObject)
    {

        {
            gameObject.SetActive(false);
            Player.SetActive(true);
            EnemyManager.Instance.StartCoroutine(EnemyManager.Instance.StartGame());
            EnemyManager.Instance.Leveindex = levelMessage.Instance.levelNumber;
        }
    }
}


