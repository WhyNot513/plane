using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class sign_inManage : UnitySingleton<sign_inManage>
{
    public List<int> sign_day = new List<int>(); // 值为0 没有签到的 值为1 正常签到 值为2 补签
    public Button sign_in_btn;

    public Button repair_signin_btn;//补签按钮
    public Button Back_btn_repair;//补签面板的返回
    public GameObject repair_panel;//补签面板
    public Button addTime;


    public Button back_reward;//获奖界面
    public GameObject reard_panel;

    public GameObject MissingItem;//缺失物品界面

    public SignInDate SignInDate;

    int Time_signin;//签到时间

    public string time;
    public string Nowtime;
    public static DateTime SeverNow;

    public Text CumulativeSignIn_txt;

    private void Awake()
    {
        time = SignInDate.LasterSignin_time.ToString();

        sign_in_btn.onClick.AddListener(() => SignIn());
        repair_signin_btn.onClick.AddListener(reapir_signin);
        addTime.onClick.AddListener(() => addDay());
        Back_btn_repair.onClick.AddListener(() => Back(repair_panel));
        back_reward.onClick.AddListener(() => Back(reard_panel));
        IsOpenSignPanel();
        Time_signin = SeverNow.Day;
    }
    void addDay()
    {
        SeverNow = SeverNow.AddDays(1);
        // Debug.Log(SeverNow.ToString());
        Nowtime = SeverNow.ToString();
        SignInDate.NowSignin_time = Nowtime;
        Time_signin = SeverNow.Day;
    }


    public void reapir_signin() //补签
    {
        if (Time_signin - 2 >= 0)
            if (sign_day[Time_signin - 2] == 1)
            {
                Debug.Log("没有需要补签的");

            }
            else
            {
                //弹窗 是否消耗东西补签 不够 就弹出不足是否购买 够就补签
                sign_day[Time_signin - 2] = 2;
                SignInDate.sign_dayDate = sign_day;
                SignInDate.CumulativeSignIn++;
                CumulativeSignIn_txt.text = "累计签到" + SignInDate.CumulativeSignIn.ToString();
                sign_in.Day_mothlist[Time_signin - 2].GetComponent<RawImage>().color = Color.blue;
                Back(repair_panel);
                reard_panel.SetActive(true);
            }


    }
    public void Back(GameObject obj) //返回按钮
    {
        obj.SetActive(false);
    }
    public void SignIn()//签到
    {

        if (sign_day[Time_signin - 1] != 0 && sign_day.Count > 0) return;
        sign_day[Time_signin - 1] = 1;
        SignInDate.LasterSignin_time = SeverNow.ToString();
        time = SignInDate.LasterSignin_time;
        SignInDate.IsSignIn = true;
        SignInDate.sign_dayDate = sign_day;
        SignInDate.CumulativeSignIn++;
        CumulativeSignIn_txt.text = "累计签到" + SignInDate.CumulativeSignIn.ToString();
        sign_in.Day_mothlist[Time_signin - 1].GetComponent<RawImage>().color = Color.red;

        reard_panel.SetActive(true);



    }
    public void IsOpenSignPanel() //判断是否要打开签到页面
    {
        // 把字符串类型日期转换为日期类型
        // SeverNow = Convert.ToDateTime(Nowtime); //本次登录的时间
        SeverNow = Convert.ToDateTime(SignInDate.NowSignin_time); //本次登录的时间
        DateTime laster = Convert.ToDateTime(SignInDate.LasterSignin_time);
        if (SeverNow.Year != laster.Year || SeverNow.Month != laster.Month || SeverNow.Day != laster.Day)
        {
            SignInDate.IsSignIn = false;
        }
        if (SeverNow.Year != laster.Year || SeverNow.Month != laster.Month) //如果是不同的一个月份或者年份更新签到数据
        {
            SignInDate.sign_dayDate.Clear();
            SignInDate.CumulativeSignIn = 0;
            for (int i = 0; i < SignInDate.GiftBaglist.Count; i++)
            {
                SignInDate.GiftBaglist[i] = 0;
            }

        }
        if (SignInDate.sign_dayDate.Count != 0)
        {
            sign_day = SignInDate.sign_dayDate;

        }
        //Debug.Log(SignInDate.sign_dayDate.Count);
        //Debug.Log(sign_day.Count);


    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        CumulativeSignIn_txt.text = "累计签到" + SignInDate.CumulativeSignIn.ToString();
        if (SignInDate.IsSignIn == false) //判断是否要自动弹出来
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        showDateSign();
    }
    /// <summary>
    /// 解锁礼包
    /// </summary>
    /// <param name="UnLock"></param>
    public bool OpenGiftBag(int UnLock)
    {
        if (SignInDate.CumulativeSignIn >= UnLock)
        {
            switch (UnLock)
            {
                case 7:
                    SignInDate.GiftBaglist[0] = 1;
                    break;
                case 15:
                    SignInDate.GiftBaglist[1] = 1;
                    break;
                case 30:
                    SignInDate.GiftBaglist[2] = 1;
                    break;

            }

            return true;
        }
        return false;
    }

    public void JudeGiftOpen(ref bool IsOpen, int Unlock)
    {
        switch (Unlock)
        {
            case 7:
                IsOpen = SignInDate.GiftBaglist[0] == 1 ? true : false;
                break;
            case 15:
                IsOpen = SignInDate.GiftBaglist[1] == 1 ? true : false;
                break;
            case 30:
                IsOpen = SignInDate.GiftBaglist[2] == 1 ? true : false;
                break;
        }
    }

    public void showDateSign() //显示那些已经签到了 那些是补签
    {
        for (int i = 0; i < SignInDate.sign_dayDate.Count; i++)
        {
            if (SignInDate.sign_dayDate[i] == 1)
            {

                sign_in.Day_mothlist[i].GetComponent<RawImage>().color = Color.red;
            }
            else if (SignInDate.sign_dayDate[i] == 2)
                sign_in.Day_mothlist[i].GetComponent<RawImage>().color = Color.blue;

        }
        JudeMakeUpSign(Time_signin - 2);
    }
    public void JudeMakeUpSign(int index) //判断是否需要补签
    {
        if (index >= 0 && index < sign_day.Count)
        {
            if (sign_day[index] == 0)
            {
                sign_in.Day_mothlist[index].AddComponent<MakeUpSign>();
                //添加补签代码
            }
        }
        else
        {
            Debug.LogWarning("SignInDate.sign_dayDate没有这个索引的数据");
        }

    }



}
