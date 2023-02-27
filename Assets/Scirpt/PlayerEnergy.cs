using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : UnitySingleton<PlayerEnergy>
{
    [SerializeField] Energybar energybar;
    [SerializeField] float overdriveInterval = 0.1f;//能量池消耗时间
    [SerializeField] float AfterUseWait = 5f;//能量条使用后的冷却时间
    [SerializeField] int Precent;//消耗能量百分比
    [SerializeField] Player player;
    [SerializeField] int Automatic_recovery; //自动恢复蓝量
    public float recovery_time;//恢复间隔
    public const int MAX = 100;
    public const int PRECRNT = 10;
    public int energy = 90;
    bool available = true;//判断是否可以获得能量

    WaitForSeconds waitForOverdriveInterval;
    private void Awake()
    {
        waitForOverdriveInterval = new WaitForSeconds(overdriveInterval);


    }
    private void OnEnable()
    {
        StartCoroutine(nameof(back_energy));

        playerOverdrive.on += PlayerOverDriveOn;
        playerOverdrive.off += PlayerOverDriveOff;
    }
    private void OnDisable()
    {
        playerOverdrive.on -= PlayerOverDriveOn;
        playerOverdrive.off -= PlayerOverDriveOff;
    }
    private void Start()
    {
        energybar.Initalize(energy, MAX);
    }
    public void Obtain(int value)
    {
        if (energy == MAX || !available || !gameObject.activeSelf) return;

        energy = Mathf.Clamp(energy + value, 0, MAX);
        energybar.UpdateStats(energy, MAX);
    }
    public void Use(int value)
    {
        energy -= value;
        energybar.UpdateStats(energy, MAX);

        if (energy == 0 && !available)
        {
            //Debug.Log(energy + "---------" + available);
            playerOverdrive.off();
        }
    }
    public bool isEnough(int value) => energy >= value;

    public void PlayerOverDriveOn()
    {
        //  Debug.LogWarning("77777777777");
        available = false;
        StartCoroutine(nameof(KeepUsingCoroutine));
    }
    public void PlayerOverDriveOff()
    {
        //  Debug.LogWarning("888888888888888");
        StartCoroutine(nameof(AfterUseEnergy));
        StopCoroutine(nameof(KeepUsingCoroutine));
    }
    IEnumerator KeepUsingCoroutine()
    {
        while (gameObject.activeSelf && energy > 0)
        {
            if (player != null)
            {
                switch (player.plane)
                {
                    case PlaneType.DefaultPanel:
                        Precent = 100;
                        break;
                    case PlaneType.AttackPanel:
                        Precent = 100;
                        //全屏大额伤害
                        break;
                    case PlaneType.AddbloodPlane:
                        Precent = 1;
                        //回血
                        break;
                    case PlaneType.TimeSlowPlane:
                        //全场减速
                        Precent = 1;
                        break;
                    case PlaneType.addDamagePlane:
                        Precent = 1;
                        break;

                }
            }
            Use(Precent);
            float endPauseTime = Time.realtimeSinceStartup + overdriveInterval;
            yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime);

            //  yield return waitForOverdriveInterval;
        }
    }

    IEnumerator AfterUseEnergy()

    {
        float endPauseTime = Time.realtimeSinceStartup + AfterUseWait;
        yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime);
        available = true;
    }
    public virtual void RestoryEnergy(int value)
    {
        if (MAX == energy) return;

        energy = (int)Mathf.Clamp(energy + value, 0, MAX); //防止回蓝加过了
        energybar.UpdateStats(energy, MAX);
    }

    IEnumerator back_energy()
    {
        while (energy < MAX)
        {
            if (available == true && player.gameObject.activeInHierarchy)
            {
                //    Debug.Log("hi");
                RestoryEnergy(Automatic_recovery);
            }
            float endPauseTime = Time.realtimeSinceStartup + recovery_time;
            yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime);
        }



    }
    //该游戏是一款2d动作冒险的游戏,玩家需要在游戏关卡中寻找并收集机械核心从而获得游戏胜利


}
