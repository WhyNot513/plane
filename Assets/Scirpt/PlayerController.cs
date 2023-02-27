using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Character
{
    [SerializeField] public bool regenerateHealth = true; //玩家是否恢复生命值
    [SerializeField] float HealthRegenerateTime;//恢复生命值时间
    [SerializeField] public float HealthRegeneratePercent;//恢复生命值百分比


    public WaitForSeconds waitHealthRegenerateTime;
    public Coroutine HealthRegenerateCor;

    [SerializeField] protected Statebar_Hub statrbar;
    [SerializeField] protected Energybar energybar;



    private void Awake()
    {



    }
    private void OnDestroy()
    {

    }
    private void Start()
    {
        waitHealthRegenerateTime = new WaitForSeconds(HealthRegenerateTime);

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        statrbar.Initalize(currentHealth, MaxHealth);


    }
    private void Update()
    {
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        statrbar.UpdateStats(currentHealth, MaxHealth);

    }
    public override void RestoryHealth(float value)
    {
        base.RestoryHealth(value);
        statrbar.UpdateStats(currentHealth, MaxHealth);
    }

    public override void Die()
    {
        base.Die();
        Player.CanMove = false;
        statrbar.UpdateStats(0, MaxHealth);
        GameManager.GameState = GameState.GameOver;
    }

    public void SetAttribute(float Health = 0, float damage = 0, float back_bule = 0)
    {
        MaxHealth += Health;
        GameManager.Instance.player_damage += damage;
        PlayerEnergy.Instance.recovery_time -= back_bule;
    }


}
