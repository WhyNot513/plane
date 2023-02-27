using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Bg : MonoBehaviour
{
    public GameObject Inventory_lattice; //物品格子物体
    public Vector2 offest;//物品栏间隔
    [Header("选择按钮范围")] public float bottom;
    float Inventory_lattice_Width;//物品格子宽
    float Inventory_lattice_Height;//物品格子高
    float Inventory_Width;//物品栏背景宽
    float Inventory_Height;//物品栏背景高
    int WidthCount; //生成的物品格子：宽
    int HeightCount;//生成的物品格子：长
    public static int cout;//生成的物品格子总数
    GridLayoutGroup GridGroup;
    public static List<GameObject> latticeList = new List<GameObject>(); //物品栏格子列表

    public Button sell;
    public Button Sure_sell;
    public Button back_btn;

    private void Awake()
    {
        GridGroup = this.GetComponent<GridLayoutGroup>() != null ? this.GetComponent<GridLayoutGroup>() : gameObject.AddComponent<GridLayoutGroup>().GetComponent<GridLayoutGroup>();
        GridGroup.spacing = offest;
        Inventory_lattice_Width = Inventory_lattice.GetComponent<RectTransform>().rect.width;
        Inventory_lattice_Height = Inventory_lattice.GetComponent<RectTransform>().rect.height;
        Inventory_Width = GetComponentInParent<RectTransform>().rect.width;
        Inventory_Height = GetComponent<RectTransform>().rect.height - bottom;
        GridGroup.cellSize = new Vector2(Inventory_lattice_Width, Inventory_lattice_Height);
        WidthCount = ((int)(Inventory_Width / (Inventory_lattice_Width + GridGroup.spacing.x)));
        HeightCount = ((int)(Inventory_Height / (Inventory_lattice_Height + GridGroup.spacing.y)));
        GridGroup.padding.left = (int)((Inventory_Width - (WidthCount * Inventory_lattice_Width + (WidthCount - 1) * GridGroup.spacing.x)) / 2);
        GridGroup.padding.top = (int)((Inventory_Height - (HeightCount * Inventory_lattice_Height + (HeightCount - 1) * GridGroup.spacing.y)) / 2);


        sell.onClick.AddListener(InventoryManager.Instance.IsOpenSell);
        Sure_sell.onClick.AddListener(InventoryManager.Instance.Sell);

        back_btn.onClick.AddListener(() => this.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false));
        back_btn.onClick.AddListener(() => EnemyManager.Instance.StartCoroutine(EnemyManager.Instance.StartGame()));
        Init();
    }
    private void OnEnable()
    {
        InventoryManager.Instance.Init();
    }
    private void Init()
    {
        latticeList.Clear();
        cout = WidthCount * HeightCount;
        for (int i = 0; i < cout; i++)
        {
            latticeList.Add(Instantiate(Inventory_lattice, gameObject.transform));
            latticeList[i].name = latticeList[0].name + i;
        }
    }

    public static void ChangePosList(GameObject obj)
    {
        latticeList.Remove(obj);
        latticeList.Add(obj);
    }


}
