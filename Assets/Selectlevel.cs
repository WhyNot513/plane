using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectlevel : MonoBehaviour
{
    public Button Btn_rightChapter;
    public Button Btn_leftChapter;
    public Button Btn_sure;
    public Button Btn_GameStart;
    public RawImage Chapter_Bg;
    public List<Texture2D> Chapter_texturelist = new List<Texture2D>();

    private void Awake()
    {
        Btn_rightChapter.onClick.AddListener(() => mainPanel.Right(ref levelMessage.Instance.chapterMessage, Chapter_Bg, Chapter_texturelist));
        Btn_leftChapter.onClick.AddListener(() => mainPanel.left(ref levelMessage.Instance.chapterMessage, Chapter_Bg, Chapter_texturelist));
        Btn_sure.onClick.AddListener(selectChapter);
        Btn_GameStart.onClick.AddListener(() => GameManager.Instance.GameStart(this.gameObject));
    }
    private void OnEnable()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    void selectChapter()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }


}
