using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : UnitySingleton<EnemyManager>
{
    [SerializeField] bool spaenEnemy = true;//是否生成敌人
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float TimeBetweenSpawns = 1f;//敌人生成间隔时间
    WaitForSeconds waitTimeBetweenSpawns;

    //[SerializeField] int minEnmeyAmount = 4; //最小敌人数量
    //[SerializeField] int maxEnmeyAmount = 10; //最大敌人数量
    public int waveNumber = 1; //敌人波数
    int EnemyAmount; //敌人总数
    public List<GameObject> enemies = new List<GameObject>();
    WaitUntil WaitUntilNoEmey;//会挂起等待知道某个条件满足的时候才会执行
    public float timeSpaee;//下一波生成间隔
    public bool TimeBetweenSpawnsEnemy; //是否开启下一波
    public float WaitTime;//等待的时间

    public Dictionary<int, List<EnemyType>> levelEnemyType_dic = new Dictionary<int, List<EnemyType>>(); //每一组合的飞机类型 
    public Dictionary<int, List<int>> levelEnemyCount_dic = new Dictionary<int, List<int>>();//每一组合的飞机数量

    public List<GameObject> xiao_enemylist = new List<GameObject>();//小型的飞机
    public List<GameObject> zhong_enemylist = new List<GameObject>();//中型的飞机
    public List<GameObject> Big_enemylist = new List<GameObject>(); //大型的飞机

    public int Leveindex;//每一关卡的索引
    public int roundIndex;//每一关卡的每一波的索引
    public GameObject RandomEnemy => enemies.Count == 0 ? null : enemies[Random.Range(0, enemies.Count)];

    private void Awake()
    {
        waitTimeBetweenSpawns = new WaitForSeconds(TimeBetweenSpawns);
        //    WaitUntilNoEmey = new WaitUntil(Noememy);//写法1
        WaitUntilNoEmey = new WaitUntil(() => enemies.Count == 0 || TimeBetweenSpawnsEnemy);//写法2

    }

    public void Init()
    {

    }

    public bool Noememy()
    {
        return enemies.Count == 0;
    }

    public IEnumerator StartGame()
    {
        while (spaenEnemy && GameManager.GameState != GameState.GameOver)
        {


            yield return WaitUntilNoEmey;
            yield return new WaitForSeconds(1f);
            TimeBetweenSpawnsEnemy = false;
            yield return StartCoroutine(nameof(RandomliSpawnCoroutine));
        }

    }

    IEnumerator RandomliSpawnCoroutine()
    {

        //EnemyAmount = Mathf.Clamp(EnemyAmount, minEnmeyAmount + waveNumber / 3, maxEnmeyAmount);

        if (roundIndex >= levelEnemyCount_dic[Leveindex].Count)
        {


            roundIndex = 0;
        }


        // roundIndex = 0;
        EnemyAmount = levelEnemyCount_dic[Leveindex][roundIndex];

        Debug.Log(EnemyAmount);

        for (int i = 0; i < EnemyAmount; i++)
        {
            switch (levelEnemyType_dic[Leveindex][roundIndex])
            {
                case EnemyType.xiao:
                    enemies.Add(PoolManager.Release(xiao_enemylist[Random.Range(0, xiao_enemylist.Count)]));
                    break;
                case EnemyType.zhong:
                    enemies.Add(PoolManager.Release(zhong_enemylist[Random.Range(0, zhong_enemylist.Count)]));
                    break;
                case EnemyType.da:
                    enemies.Add(PoolManager.Release(Big_enemylist[Random.Range(0, Big_enemylist.Count)]));
                    break;
                default:
                    break;
            }
            yield return waitTimeBetweenSpawns;
        }
        roundIndex++;



        //for (int i = 0; i < EnemyAmount; i++)
        //{
        //    enemies.Add(PoolManager.Release(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]));
        //    yield return waitTimeBetweenSpawns;
        //}
        timeSpaee = 0f;
        waveNumber++;
    }

    private void Update()
    {
        timeSpaee += Time.deltaTime;
        if (timeSpaee >= WaitTime)
        {
            TimeBetweenSpawnsEnemy = true;
            timeSpaee = 0f;
        }

    }
    public void RemoveFromlist(GameObject obj)
    {

        enemies.Remove(obj);
    }



}
