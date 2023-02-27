using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = ("date/Character"), fileName = ("characterStateDate_"))]
//[System.Serializable]
public class ReadExcel : MonoBehaviour
{
    public TextAsset plane;
    public List<CharacterDate> playerDate = new List<CharacterDate>();
    private void Awake()
    {
        Read();
    }
    private void OnValidate()
    {

    }
    public void Read()
    {

        if (!plane) return;
        string[] date = plane.text.Split('\n'); //将每一行的数据分隔开
        playerDate.Clear();
        Debug.Log(date);
        for (int j = 0; j < date.Length; j++)
        {
            Debug.Log(date[j]); ;
        }
        for (int i = 1; i < date.Length - 1; i++)
        {
            string[] text = date[i].Split(',');//将每行中的数据分隔开

            CharacterDate player = new CharacterDate();
            player.MaxHeather = int.Parse(text[0]);

            string a = (text[1]);


            playerDate.Add(player);
        }


    }

}
