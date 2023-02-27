using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mainPanel : MonoBehaviour
{
    public GameObject Prepare; //配置界面
    public GameObject Tips; //提示界面
    public GameObject select; //选择界面

    public GameObject aggrandizement; //强化界面
    public GameObject mask;

    public static GameObject Prepare_panel;
    public static GameObject Tips_panel;
    public static GameObject select_panel;
    public static GameObject aggrandizement_panel;
    public static GameObject Mask;
    public static List<string> WeaponeName = new List<string> { "默认子弹", "导弹", "激光", "追钟", "散弹" };
    private void Awake()
    {
        Prepare_panel = Prepare;
        Tips_panel = Tips;
        select_panel = select;
        aggrandizement_panel = aggrandizement;
        Mask = mask;

    }
    public static void Back()
    {
        Mask.gameObject.SetActive(false);
    }

    public static void left(ref int index, RawImage texure2D, List<Texture2D> list)
    {
        index = index - 1 < 0 ? 0 : index - 1;



        texure2D.texture = list[index];
    }
    public static void Right(ref int index, RawImage texure2D, List<Texture2D> list)
    {
        index = index + 1 >= list.Count ? index : index + 1;

        texure2D.texture = list[index];
    }


}
