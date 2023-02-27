using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBg : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    RectTransform rectTransform;
    public float width;
    public float heigh;
    public float moveHeigh;//移动高度
    public float moveWidth;//移动宽度
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<MoveBg>().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.size.x;
        heigh = spriteRenderer.size.y;

        //rectTransform = GetComponent<RectTransform>();
        //width = rectTransform.rect.width;
        //heigh = rectTransform.rect.height;

        moveHeigh = (((heigh - MyCamera.maxY * 2)) / 2);
        moveWidth = (((width - MyCamera.maxX * 2)) / 2);

    }
    private void OnEnable()
    {

    }

    public float time;
    // Update is called once per frame
    void Update()
    {

        time += Time.unscaledDeltaTime;
        transform.Translate(-transform.GetChild(0).transform.position.x * Time.unscaledDeltaTime * 2, -transform.GetChild(0).transform.position.y * Time.unscaledDeltaTime * 2, 0);
        time = 0;
        //Debug.Log(rectTransform.anchoredPosition.x );
        if (transform.localPosition.x > moveWidth || transform.localPosition.x < -moveWidth)
        {
            int dir = transform.localPosition.x > 0 ? 1 : -1;
            transform.localPosition = new Vector3(dir * moveWidth, transform.localPosition.y, 0);
        }
        if (transform.localPosition.y > moveHeigh || transform.localPosition.y < -moveHeigh)
        {
            int dir = transform.localPosition.y > 0 ? 1 : -1;
            transform.localPosition = new Vector3(transform.localPosition.x, dir * moveHeigh, 0);
        }
    }

}
