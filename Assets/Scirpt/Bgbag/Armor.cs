using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum Term_properties_Arrmor
{
    add_damage_5,
    add_damage_6,
    add_damage_7,
    add_damage_8,
    add_damage_9,
    add_damage_10,
    add_health,
}


public class Armor : Iventory
{
    public float damage;
    public List<Term_properties_Arrmor> entry;
    public List<int> damage_qu = new List<int>();//不同品质的装备的基础属性

    private void OnEnable()
    {
        Init();
    }
    public override void selled(int index)
    {
        base.selled(index);
    }
    public override void Set() //使用
    {
        InventoryManager.UpdateProperty(damage: (damage + this.transform.parent.GetComponent<equipment_slot>().addattribute));
        set_Term_properties();
    }
    public override void takeoff() //放下
    {
        InventoryManager.UpdateProperty(damage: -(damage + this.transform.parent.GetComponent<equipment_slot>().addattribute));
        Take_off_Term_properties();
    }
    /// <summary>
    /// 词条属性
    /// </summary>
    public void set_Term_properties()
    {

        int add = 0;
        for (int i = 0; i < entry.Count; i++)
        {
            switch (entry[i])
            {
                case Term_properties_Arrmor.add_damage_5:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.05f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;
                case Term_properties_Arrmor.add_damage_6:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.06f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;
                case Term_properties_Arrmor.add_damage_7:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.07f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;
                case Term_properties_Arrmor.add_damage_8:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.08f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;
                case Term_properties_Arrmor.add_damage_9:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.09f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;
                case Term_properties_Arrmor.add_damage_10:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.1f);
                    InventoryManager.UpdateProperty(damage: (add));
                    break;

                case Term_properties_Arrmor.add_health:
                    InventoryManager.UpdateProperty(damage: (damage + this.transform.parent.GetComponent<equipment_slot>().addattribute));
                    break;
                default:
                    break;
            }
        }
        Debug.Log(add);
    }
    public void Take_off_Term_properties()
    {
        int add = 0;
        for (int i = 0; i < entry.Count; i++)
        {
            switch (entry[i])
            {
                case Term_properties_Arrmor.add_damage_5:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.05f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                case Term_properties_Arrmor.add_damage_6:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.06f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                case Term_properties_Arrmor.add_damage_7:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.07f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                case Term_properties_Arrmor.add_damage_8:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.08f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                case Term_properties_Arrmor.add_damage_9:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.09f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                case Term_properties_Arrmor.add_damage_10:
                    add = (int)((damage + this.transform.parent.GetComponent<equipment_slot>().addattribute) * 0.1f);
                    InventoryManager.UpdateProperty(damage: (-add));
                    break;
                default:
                    break;
            }
        }
    }
    public void Init()
    {
        damage = damage_qu[quality];
        GetComponent<RawImage>().texture = InventoryManager.Instance.Armor_qu[quality];
    }
    public override void Set_entry()
    {
        int index = 0;
        bool a = false;
        for (int i = 0; i < quality; i++)
        {
            if (!a)
                index = Random.Range(0, 6);
            else
                index = Random.Range(2, 6);
            if (!a)
                a = index < 2 ? true : false;
            if (entry.Exists(t => t == (Term_properties_Arrmor)index))
            {
                i--;
            }
            else
            {
                entry.Add((Term_properties_Arrmor)index);
            }
        }

    }



}
