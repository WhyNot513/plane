using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public PrePare_panel panel;
    public Text context;
    public Button Sure_btn;
    public RawImage raw;//装备图片

    public List<string> wapome_txt = new List<string>();
    public List<string> arrmor_txt = new List<string>();


    private void Awake()
    {
        Sure_btn.onClick.AddListener(back);
    }
    private void Start()
    {

    }
    int index;
    // Start is called before the first frame update
    private void OnEnable()
    {
        raw.texture = PrePare_panel.weapome ? panel.weaponeList[PrePare_panel.index_weapone] : panel.armorList[PrePare_panel.index_armor];
        context.text = PrePare_panel.weapome ? wapome_txt[PrePare_panel.index_weapone] : arrmor_txt[PrePare_panel.index_armor];
    }

    void back()
    {
        this.gameObject.SetActive(false);
    }
}
