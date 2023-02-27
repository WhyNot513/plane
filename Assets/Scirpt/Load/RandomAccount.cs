using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RandomAccount : MonoBehaviour
{
    string UserName;
    public int count;
    public int PassCount;
    public Button btn_save;

    public Text txt_UserName;
    public Text txt_Password;
    public Camera Camera1;
    private void Awake()
    {
        btn_save.onClick.AddListener(() => CameraCapture(Camera1, new Rect(Screen.width * 0f, Screen.height * 0f, Screen.width * 1f, Screen.height * 1f)));
    }
    private void OnEnable()
    {
        txt_UserName.text = Name();
        txt_Password.text = PassWord();
    }
    string Name()
    {
        count = UnityEngine.Random.Range(6, 9);
        for (int i = 0; i < count; i++)
        {
            //System.Random random = new System.Random();
            if (i < count / 2)
                UserName += randomAZaz();
            else
                UserName += UnityEngine.Random.Range(0, 10).ToString();
        }
        return UserName;

    }
    public string randomAZaz()//随机字母包括大小写
    {
        int a = UnityEngine.Random.Range(0, 2);
        int text = a == 0 ? UnityEngine.Random.Range(65, 91) : UnityEngine.Random.Range(97, 123);
        return Convert.ToChar(text).ToString();
    }
    public string PassWord()
    {
        PassCount = UnityEngine.Random.Range(8, 15);
        string PassWord = "";
        for (int i = 0; i < PassCount; i++)
        {
            if (i > (PassCount - 1 - 1))
                PassWord += Randomfuhao();
            else
                PassWord += UnityEngine.Random.Range(0, 2) == 1 ? randomAZaz() : UnityEngine.Random.Range(0, 9).ToString();
        }
        int a = UnityEngine.Random.Range(0, 9);
        Debug.Log(PassWord);
        return PassWord;
    }
    public string Randomfuhao()
    {
        string[] fuhao = { "!", "@", "#", "$", "%", "^", "&", "*", "." };
        return fuhao[UnityEngine.Random.Range(0, fuhao.Length)];
    }


    /// <summary>    
    /// Captures the screenshot2.    
    /// </summary>    
    /// <returns>The screenshot2.</returns>    
    /// <param name="rect">Rect.截图的区域，左下角为o点</param>  
    /// 
    Texture2D CaptureScreenshot2(Rect rect)
    {
        //先创建一个的空纹理，大小可根据实现需要来设置
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        // 读取屏幕像素信息并存储为纹理数据，    
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        // 然后将这些纹理数据，成一个png图片文件    
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张图片: ｛0｝", filename));
        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。    
        return screenShot;
    }


    /// <summary>
    /// 对相机拍摄区域进行截图，如果需要多个相机，可类比添加，可截取多个相机的叠加画面
    /// </summary>
    /// <param name="camera">待截图的相机</param>
    /// <param name="width">截取的图片宽度</param>
    /// <param name="height">截取的图片高度</param>
    /// <param name="fileName">文件名</param>
    /// <returns>返回Texture2D对象</returns>
    public Texture2D CameraCapture(Camera camera, Rect rect)
    {
        RenderTexture render = new RenderTexture((int)rect.width, (int)rect.height, -1);//创建一个RenderTexture对象 

        camera.gameObject.SetActive(true);//启用截图相机
        camera.targetTexture = render;//设置截图相机的targetTexture为render
        camera.Render();//手动开启截图相机的渲染

        RenderTexture.active = render;//激活RenderTexture
        Texture2D tex = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, false);//新建一个Texture2D对象
        tex.ReadPixels(rect, 0, 0);//读取像素
        tex.Apply();//保存像素信息

        camera.targetTexture = null;//重置截图相机的targetTexture
        RenderTexture.active = null;//关闭RenderTexture的激活状态
        UnityEngine.Object.Destroy(render);//删除RenderTexture对象
        SaveTex(tex, "TxdtGame", $"TxdtGame{Time.time}");


#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//刷新Unity的资产目录
#endif

        return tex;//返回Texture2D对象，方便游戏内展示和使用
    }
    public static NativeGallery.Permission SaveTex(Texture2D tex, string galleryName, string fileName)
    {
        NativeGallery.Permission result = NativeGallery.SaveImageToGallery(tex, galleryName, fileName);
        Debug.Log("Permission result: " + result);
        return result;
    }



}
