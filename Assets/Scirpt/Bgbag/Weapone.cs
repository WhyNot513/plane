using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapone : Iventory
{
    public int Health;
    public ProjectileType ProjectileType;
    public override void selled(int index)
    {
        base.selled(index);
    }
    public override void Set()
    {
        InventoryManager.UpdateProperty(Health: Health + this.transform.parent.GetComponent<equipment_slot>().addattribute);
        GameManager.Instance.Player.GetComponent<Player>().ProjectileType = ProjectileType;
    }
    public override void takeoff()
    {
        InventoryManager.UpdateProperty(Health: -(Health + this.transform.parent.GetComponent<equipment_slot>().addattribute));
        GameManager.Instance.Player.GetComponent<Player>().ProjectileType = ProjectileType.Basebullets;
    }
}
