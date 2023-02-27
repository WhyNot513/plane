using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = " SignInDate", order = 1)]
public class SignInDate : ScriptableObject
{
    public string LasterSignin_time;//上一次签到的时间
    public string NowSignin_time;//当前的时间
    public bool IsSignIn;//判单今天是否签到了
    public int CumulativeSignIn;//本月累计签到次数
    public List<int> sign_dayDate = new List<int>(); // 值为0 没有签到的 值为1 正常签到 值为2 补签

    public List<int> GiftBaglist = new List<int>() { 0, 0, 0 };//开启礼包的

}
