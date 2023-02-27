using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponeUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    PrePare_panel prePare_Panel;
    public Button LeftButton;
    public Button rightbutton;
    public Button aggrandizement;
    public Text Name;
    private void Awake()
    {
        LeftButton.onClick.AddListener(left);
        rightbutton.onClick.AddListener(right);
        aggrandizement.onClick.AddListener(() => UpdateManager.Instance.UpdateBullet(PrePare_panel.index_weapone));
    }
    private void Start()
    {
        prePare_Panel = mainPanel.Prepare_panel.GetComponent<PrePare_panel>();
    }
    void left()

    {
        PrePare_panel.index_weapone = PrePare_panel.index_weapone - 1 < 0 ? 0 : PrePare_panel.index_weapone - 1;

        Name.text = mainPanel.WeaponeName[PrePare_panel.index_weapone];

    }
    void right()
    {
        PrePare_panel.index_weapone = PrePare_panel.index_weapone + 1 >= prePare_Panel.weaponeList.Count ? PrePare_panel.index_weapone : PrePare_panel.index_weapone + 1;
        Name.text = mainPanel.WeaponeName[PrePare_panel.index_weapone];
    }




}
