using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Inventory_message : MonoBehaviour
{
    Text Name;
    public static UnityAction<GameObject> PointObj = delegate { };
    public Button back;

    public Button Use; //使用装备
    public Button TakeOff;//取消装备
    bool EquieUse;//判断该装备是否使用
    public Vector3 pos = new Vector3();
    public static UnityAction CloseMessage = delegate { };
    public Vector2 showButtonPos;//展示出来的位置
    public Vector2 NoShowButtonPos;//展示出来的位置

    public Button Sell_btn;//出售按钮
    private void Awake()
    {
        showButtonPos = Use.gameObject.GetComponent<RectTransform>().anchoredPosition;
        NoShowButtonPos = TakeOff.gameObject.GetComponent<RectTransform>().anchoredPosition;
        CloseMessage += close;
        PointObj += Show;
        Name = GetComponentInChildren<Text>();
        pos = this.gameObject.GetComponent<RectTransform>().localPosition;

        back.onClick.AddListener(() => Show(null));
        Use.onClick.AddListener(Useeqiue);
        TakeOff.onClick.AddListener(TakeItOff);

        Sell_btn.onClick.AddListener(Sell_panel.show);
    }

    private void OnDestroy()
    {
        PointObj -= Show;
        CloseMessage -= close;
    }

    private void Update()
    {

    }
    void Show(GameObject eqiue)
    {
        if (eqiue != null)
        {
            EquieUse = eqiue.GetComponent<Iventory>().isUse;
            UseOrTakeOff(EquieUse);
        }
        InventoryManager.Instance.Target = eqiue;

        if (eqiue != null)
        {
            if (!eqiue.CompareTag("goods")) return;

            Name.text = eqiue.name;
            this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
        else
        {
            close();
        }
    }
    public void close()
    {
        gameObject.GetComponent<RectTransform>().localPosition = pos;
    }

    void Useeqiue() //使用装备
    {
        string N = InventoryManager.Instance.Target.GetComponent<Iventory>().Name.Substring(InventoryManager.Instance.Target.GetComponent<Iventory>().Name.Length - 2);
        if (N == "材料")
        {
            //  if (InventoryManager.Instance.Target.GetComponent<Iventory>().count <= 0)
            {
                for (int i = 0; i < InventoryManager.Instance.Use_Inventory.Count; i++)
                {
                    if (InventoryManager.Instance.Use_Inventory[i] == InventoryManager.Instance.Target.GetComponent<Iventory>().Name)
                    {
                        InventoryManager.Instance.Use_InventoryCount[i]--;
                        InventoryManager.Instance.Target.GetComponent<Iventory>().count--;
                        if (InventoryManager.Instance.Use_InventoryCount[i] == 0)
                        {
                            InventoryManager.Instance.removeInlattice(InventoryManager.Instance.Target);
                            InventoryManager.Instance.RemoveInventory(i);
                            InventoryManager.Instance.InvetoryCount--;
                            InventoryManager.Instance.SetPagCount();
                            Destroy(InventoryManager.Instance.Target);
                        }
                    }
                }
            }
            Show(null);
            return;
        }


        int index = InventoryManager.Instance.Target.GetComponent<Iventory>().index;

        if (InventoryManager.Instance.useObj[index].transform.childCount > 0) //替换装备
        {
            GameObject child = InventoryManager.Instance.useObj[index].transform.GetChild(0).gameObject;
            child.GetComponent<Iventory>().takeoff();
            Transform parent = InventoryManager.Instance.Target.transform.parent;
            InventoryManager.Instance.Target.transform.SetParent(InventoryManager.Instance.useObj[index].transform, false); //装备的物体
            child.GetComponent<Iventory>().isUse = false;
            child.transform.SetParent(parent, false);//被替换的物体

            InventoryManager.Instance.ChangeInventoryList(InventoryManager.Instance.Target, child);
        }
        else
        {
            InventoryManager.Instance.removeInlattice(InventoryManager.Instance.Target);
            InventoryManager.Instance.Use_eqiue++;
            InventoryManager.Instance.Target.transform.SetParent(InventoryManager.Instance.useObj[index].transform, false);

        }
        InventoryManager.Instance.Target.GetComponent<Iventory>().Set();
        InventoryManager.Instance.Target.GetComponent<Iventory>().isUse = true;
        InventoryManager.Instance.SetPagCount();
        Show(null);

    }


    void TakeItOff() //卸下装备
    {
        int index = -1;
        InventoryManager.Instance.InventoryList.Add(InventoryManager.Instance.Target);
        for (int i = 0; i < Inventory_Bg.latticeList.Count; i++)
        {
            if (Inventory_Bg.latticeList[i].transform.childCount == 0)
            {
                index = i;
                break;
            }
        }
        InventoryManager.Instance.Target.GetComponent<Iventory>().takeoff(); //再更改父物体前修改
        if (index != -1)
        {
            InventoryManager.Instance.Target.transform.SetParent(Inventory_Bg.latticeList[index].transform, false);
        }
        else
        {
            InventoryManager.Instance.Target.transform.SetParent(InventoryManager.Instance.recyclrbin.transform, false);
        }
        InventoryManager.Instance.Use_eqiue--;
        InventoryManager.Instance.Target.GetComponent<Iventory>().isUse = false;
        InventoryManager.Instance.SetPagCount();
        Show(null);
    }
    void UseOrTakeOff(bool isUse) //判断出现使用装备还写卸下装备按钮
    {
        RectTransform UsePos = Use.GetComponent<RectTransform>();
        RectTransform TakeOffPos = TakeOff.GetComponent<RectTransform>();
        if (isUse)
        {

            UsePos.anchoredPosition = NoShowButtonPos;
            TakeOffPos.anchoredPosition = showButtonPos;
        }
        else
        {

            UsePos.anchoredPosition = showButtonPos;
            TakeOffPos.anchoredPosition = NoShowButtonPos;
        }
    }



}
