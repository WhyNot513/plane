using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakeUpSign : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //打开是否补签的面板
        sign_inManage.Instance.repair_panel.SetActive(true);
    }
}
