using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftBag : MonoBehaviour, IPointerDownHandler
{
    public int Unlock; //解锁条件
    public bool IsOpen;//判断是否已经领取了
    private void OnEnable()
    {
        sign_inManage.Instance.JudeGiftOpen(ref IsOpen, Unlock);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        sign_inManage.Instance.JudeGiftOpen(ref IsOpen, Unlock);
        if (!IsOpen)
            if (sign_inManage.Instance.OpenGiftBag(Unlock))
            {
                Debug.Log("开礼包");
            }
            else
            {
                Debug.Log("天数不够");
            }
        else
        {
            Debug.Log("已经开启过了");
        }
    }
}
