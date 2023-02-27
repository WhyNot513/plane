using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePare_panel : MonoBehaviour
{

    public List<Button> buttons = new List<Button>();//创建了一个按钮列表  在场景中创建完按钮后,将按钮拖拽到列表中即可,不要要在脚本中声明按钮
    public List<Texture2D> weaponeList = new List<Texture2D>();
    public List<Texture2D> armorList = new List<Texture2D>();
    [SerializeField] RawImage WeaponeRaw;
    [SerializeField] RawImage armorRaw;

    public static int index_weapone;
    public static int index_armor;
    public static bool weapome;

    void Start()
    {
        AddClickEvents();
    }
    private void OnEnable()
    {
        WeaponeRaw.texture = weaponeList[index_weapone];
        armorRaw.texture = armorList[index_armor];
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
            case "Back_btn"://这里的Button1是指场景中,按钮的名字
                Back();
                break;
            case "aggrandizement_btn":
                mainPanel.aggrandizement_panel.SetActive(true);
                this.gameObject.SetActive(false);
                break;

            case "weapons_btn":
                weapome = true;

                OpenTips();
                break;
            case "armor_btn":
                weapome = false;

                OpenTips();
                break;
            case "weaponsLeft_btn":
                mainPanel.left(ref index_weapone, WeaponeRaw, weaponeList);
                break;
            case "weaponsright_btn":
                mainPanel.Right(ref index_weapone, WeaponeRaw, weaponeList);
                break;
            case "armorLeft_btn":
                mainPanel.left(ref index_armor, armorRaw, armorList);
                break;
            case "armorright_btn":
                mainPanel.Right(ref index_armor, armorRaw, armorList);
                break;
            default:
                break;
        }

    }
    void OpenTips()
    {

        mainPanel.Tips_panel.SetActive(true);


    }

    void Back()
    {
        this.gameObject.SetActive(false);
        mainPanel.select_panel.SetActive(true);
    }


}
