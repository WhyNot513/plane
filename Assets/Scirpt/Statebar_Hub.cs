using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statebar_Hub : Statrbar
{
    [SerializeField] Text PercentText;

    void SetPercentText()
    {
        PercentText.text = " " + Mathf.RoundToInt(TargrtFillAmount * 100f) + "%";
    }
    public override void Initalize(float curentValue, float MaxValue)
    {
        base.Initalize(curentValue, MaxValue);
        SetPercentText();
    }
    protected override IEnumerator BufferedFillCoroutine(Image image)
    {
        SetPercentText();
        return base.BufferedFillCoroutine(image);

    }
}
