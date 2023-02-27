using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;


public class RegisterPanel : MonoBehaviour
{
    public InputField Input_UserName; //用户民
    public InputField Input_PasswordFirst; //第一次输入密码
    public InputField Input_PasswordSecone; //第二次输入码
    public Button Btn_Register;
    public int MinInput;//最小输入个数
    LoadData loadData;
    public string FirstPassWord; //第一次输入的密码
    public string scecondPassWord;//第二次输入的密码
    public List<Text> InputTips_list = new List<Text>();//输入提示显示是否符合规定
    public int InputIndex;//当前正在输入的输入框
    public List<bool> InputIsMatch_list = new List<bool>();
    private void Awake()
    {
        Input_UserName.onValueChanged.AddListener(AccountRegex);
        Input_PasswordFirst.onEndEdit.AddListener(RegexLevel);
        Input_PasswordSecone.onEndEdit.AddListener(JudePasswordDiff);
        Btn_Register.onClick.AddListener(SusseeRegister);

    }
    private void Start()
    {
        loadData = LoadMananger.Instance.loadData;
        for (int i = 0; i < InputTips_list.Count; i++)
        {
            InputIsMatch_list.Add(false);
        }
    }

    // 账号验证 
    //规则：开头用字母使用数字与字母符号结合的方法组成6-9位的账号数
    public void AccountRegex(string context)
    {
        InputIndex = 0;
        if (context.Length == 0)
        {
            InputTips_list[InputIndex].text = "不能留空";
            InputIsMatch_list[InputIndex] = false;
            return;
        }
        string rule = @"^[a-zA-Z][a-zA-Z\d]{6,9}$";
        Regex re = new Regex(rule);
        bool match = re.IsMatch(context);
        //Debug.Log(re.IsMatch(context));//true
        InputTips_list[InputIndex].text = match ? "正确" : "用字母开头使用数字与字母组成6-9位的账号数";
        InputIsMatch_list[InputIndex] = match;
    }
    public bool IsMatch;

    //密码验证
    public void RegexLevel(string input)
    {
        string[] level = { "复杂", "中等", "简单" };
        int index = -1;
        InputIndex = 1;
        if (input.Length == 0)
        {
            InputTips_list[InputIndex].text = "不能留空";
            InputIsMatch_list[InputIndex] = false;
            return;
        }
        //复杂 必须有数字与字母和符号混合组成的8-15位数(就是剔除其余格式可能，留下目标格式)
        string rule1 = @"^(?!\d{8,15}$)(?![a-zA-Z]{8,15}$)(?![!@#$%^&*.]{8,15}$)(?![!@#$%^&*.\d]{8,15}$)(?![a-zA-Z\d]{8,15}$)(?![a-zA-Z!@#$%^&*.]{8,15}$)([a-zA-Z\d@#$%^&*.]{8,15}$)";
        //中等  由数字和字母或者数字和符号或者字母和符号组成的8-15位数
        string rule2 = @"^(?![a-zA-Z]{8,15}$)(?!\d{8,15}$)(?![!@#$%^&*.]{8,15}$)([a-zA-Z\d]|[a-zA-Z!@#$%^&*.]|[\d!@#$%^&*.]){8,15}$";
        //简单  由单一的数字或者字母或者符号组成的8-15位数
        string rule3 = @"^([a-zA-Z]|[\d{8,15}]|[!@#$%^&*.]){8,15}$";
        Regex r1 = new Regex(rule1);
        Regex r2 = new Regex(rule2);
        Regex r3 = new Regex(rule3);
        if (r1.IsMatch(input))
        {
            index = 0;
        }
        else if (r2.IsMatch(input))
        {
            index = 1;
        }
        else if (r3.IsMatch(input))
        {
            index = 2;
        }
        FirstPassWord = index != -1 ? input : null;
        InputTips_list[InputIndex].text = index != -1 ? level[index] : "请输入8到15为可以包含数字字母和!@#$%^&*.";
        InputIsMatch_list[InputIndex] = index != -1;
    }
    public void JudePasswordDiff(string password)
    {
        InputIndex = 2;
        IsMatch = FirstPassWord == password ? true : false;
        InputTips_list[InputIndex].text = IsMatch ? "正确" : "与上一次不相同";
        InputIsMatch_list[InputIndex] = IsMatch;
    }
    public void ShowPassWord(InputField inputField) //显示密码
    {
        inputField.contentType = inputField.contentType == InputField.ContentType.Standard ? InputField.ContentType.Password : InputField.ContentType.Standard;
        inputField.ForceLabelUpdate();

    }
    public void SusseeRegister() //注册成功
    {
        foreach (var item in InputIsMatch_list)
        {
            if (item == false)
            {
                Debug.Log("信息填写有误");
                return;
            }
        }
        this.gameObject.SetActive(false);
        LoadMananger.Instance.loadData.UserName = Input_UserName.text;
        LoadMananger.Instance.loadData.UserPassWord = Input_PasswordSecone.text;
    }
}
