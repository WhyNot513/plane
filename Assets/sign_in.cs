using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sign_in : MonoBehaviour
{
    public GameObject days;
    public GameObject week;
    Transform father;
    GridLayoutGroup gridLayout;
    public float width;
    public float height;
    public int WeightCount;
    public int HeightCount;
    List<GameObject> daylist = new List<GameObject>(); //所有的展示日期的格子
    public static List<GameObject> Day_mothlist = new List<GameObject>(); //当月需要显示的日期的格子
    private void Awake()
    {
        father = this.transform;
        width = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y;
        gridLayout = GetComponent<GridLayoutGroup>();



        gridLayout.padding.left = gridLayout.padding.right = (int)(((width - (WeightCount * gridLayout.cellSize.x) - ((WeightCount - 1) * gridLayout.spacing.x)) / 2));

    }
    private void Start()
    {
        for (int i = 0; i < 48; i++)
        {
            if (i < 7)
            {
                GameObject obj = Instantiate(week, father);
                obj.GetComponentInChildren<Text>().text = JudeWeek(i);
            }
            else
                daylist.Add(Instantiate(days, father));
        }
        JudeDays();
    }
    public string JudeWeek(int i)
    {
        string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        return Day[i];
    }

    public void JudeDays()
    {
        // 根据当前服务器时间判定：
        // string weekstr = DateTime.Now.DayOfWeek.ToString();
        int year = sign_inManage.SeverNow.Year;
        int month = sign_inManage.SeverNow.Month;
        int first_day = whatDay(year, month);
        Debug.Log(first_day);
        int days = 1;
        for (int i = 0; i < daylist.Count; i++)
        {
            if (i >= first_day && i < GetDay1() + first_day)
            {
                daylist[i].GetComponentInChildren<Text>().text = days.ToString();
                Day_mothlist.Add(daylist[i]);
                if (sign_inManage.Instance.SignInDate.sign_dayDate.Count == 0)
                    sign_inManage.Instance.sign_day.Add(0);
                days++;
            }
            else
            {
                Color color = daylist[i].GetComponent<RawImage>().color;
                daylist[i].GetComponent<RawImage>().color = new Color(color.r, color.g, color.a, 0);
            }

        }
        //Debug.Log(GetDay1());




    }
    //得到当月有多少天
    public int GetDay1()
    {
        DateTime dt = sign_inManage.SeverNow;
        int day = DateTime.DaysInMonth(dt.Year, dt.Month);
        return day;

    }


    #region  //根据年份 和月份判断每个月的第一天是星期几
    bool leapYear(int y)
    {
        if (y % 4 == 0 && y % 100 != 0 || y % 400 == 0) return true;
        return false;
    }

    int whatDay(int year, int month)
    {
        int dyear = 0, nd = 0, w, lyear = 0;
        if (year == 1) nd = 0;
        else
        {
            for (int i = 1; i < year; i++)
            {
                if (leapYear(i)) lyear += 1;
                else dyear += 1;
            }
            nd = dyear * 365 + lyear * 366;
        }
        if (month == 1) nd += 1;
        else
        {
            List<int> monthl = new List<int> { 1, 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            for (int j = 0; j <= month - 1; j++)
            {
                nd += monthl[j];
            }
            if (leapYear(year) && month >= 3) nd += 29;
            if (leapYear(year) == true && month >= 3) nd += 28;
        }
        w = 0;
        w += nd;
        w = w % 7;
        if (w == 0) w = 7;
        return w;


    }
    #endregion
}

