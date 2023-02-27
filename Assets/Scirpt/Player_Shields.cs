using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Shields : Shields
{
    public float timeKill;
    public float timeEnergy;

    float startTime_kill;
    float startTime_energy;
    public static bool IsKilShiels;
    public static bool EnergyShiels;
    public float CurrentHeat_energy;//能量爆发的护盾
    // Start is called before the first frame update

    public static UnityAction<float> addHealth = delegate { };
    public static UnityAction<float> addMaxHealth_energy = delegate { }; //设置大招护盾值
    protected override void Awake()
    {
        base.Awake();
        addMaxHealth_energy += AddMaxHealth_energy;
    }
    protected override void OpenShields()
    {
        currentHealth = 0;
        addHealth += AddCurHealth;
        if (IsKilShiels)
        {
            currentHealth = 10;
        }


        base.OpenShields();

    }

    private void OnDisable()
    {
        addHealth -= AddCurHealth;
        addMaxHealth_energy -= AddMaxHealth_energy;
    }
    protected override void CloseShields()
    {

        if (currentHealth <= 0 || timeKill > 10.0f)
        {
            if (IsKilShiels)
            {
                IsKilShiels = false;
                timeKill = 0;
                currentHealth = 0;
            }
        }
        if (CurrentHeat_energy <= 0 || timeEnergy > 5f)
        {
            if (EnergyShiels)
            {
                EnergyShiels = false;
                timeEnergy = 0;
                CurrentHeat_energy = 0;
            }
        }
        if (!IsKilShiels && !EnergyShiels)
        {
            this.enabled = false;
            Shield.SetActive(false);
            circle.enabled = false;
        }
        // Debug.Log(Time.unscaledDeltaTime);
        if (IsKilShiels)
        {
            timeKill += Time.unscaledDeltaTime;
        }
        if (EnergyShiels)
        {
            timeEnergy += Time.unscaledDeltaTime;
        }
    }

    public void AddCurHealth(float addHealth)
    {
        currentHealth += addHealth;
        timeKill = 0;
    }
    public override void takeDameage(float damage = 1)
    {
        if (CurrentHeat_energy > 0)
        {
            CurrentHeat_energy -= damage;
        }
        else
        {
            base.takeDameage(damage);
        }

    }
    public void AddMaxHealth_energy(float addHealth)
    {
        CurrentHeat_energy = addHealth;

    }


}
