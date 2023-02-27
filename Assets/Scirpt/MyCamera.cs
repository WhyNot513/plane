using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour
{

    float devHeight = 9.6f;
    float devWidth = 5.6f;
    public static float minX;
    public static float minY;
    public static float maxX;
    public static float maxY;
    public static float midY;
    // Use this for initialization
    void Awake()
    {

        float screenHeight = Screen.height;

        Debug.LogWarning("screenHeight = " + screenHeight);
        Debug.LogWarning("screenWeight = " + Screen.width * 1.0f);

        //this.GetComponent<Camera>().orthographicSize = screenHeight / 200.0f;

        float orthographicSize = this.GetComponent<Camera>().orthographicSize;
        //  Debug.LogWarning("orthographicSize = " + orthographicSize);

        float aspectRatio = Screen.width * 1.0f / Screen.height;
        // Debug.LogWarning("aspectRatio = " + aspectRatio);
        float cameraWidth = orthographicSize * 2 * aspectRatio;







        if (cameraWidth < devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            // Debug.LogWarning("new orthographicSize = " + orthographicSize);
            //if (orthographicSize > 6.3f)
            //{
            //    orthographicSize = orthographicSize - 1;
            //}
            //this.GetComponent<Camera>().orthographicSize = orthographicSize;
        }
        midY = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f)).y;
        Debug.LogWarning($"midY{midY}");
        Vector2 BottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f));
        minX = BottomLeft.x;
        minY = BottomLeft.y;

        Vector2 TopRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f));
        maxX = TopRight.x;
        maxY = TopRight.y;

        //Debug.Log(minX);
        //Debug.Log(maxX);
        //Debug.Log(maxY);
        //Debug.Log(minY);
    }


    void Update()
    {

    }
    //  Update is called once per frame


    public static Vector3 RangSpawnEnemyPos(float PaddingX, float SpwanPaddingY)
    {
        Vector3 pos = Vector3.zero;
        pos.y = maxY + SpwanPaddingY;
        pos.x = Random.Range(minX + PaddingX, maxX - PaddingX);

        return pos;
    }
    /// <summary>
    /// 敌人在上半部分移动
    /// </summary>
    /// <param name="PaddingX"></param>
    /// <param name="PaddingY"></param>
    /// <returns></returns>
    public static Vector3 RangUpHalfPos(float PaddingX, float PaddingY)
    {

        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minX + PaddingX, maxX - PaddingX);
        pos.y = Random.Range(midY + PaddingY, maxY - PaddingX);

        return pos;
    }
    /// <summary>
    /// 全图移动
    /// </summary>
    /// <param name="PaddingX"></param>
    /// <param name="PaddingY"></param>
    /// <returns></returns>
    public static Vector3 RaneEnemeyHalfPos(float PaddingX, float PaddingY)
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minX + PaddingX, maxX - PaddingX);
        pos.y = Random.Range(minY + PaddingY, maxY - PaddingY);
        //Debug.LogWarning($"minX{minX + PaddingX} .maxX{maxX - PaddingX}");
        //Debug.LogWarning($"midY{minY + PaddingY} .maxY{maxY - PaddingY}");
        return pos;
    }

    /// <summary>
    /// 移动出圈
    /// </summary>
    /// <param name="PaddingX"></param>
    /// <param name="PaddingY"></param>
    /// <returns></returns>
    public static Vector3 RaneEnemeyGoOutPos(float PaddingX, float PaddingY)
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(minX - PaddingX * 2, maxX + PaddingX * 2);
        while (pos.x > minX - PaddingX || pos.x < maxX + PaddingX)
        {
            pos.x = Random.Range(minX - PaddingX * 2, maxX + PaddingX * 2);
            if (pos.x < minX - PaddingX || pos.x > maxX + PaddingX)
            {
                break;
            }
        }
        pos.y = Random.Range(midY, maxY - PaddingY);

        return pos;
    }
}
