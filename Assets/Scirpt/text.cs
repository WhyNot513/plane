using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    // Start is called before the first frame update





    [SerializeField] Text text1;
    [SerializeField] Button btn;


    [SerializeField] Text text2;
    [SerializeField] Button btn2;
    //生命数
    public int live = 5;
    public CodeStage.AntiCheat.ObscuredTypes.ObscuredInt live2 = 10;

    void Start()
    {
        text1.text = "剩余生命：" + live;

        btn.onClick.AddListener(() =>
        {
            live--;
            if (live > 0)
            {
                text1.text = "剩余生命：" + live;
            }
            else
            {
                text1.text = "游戏结束";
            }
        });

        text2.text = "剩余生命：" + live2;

        btn2.onClick.AddListener(() =>
        {
            live2--;
            if (live2 > 0)
            {
                text2.text = "剩余生命：" + live2;
            }
            else
            {
                text2.text = "游戏结束";
            }
        });
    }
}



