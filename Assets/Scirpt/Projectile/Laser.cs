using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject Enemy;
    Enemy enemy;
    EnemyController controller;
    [SerializeField] int Grade;
    //......攻击。。。//
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float lenghth;
    [SerializeField] float damage;
    float DefaultDamage;//默认伤害
    [SerializeField] float UpdateDamage;//升级后的伤害
    [SerializeField] float decelerate;//等级二对敌人飞机的减速
    [SerializeField] float AddDamageTime;
    //..........time.....//

    float time;
    float LastFireTime;
    [SerializeField] float fireTime;
    void Start()
    {

    }
    private void OnEnable()
    {
        Grade = UpdateManager.Instance.Grade_Laser;
        if (Grade >= 1)
        {
            damage = UpdateDamage;
        }
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    Ray2D ray;

    void Update()
    {
        Debug.Log(transform.gameObject.name);
        ray = new Ray2D(transform.position, Vector2.up);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, lenghth, enemyMask);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("Enemy"))
            {


                if (Enemy != info.transform.gameObject)
                {
                    Debug.Log("查找物体");
                    Enemy = info.transform.gameObject;
                    enemy = info.transform.gameObject.GetComponent<Enemy>();
                    if (controller != null)
                        controller.MoveSpeed = controller.defaultSpeed;
                    controller = info.transform.gameObject.GetComponent<EnemyController>();
                    if (Grade>=2)
                        controller.MoveSpeed = controller.MoveSpeed * decelerate;
                    AddDamageTime = 0;
                }
                else
                {
                    AddDamageTime += Time.deltaTime;
                }

                time = Time.unscaledTime - LastFireTime;

                if (time > fireTime)
                {

                    if (AddDamageTime > 3f&&Grade>=3)
                    {
                        enemy.TakeDamage(damage + UpdateDamage);
                    }
                    else
                    {
                        enemy.TakeDamage(damage);
                    }

                    LastFireTime = Time.unscaledTime;
                    time = 0;
                }
                lineRenderer.SetPosition(1, new Vector3(0, info.distance, 0));



            }

        }
        else
        {
            if (controller != null)
                controller.MoveSpeed = controller.defaultSpeed;
            lineRenderer.SetPosition(1, new Vector3(0, lenghth, 0));
            //   Enemy = info;
            Debug.Log("没有碰撞任何对象");
        }

        Debug.DrawRay(ray.origin, ray.direction * lenghth, Color.blue);//起点，方向，颜色（可选）

    }
}
