using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour
{
    private void a()
    {
        FileStream fs = new FileStream(Application.dataPath + "/Test", FileMode.Open, FileAccess.Read);

        byte[] buffur = new byte[fs.Length];

        fs.Read(buffur, 0, (int)fs.Length);

        fs.Close();
    }
    //将本地文件转为字节

}
