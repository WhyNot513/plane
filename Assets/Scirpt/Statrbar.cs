using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statrbar : MonoBehaviour
{
    [SerializeField] Image fillImageBack;
    [SerializeField] Image fillImageFront;
    [SerializeField] bool DelayFill = true;//是否开启缓慢填充
    [SerializeField] float fillDelay = 0.5f;
    [SerializeField] float fillSpeed = 01f;

    float currentFillAmount;//当期的填充值
    protected float TargrtFillAmount;//目标填充值
    float t;

    WaitForSeconds WaitForDelayFill;
    Coroutine BufferedFillingCoroutine;
    Canvas canvas;

    private void Awake()
    {

        canvas = GetComponent<Canvas>();
        WaitForDelayFill = new WaitForSeconds(fillDelay);
    }
    public virtual void Initalize(float curentValue, float MaxValue)
    {
        currentFillAmount = curentValue / MaxValue;
        TargrtFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = TargrtFillAmount;
    }

    public void UpdateStats(float currentValue, float maxValue)
    {
        TargrtFillAmount = currentValue / maxValue;
        if (BufferedFillingCoroutine != null)
        {
            StopCoroutine(BufferedFillingCoroutine);
        }
        if (currentFillAmount > TargrtFillAmount)//当前状态值减少
        {
            fillImageFront.fillAmount = TargrtFillAmount; //后面的图片慢慢减少
            BufferedFillingCoroutine = StartCoroutine(BufferedFillCoroutine(fillImageBack));
        }
        if (currentFillAmount < TargrtFillAmount)//当前状态值增加
        {
            fillImageBack.fillAmount = TargrtFillAmount;//前面的图片慢慢增加
            BufferedFillingCoroutine = StartCoroutine(BufferedFillCoroutine(fillImageFront));
        }
    }
    protected virtual IEnumerator BufferedFillCoroutine(Image image)
    {
        if (DelayFill)
        {
            yield return WaitForDelayFill;
        }
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(currentFillAmount, TargrtFillAmount, t);
            image.fillAmount = currentFillAmount;
            yield return null;
        }
    }
}
