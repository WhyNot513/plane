using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sell_panel : MonoBehaviour
{
    public Button Back_btn;
    public Slider slider;
    public Button sell_btn;
    int count;
    Vector3 Startpos;
    public static GameObject panel;
    private void Awake()
    {
        panel = this.gameObject;
        Startpos = GetComponent<RectTransform>().localPosition;
        sell_btn.onClick.AddListener(sell);
        Back_btn.onClick.AddListener(() => Inventory_message.PointObj(null));
        Inventory_message.PointObj += Setting;
    }
    private void OnDestroy()
    {
        Inventory_message.PointObj -= Setting;
    }
    public void Setting(GameObject obj)
    {
        if (obj == null)
        {
            gameObject.GetComponent<RectTransform>().localPosition = Startpos;
        }
        else
        {
            slider.maxValue = obj.gameObject.GetComponent<Iventory>().count;
            slider.value = 0;
        }
    }
    public static void show()
    {
        panel.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
    public void sell()
    {
        count = (int)slider.value;
        InventoryManager.Instance.Sell(count);
        Inventory_message.PointObj(null);

    }


}
