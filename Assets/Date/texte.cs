using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texte : MonoBehaviour
{
    public Text text;
    public static Text t;
    [SerializeField] ReadExcel tObj;
    private void Start()
    {
        t = text;
        Debug.Log(tObj.playerDate.Count);
        t.text = tObj.playerDate[1].MaxHeather.ToString();
    }

}
