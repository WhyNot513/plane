using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aggrandizement_panel : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();//创建了一个按钮列表  在场景中创建完按钮后,将按钮拖拽到列表中即可,不要要在脚本中声明按钮
    public GameObject UpdatePanel;//选择不同升级界面
    public GameObject weapone_panel;//子弹升级界面
    public GameObject arrmor_panel;//大招升级界面
    public GameObject LeftAndRight;//左右选择界面
    void Start()
    {
        AddClickEvents();
    }
    private void OnEnable()
    {
        UpdatePanel.SetActive(true);
        weapone_panel.SetActive(false);
        arrmor_panel.SetActive(false);
        LeftAndRight.SetActive(false);
    }
    void AddClickEvents()
    {
        int x = 0;
        foreach (Button item in buttons)
        {
            int y = x;
            item.onClick.AddListener(() => ClickEvent2(y));//此处用的第二种点击方法
            x++;
        }
    }

    void ClickEvent2(int a)
    {
        //通过判断点击按钮的名字调用相应的方法
        switch (buttons[a].name)
        {
            case "Weapon_update"://这里的Button1是指场景中,按钮的名字
                UpdatePanel.gameObject.SetActive(false);
                LeftAndRight.SetActive(true);
                weapone_panel.SetActive(true);
                break;
            case "arromr_update":
                UpdatePanel.gameObject.SetActive(false);
                LeftAndRight.SetActive(true);
                arrmor_panel.SetActive(true);
                break;

            case "Back_btn":
                mainPanel.select_panel.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            case "opean_Panel":
                this.gameObject.SetActive(false);
                mainPanel.Prepare_panel.SetActive(true);
                break;
            case "Left_btn":
                weapone_panel.SetActive(true);
                arrmor_panel.SetActive(false);
                break;
            case "right_btn":
                arrmor_panel.SetActive(true);
                weapone_panel.SetActive(false);
                break;
            default:
                break;
        }
    }
}
