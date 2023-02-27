using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MissingItem_panel : MonoBehaviour
{
    public Text itemName;//缺失的物品名字
    public Button No_btn;
    public Button Yes_btn;
    public static UnityAction<string> context = delegate { };
    private void Awake()
    {
        context += Show;
        No_btn.onClick.AddListener(() => this.gameObject.SetActive(false));
        Yes_btn.onClick.AddListener(OpenStore);
    }
    private void OnDestroy()
    {
        context -= Show;
    }
    private void OnEnable()
    {

    }
    public void Show(string Context)
    {

        itemName.text = Context;
    }
    public void OpenStore()
    {
        //打开商城界面
    }
}
