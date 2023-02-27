using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour
{



    public static GameObject shandian;
    static Vector2 sizeBox;
    [SerializeField] float time = 0.5f;
    // Use this for initialization
    void Start()
    {


        shandian = this.gameObject;
        sizeBox = GetComponent<SpriteRenderer>().size;
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(destory));
    }
    // Update is called once per frame
    void Update()
    {



    }

    public void GetAngle(GameObject Cylinder1, GameObject Cylinder2)
    {
        ////点乘

        //float dot = Vector3.Dot(Cylinder1.transform.right, -Cylinder2.transform.up);
        ////弧度转角度
        //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        ////叉乘求法线
        //Vector3 dir = Vector3.Cross(Cylinder1.transform.right, -Cylinder2.transform.up);

        //angle = (Vector3.Dot(Cylinder2.transform.forward, dir) < 0 ? -1 : 1) * angle;
        //Debug.Log("夹角为：" + angle);

        //Vector3 origin = new Vector3(Cylinder2.transform.localPosition.x, Cylinder1.transform.localPosition.y, 0);
        //origin = c1.transform.position;


        //Vector3 Cylinder1Dir = (Cylinder1.transform.localPosition - origin).normalized;
        //Vector3 Cylinder2Dir = (Cylinder2.transform.localPosition - origin).normalized;


        //float dot = Vector3.Dot(Cylinder1Dir, Cylinder2Dir);
        //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //   Debug.Log("夹角为：" + angle);
        //物体一到物体二的方向
        Vector3 dir = (Cylinder2.transform.localPosition - Cylinder1.transform.localPosition).normalized;
        float angle = dir.y < 0 ? -Vector3.Angle(Cylinder1.transform.right, dir) : Vector3.Angle(Cylinder1.transform.right, dir);
        float distance2 = Vector3.Distance(Cylinder2.transform.localPosition, Cylinder1.transform.position);
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        int scalePrecent = (int)(distance2 / sizeBox.y);
        this.gameObject.transform.localPosition = Cylinder1.transform.localPosition;
        this.gameObject.transform.localScale = new Vector3(2, scalePrecent, 2);


        // Vector3.Angle(Cylinder1.transform.localPosition, -Cylinder2.transform.localPosition);
    }
    IEnumerator destory()
    {

        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}

