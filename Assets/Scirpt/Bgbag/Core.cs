using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Iventory
{
    public float time;
    public PlaneType planeType;
    public override void selled(int index)
    {
        base.selled(index);
    }
    public override void Set()
    {
        InventoryManager.UpdateProperty(back_blue: (time + this.transform.parent.GetComponent<equipment_slot>().addattribute));
        GameManager.Instance.Player.GetComponent<Player>().plane = planeType;
    }
    public override void takeoff()
    {
        InventoryManager.UpdateProperty(back_blue: -(time + this.transform.parent.GetComponent<equipment_slot>().addattribute));
        GameManager.Instance.Player.GetComponent<Player>().plane = PlaneType.DefaultPanel;
    }
}
