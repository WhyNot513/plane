using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class level : MonoBehaviour, IPointerDownHandler
{
    public Text txt_level;
    public int Level;
    private void OnEnable()
    {
        Init();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        levelMessage.Instance.levelNumber = Level;
    }
    public void Init()
    {
        txt_level.text = Level.ToString();
    }


}
