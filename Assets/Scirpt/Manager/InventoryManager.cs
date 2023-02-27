using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Inventory_styte
{
    weapone,
    material,
    splinter
}


public class InventoryManager : UnitySingleton<InventoryManager>
{
    public List<string> Use_Inventory = new List<string>(); //拥有的物品
    public List<int> Use_Inventory_quality = new List<int>();//拥有物品的品质
    public List<GameObject> Use_InventoryObj = new List<GameObject>(); //拥有的物品
    public List<int> Use_InventoryCount = new List<int>();
    public Dictionary<string, GameObject> Inventory = new Dictionary<string, GameObject>(); //物品字典 key名字 value 预制体
    public List<string> Add_Inventory = new List<string>();//增加的物品
    public List<int> Add_Inventory_quality = new List<int>();//增加的物品品质
    public List<GameObject> InventoryList = new List<GameObject>();//拥有的物品列表 实例化
    string[] IventoryName = new string[] { "白色-武器1装备", "蓝色-装甲1装备", "黄色-核心1材料", "白色-核心1装备" };
    public List<GameObject> Inventory_GamObject = new List<GameObject>(); //所有的物品预制体


    public int index;//当前物品栏页面
    public GameObject Target; //当前点击的物品
    public List<GameObject> useObj = new List<GameObject>();
    public int Use_eqiue;//正在使用的物体
    public GameObject oldParent;

    public bool IsDrag;//开启拖拉;
    public int InvetoryCount;//当前拥有的物品数目
    public int PageCount;//物品占用了几个页面

    public List<GameObject> sellList = new List<GameObject>();//出售物品列表
    public Dictionary<GameObject, int> SellDic = new Dictionary<GameObject, int>();//key 出售物体 value 数量 
    public bool IsSell;//判断是否开启了一键出售

    public GameObject recyclrbin; //回收站避免重复初始化装备
    public Button Right_btn; //右边
    public Button left_btn; //左边


    public List<Texture2D> Armor_qu = new List<Texture2D>(); //不同等级的装备纹理

    private void Awake()
    {
        for (int i = 0; i < IventoryName.Length; i++)
        {
            Inventory.Add(IventoryName[i], Inventory_GamObject[i]);
        }

        Right_btn.onClick.AddListener(right);
        left_btn.onClick.AddListener(left);
    }

    private void Start()
    {
    }

    private void Update()
    {
        #region 拖拽功能--
        ///-----------------拖拽功能--------------//
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Target != null)
        //{
        //    Inventory_message.CloseMessage();
        //    if (oldParent == null)
        //        oldParent = Target.transform.parent.gameObject;
        //    Target.transform.SetParent(oldParent.transform.parent.transform.parent);
        //    Target.transform.SetSiblingIndex(100);
        //    Target.transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 10);
        //    Target.GetComponent<RawImage>().raycastTarget = false;
        //    IsDrag = true;
        //}
        //else
        //{
        //    //  IsDrag = false;
        //}
        #endregion //
    }


    #region 添加每一页的物品信息
    /// <summary>
    /// 添加物品栏每一页的物品信息
    /// </summary>
    void AddInventoryMessage(string name)
    {

    }
    #endregion


    void right()
    {
        index++;
        ShowInventory();
    }
    void left()
    {
        index = index - 1 < 0 ? 0 : index - 1;
        ShowInventory();
    }
    void ShowInventory() //背包左右移动展示拥有的物体
    {
        int a = index < PageCount ? Inventory_Bg.latticeList.Count : InventoryList.Count - index * Inventory_Bg.latticeList.Count;//当前页面拥有的物体
        for (int i = 0; i < Inventory_Bg.latticeList.Count; i++)
        {
            if (Inventory_Bg.latticeList[i].transform.childCount != 0)
                Inventory_Bg.latticeList[i].transform.GetChild(0).transform.SetParent(recyclrbin.transform, false);
        }
        for (int i = 0; i < a; i++)
        {
            InventoryList[(index) * Inventory_Bg.latticeList.Count + i].transform.SetParent(Inventory_Bg.latticeList[i].transform, false);
        }
    }
    public void RemoveInventoryList(GameObject obj) //装备使用后要移除在物品栏列表
    {
        InventoryList.Remove(obj);
    }
    public void ChangeInventoryList(GameObject use, GameObject takeoff)
    {
        int index = 0;
        for (int i = 0; i < InventoryList.Count; i++)
        {
            if (InventoryList[i] == use)
            {
                index = i;
            }
        }
        InventoryList[index] = takeoff;
    }
    public void Add()
    {
        for (int i = 0; i < Add_Inventory.Count; i++)
        {
            string n = Add_Inventory[i].Substring(Add_Inventory[i].Length - 2);
            if (n == "装备")
            {
                Use_Inventory.Add(Add_Inventory[i]);
                Use_InventoryCount.Add(1);
                Use_Inventory_quality.Add(Add_Inventory_quality[i]);
            }
            else if (n == "材料")
            {
                int index_M = -1;
                for (int j = 0; j < Use_Inventory.Count; j++)
                {
                    if (Use_Inventory[j] == Add_Inventory[i])
                    {
                        if (Use_Inventory_quality[j] == Add_Inventory_quality[i])
                        {
                            index_M = j;
                        }

                    }
                }
                if (index_M != -1)
                {
                    Use_InventoryCount[index_M]++;
                    Use_InventoryObj[index_M].GetComponent<Iventory>().count++;
                }
                else
                {
                    Use_Inventory.Add(Add_Inventory[i]);
                    Use_InventoryCount.Add(1);
                    Use_Inventory_quality.Add(Add_Inventory_quality[i]);
                }
            }
        }

        Add_Inventory.Clear();
        Add_Inventory_quality.Clear();
    }
    public void Init()
    {
        Add();

        for (int i = InvetoryCount; i < Use_Inventory.Count; i++)
        {
            if (InvetoryCount < Inventory_Bg.latticeList.Count)
            {
                InventoryList.Add(Instantiate(Inventory[Use_Inventory[i]], Inventory_Bg.latticeList[i - Use_eqiue].transform));
            }
            else
            {
                InventoryList.Add(Instantiate(Inventory[Use_Inventory[i]], recyclrbin.transform));
            }
            InventoryList[i - Use_eqiue].name += i;
            Iventory inventory = InventoryList[i - Use_eqiue].gameObject.GetComponent<Iventory>();
            inventory.count = Use_InventoryCount[i]; //设置物品数量
            inventory.quality = Use_Inventory_quality[i];//设置物品的品质
            inventory.Set_entry();
            inventory.gameObject.SetActive(false);
            inventory.gameObject.SetActive(true);

            Use_InventoryObj.Add(InventoryList[i - Use_eqiue]);
            InvetoryCount++;
        }

        SetPagCount();
    }
    public void SetPagCount()//设置物品页面
    {
        PageCount = InventoryList.Count % Inventory_Bg.latticeList.Count == 0 ? InventoryList.Count / Inventory_Bg.latticeList.Count - 1 : InventoryList.Count / Inventory_Bg.latticeList.Count;
        PageCount = PageCount < 0 ? 0 : PageCount;
        if (index > PageCount)
        {
            index--;
            ShowInventory();
        }
    }

    #region 出售物品

    public void IsOpenSell()
    {
        IsSell = IsSell == false ? true : false;
        SellDic.Clear();
    }


    /// <summary>
    /// 设置需要出售的物品
    /// </summary>
    /// 
    public void SetsellInventy(GameObject sellObj)
    {
        if (SellDic.ContainsKey(sellObj))
        {
            SellDic.Remove(sellObj);
            sellObj.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        else
        {
            SellDic.Add(sellObj, sellObj.GetComponent<Iventory>().count);
            sellObj.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        foreach (KeyValuePair<GameObject, int> item in SellDic)
        {
            Debug.Log(item);
        }

    }
    //一键出售
    public void Sell()
    {
        for (int j = Use_InventoryObj.Count - 1; j >= 0; j--)
        {
            foreach (KeyValuePair<GameObject, int> item in SellDic)
            {
                if (Use_InventoryObj[j] == item.Key)
                {
                    Debug.Log(j);
                    item.Key.GetComponent<Iventory>().selled(item.Value);
                    RemoveInventory(j);
                    SetPagCount();
                    removeInlattice(item.Key);
                    Destroy(item.Key);
                    break;
                }
            }
        }
        SellDic.Clear();
        IsSell = false;
    }
    //单个出售
    public void Sell(int count)
    {
        for (int i = Use_InventoryObj.Count - 1; i >= 0; i--)
        {
            if (Use_InventoryObj[i] == Target)
            {
                Use_InventoryCount[i] -= count;
                Target.GetComponent<Iventory>().count -= count;
                Target.GetComponent<Iventory>().selled(count);
                if (Use_InventoryCount[i] == 0)
                {
                    removeInlattice(Target);
                    RemoveInventory(i);
                    InvetoryCount--;
                    SetPagCount();
                    Destroy(Target);
                }
            }
        }
    }

    public void RemoveInventory(int j)
    {
        Use_Inventory.RemoveAt(j);
        Use_InventoryObj.RemoveAt(j);
        Use_InventoryCount.RemoveAt(j);
        InvetoryCount--;
    }

    public void removeInlattice(GameObject obj)//移除格子上的物品
    {
        if (!obj.GetComponent<Iventory>().isUse)
        {
            Inventory_Bg.ChangePosList(obj.transform.parent.gameObject); //使当前的物品格子在格子列表中移动到最后一个
            obj.transform.parent.transform.SetSiblingIndex(100);//修改子物体的排序
            if (((index + 1) * Inventory_Bg.cout) < InventoryList.Count)
                InventoryList[(index + 1) * Inventory_Bg.cout].transform.SetParent(obj.transform.parent.transform, false);

        }
        RemoveInventoryList(obj);
    }
    #endregion

    //public void UpdateList()
    //{
    //    Use_Inventory.Clear();
    //    Use_InventoryCount.Clear();
    //}


    public static void UpdateProperty(float Health = 0, float damage = 0, float back_blue = 0)
    {

        GameManager.Instance.Player.GetComponent<PlayerController>().SetAttribute(Health, damage, back_blue);

    }
}
