using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusrKillManager : UnitySingleton<MusrKillManager>
{
    float explosionRadius = 10f;
    [SerializeField] List<LayerMask> GamobjecttMask = new List<LayerMask>();
    [SerializeField] PlayerController playerController;
    [SerializeField] Player player;
    [SerializeField] float Muse2damage;
    public GameObject Wingman;
    public bool isTimeslow;//是否开启了敌机减速的功能
    public bool IsAddDamage;//是否开启增加伤害
    public float DamagePrecent;//开启大招后子弹增加的伤害

    public GameObject Coin;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void OverDrving(PlaneType type)
    {
        switch (type)
        {
            case PlaneType.DefaultPanel:
                foreach (var item in Physics2D.OverlapCircleAll(transform.position, explosionRadius, GamobjecttMask[0]))
                {
                    int a = Random.Range(0, 2);

                    if (a == 0)
                        PoolManager.Release(Coin, item.transform.localPosition);
                    item.gameObject.SetActive(false);
                }
                //消除场上子弹
                break;
            case PlaneType.AttackPanel:
                float precent = 1;
                switch (Physics2D.OverlapCircleAll(transform.position, explosionRadius, GamobjecttMask[1]).Length)
                {
                    case 1:
                        precent = 2f;
                        break;
                    case 2:
                        precent = 1.3f;
                        break;
                    case 3:
                        precent = 1.1f;
                        break;

                    default:
                        break;
                }
                foreach (var item in Physics2D.OverlapCircleAll(transform.position, explosionRadius, GamobjecttMask[1]))
                {
                    if (item.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.TakeDamage(Muse2damage * precent);
                    }
                }
                //全屏大额伤害
                break;
            case PlaneType.AddbloodPlane:
                if (gameObject.activeSelf)
                {
                    Player.playshields.enabled = true;
                    Player_Shields.EnergyShiels = true;
                    Player_Shields.addMaxHealth_energy(playerController.MaxHealth);

                    if (playerController.HealthRegenerateCor != null)
                    {
                        StopCoroutine(playerController.HealthRegenerateCor);
                    }
                    if (playerController.regenerateHealth)
                    {
                        playerController.HealthRegenerateCor = StartCoroutine(playerController.HealthRegenerateCoroutine(playerController.waitHealthRegenerateTime, playerController.HealthRegeneratePercent));

                    }
                }
                //回血
                break;
            case PlaneType.TimeSlowPlane:
                //全场减速
                isTimeslow = true;
                player.Firetime /= 2;
                Time.timeScale = 0.3f;
                break;

            case PlaneType.addDamagePlane:
                IsAddDamage = true;
                Wingman.SetActive(true);
                DamagePrecent = 2f;
                break;

            default:
                break;
        }
    }
    private void Update()
    {
        if (player.isOverDriving == false && playerController.HealthRegenerateCor != null)
        {
            StopCoroutine(playerController.HealthRegenerateCor);

            playerController.HealthRegenerateCor = null;
        }
        if (player.isOverDriving == false && isTimeslow)
        {
            player.Firetime *= 2;
            Time.timeScale = 1;
            MusrKillManager.Instance.isTimeslow = false;
        }
        if (player.isOverDriving == false && IsAddDamage)
        {
            MusrKillManager.Instance.IsAddDamage = false;
            Wingman.SetActive(false);
        }
    }



}
