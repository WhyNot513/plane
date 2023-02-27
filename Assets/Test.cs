using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    LineRenderer lineRenderer;
    string EnemyName = null;
    float time;//时间差
    [SerializeField] float TimeBetween; //扣除伤害时间差
    float lasterTime;//上一次的时间
    [SerializeField] float DamagePercent;

    [SerializeField] float defaultDamage;//初始伤害
    [SerializeField] float UpdateDamage;
    [SerializeField, Range(0, 1)] float deceleratePrecent;
    Vector3 hitPos;//碰撞点；
    public int grade;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        grade = UpdateManager.Instance.Grade_Laser;
        if (grade >= 1)
        {
            defaultDamage = UpdateDamage;
        }
        if (grade >= 2)
        {

        }
    }




    Ray2D ray;

    void Update()
    {
        ray = new Ray2D(transform.position, Vector2.right);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("Enemy"))
            {
                Debug.LogWarning("检测到敌人");
            }
            else
            {
                Debug.Log("检测到其他对象");
            }
        }
        else
        {
            Debug.Log("没有碰撞任何对象");
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);//起点，方向，颜色（可选）

    }

    // Update is called once per frame
    //void Update()
    //    {
    //        time = Time.unscaledTime - lasterTime;
    //        // lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.time * 1 + 2, 0f));
    //        if (hitPos != Vector3.zero)
    //        {
    //            lineRenderer.SetPosition(1, new Vector3(0, Mathf.Abs(transform.position.y - hitPos.y), 0));
    //        }

    //    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (EnemyName == null)
        {
            EnemyName = other.name;
            hitPos = other.bounds.ClosestPoint(transform.position);
        }



        // print("碰撞点" + hitPos);
        // Debug.LogWarning(other.name);
        //  lineRenderer.SetPosition(1, new Vector3(0, Mathf.Abs(transform.position.y - hitPos.y), 0));

        if (EnemyName == other.name && time > TimeBetween)
        {
            if (other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().MoveSpeed = deceleratePrecent;
            }

            if (other.gameObject.TryGetComponent<Enemy>(out Enemy character)) //伤害随着时间逐渐增加
            {

                // Vector3 location = this.transform.position;
                character.TakeDamage(defaultDamage * DamagePercent);

                lasterTime = Time.unscaledTime;

                //播放命中特效预制体
            }
        }
    }
    // 碰撞体的检测 

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>() != null)
        {
            collision.GetComponent<EnemyController>().MoveSpeed = collision.GetComponent<EnemyController>().defaultSpeed;
        }
        EnemyName = null;
        lineRenderer.SetPosition(1, new Vector3(0, 4, 0));
        hitPos = Vector3.zero;
    }
}
