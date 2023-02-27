using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerProjectile
{



    private void Awake()
    {

    }
    protected override void OnEnable()
    {
        Grade = UpdateManager.Instance.Grade_Shotgun;
        if (Grade >= 1)
        {
            addDamage();
        }
        if (Grade >= 2)
        {

        }
        base.OnEnable();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Player.playshields.enabled == true && isKill)
        {
            Player_Shields.IsKilShiels = true;
            Player_Shields.addHealth(10);
        }
        if (Player.playshields.enabled == false && isKill && (UpdateManager.Instance.Grade_Shotgun >= 2))
        {
            Player_Shields.IsKilShiels = true;
            Player.playshields.enabled = true;
        }

    }

    public void addDamage()
    {
        damage = UpdateDameage;
        DefaultDamage = damage;
    }
    public void AddShields()
    {

    }

}
