using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlane_panel : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();//创建了一个按钮列表  在场景中创建完按钮后,将按钮拖拽到列表中即可,不要要在脚本中声明按钮
    public RawImage Plane;
    public List<Texture2D> Plane_texture = new List<Texture2D>();
    public List<Sprite> Plane_Sprite = new List<Sprite>();
    public int index;
    public GameObject Player;//玩家的飞机
    [SerializeField] GameObject Mask;

    void Start()
    {
        AddClickEvents();
    }
    private void OnEnable()
    {
        Plane.texture = Plane_texture[index];
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
            case "GameStart_btn"://这里的Button1是指场景中,按钮的名字

                Back();
                Player.SetActive(true);
                break;
            case "aggrandizement_btn":
                mainPanel.aggrandizement_panel.SetActive(true);
                this.gameObject.SetActive(false);
                break;

            case "shop_btn":

                break;
            case "OpenReady_btn":
                Open_PreparePanel();

                break;
            case "Left_btn":
                mainPanel.left(ref index, Plane, Plane_texture);
                break;
            case "right_btn":
                mainPanel.Right(ref index, Plane, Plane_texture);
                break;
            default:
                break;
        }
    }

    void Back()
    {
        mainPanel.Back();
        this.gameObject.SetActive(false);
        assemble();
        EnemyManager.Instance.StartCoroutine(EnemyManager.Instance.StartGame());

    }
    void Open_PreparePanel()
    {
        this.gameObject.SetActive(false);
        mainPanel.Prepare_panel.SetActive(true);
    }

    /// <summary>
    /// 飞机组装
    /// </summary>
    void assemble()
    {
        //t2d为待转换的Texture2D对象

        //飞机样式
        Player.gameObject.GetComponent<SpriteRenderer>().sprite = Plane_Sprite[index] as Sprite;
        //大招类型

        switch (PrePare_panel.index_armor)
        {
            case 0:
                Player.GetComponent<Player>().plane = PlaneType.DefaultPanel;
                break;
            case 1:
                Player.GetComponent<Player>().plane = PlaneType.AttackPanel;
                break;
            case 2:
                Player.GetComponent<Player>().plane = PlaneType.AddbloodPlane;

                break;
            case 3:
                Player.GetComponent<Player>().plane = PlaneType.TimeSlowPlane;

                break;
            case 4:
                Player.GetComponent<Player>().plane = PlaneType.addDamagePlane;

                break;

            default:
                break;
        }


        //子弹类型
        switch (PrePare_panel.index_weapone)
        {
            case 0:
                Player.GetComponent<Player>().ProjectileType = ProjectileType.Basebullets;
                break;
            case 1:
                Player.GetComponent<Player>().ProjectileType = ProjectileType.Missilebullets;
                break;
            case 2:
                Player.GetComponent<Player>().ProjectileType = ProjectileType.Laserbullets;

                break;
            case 3:
                Player.GetComponent<Player>().ProjectileType = ProjectileType.Trackbullets;
                break;
            case 4:
                Player.GetComponent<Player>().ProjectileType = ProjectileType.Shotguns;

                break;

            default:
                break;
        }

    }





}
