using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingman : MonoBehaviour
{
    public Transform firePos;
    public GameObject Project;
    public float firetime;
    public GameObject Father;
    public Vector2 moveDirection;//移动的方向
    public float GradientTime;//渐变的时间
    WaitForSeconds Grandien_Cor;
    float time; SpriteRenderer fade;
    private void Awake()
    {
        fade = GetComponent<SpriteRenderer>();
        Grandien_Cor = new WaitForSeconds(GradientTime);
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(Fade));
        gameObject.transform.localPosition = Father.transform.localPosition;
    }
    private void OnDisable()
    {
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.r, 0);
    }
    private void Update()
    {
        //先移动到坐标的位置然后再开火
        time += Time.unscaledDeltaTime;
        if (time >= firetime)
        {
            //开火
            PoolManager.Release(Project, firePos.position);
            time = 0;
        }
    }
    private void Move()
    {
        this.transform.Translate(1f * moveDirection * Time.unscaledDeltaTime);
    }


    IEnumerator Fade()

    {
        Color color = fade.color;


        while (color.a <= 1)
        {
            color.a += 0.01f;
            fade.color = color;
            this.transform.Translate(1f * moveDirection * Time.unscaledDeltaTime);
            yield return Grandien_Cor;

        }

    }


}
