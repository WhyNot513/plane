using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    public InputField Input_UserName;
    public InputField Input_PassWord;
    public Button Btn_load;//登陆按钮
    private void Awake()
    {
        Btn_load.onClick.AddListener(JudeAcccount);
    }
    public void JudeAcccount()//判断账号是否存在
    {
        if (Input_UserName.text == LoadMananger.Instance.loadData.UserName && Input_PassWord.text == LoadMananger.Instance.loadData.UserPassWord)
        {
            this.gameObject.SetActive(false);
            Debug.Log("登陆成功");
        }
        else
        {
            Debug.Log("登陆失败");
        }
    }
    //玩家通过躲避怪物的攻击去寻找机械核心，收集两个机械核心则游戏获胜
}
