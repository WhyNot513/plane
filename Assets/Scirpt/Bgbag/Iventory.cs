using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Iventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isUse;//是否被使用了
    public int index = 1;
    public int count;
    public string Name;
    public Inventory_styte inventory_Styte;
    public int quality;// 0是白色 1是蓝色 2是紫色 3是红色 相对应可以拥有的词条数目


    public void OnPointerDown(PointerEventData eventData)
    {
        if (InventoryManager.Instance.Target != null) return;
        if (InventoryManager.Instance.IsSell)
        {
            InventoryManager.Instance.SetsellInventy(this.gameObject);
        }
        else
            Inventory_message.PointObj(this.gameObject);

    }
    private void OnDestroy()
    {

    }
    public virtual void selled(int index)
    {
        for (int i = 0; i < index; i++)
        {
            GameManager.Instance.coin += 100;
            Debug.Log(GameManager.Instance.coin);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //-------------拖拽逻辑------//
        //GameObject go = eventData.pointerCurrentRaycast.gameObject;

        //if (InventoryManager.Instance.IsDrag == true)
        //    if (go.tag == "lattice" && go.transform.childCount == 0)
        //    {
        //        SetAnchorpoint(go);
        //        InventoryManager.Instance.IsDrag = false;
        //        InventoryManager.Instance.Target = null;
        //    }
        //    else
        //    {
        //        SetAnchorpoint(InventoryManager.Instance.oldParent.gameObject);
        //        InventoryManager.Instance.oldParent = null;
        //        InventoryManager.Instance.IsDrag = false;
        //        InventoryManager.Instance.Target = null;
        //    }
        //GetComponent<RawImage>().raycastTarget = true;
        //InventoryManager.Instance.oldParent = null;
    }

    void SetAnchorpo(GameObject go)
    {
        this.gameObject.transform.SetParent(go.transform);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }


    public virtual void Set()
    {

    }
    public virtual void takeoff()
    {

    }
    public virtual void Set_entry() //设置词条
    {

    }
    public virtual void Set_entry(int a) //设置词条
    {

    }

}


