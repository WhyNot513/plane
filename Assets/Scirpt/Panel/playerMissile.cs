using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追踪导弹运行先速度慢 然后速度块 但是是范围爆炸
/// </summary>
public class playerMissile : PlayerProjectile
{
    [SerializeField] GameObject VfxDestory;
    [SerializeField] float lowSpeed = 8f;
    [SerializeField] float highSpeed = 25f;
    [SerializeField] float variableSpeedDelay = 05f;//变速延迟时间
    [SerializeField] float explosionRadius = 3f;//爆炸半径
    [SerializeField] LayerMask enemyMask;
    WaitForSeconds waitvariableSpeedDelay;
    private void Awake()
    {
        waitvariableSpeedDelay = new WaitForSeconds(variableSpeedDelay);
    }
    protected override void OnEnable()
    {
        Grade = UpdateManager.Instance.Grade_Missile;
        transform.rotation = Quaternion.identity;
        if (Grade >= 1) //等级1
        {
            damage = UpdateDameage;
            DefaultDamage = UpdateDameage;
        }
        if (Grade >= 2) //等级2
        {
            explosionRadius *= 2;
        }
        base.OnEnable();
        StartCoroutine(nameof(VariableSpeedCoroutine));
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        foreach (var item in Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask))
        {
            if (item.TryGetComponent<Enemy>(out Enemy enemy))
            {

                if (enemy.name != collision.gameObject.name) //判断当前造成伤害的物体避免造成二次伤害
                {
                    if (enemy.GetComponent<Enemy_shields>().enabled == false) //护盾开启的时候
                    {
                        if (isKill && Grade >= 3) //挡子弹等级为三的时候才会开启这个能力
                        {
                            enemy.TakeDamage(damage * 2);
                        }
                        else
                        {
                            enemy.TakeDamage(damage);
                        }

                    }
                    else
                    {
                        enemy.GetComponent<Enemy_shields>().takeDameage();
                    }
                }
            }
        }
    }
    IEnumerator VariableSpeedCoroutine() //导弹速度变化
    {
        moveSpeed = lowSpeed;
        yield return waitvariableSpeedDelay;
        moveSpeed = highSpeed;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    protected override IEnumerator DestoryBullet() //飞行时间到了后自己爆炸
    {

        yield return WaitDestoryTime;
        foreach (var item in Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask))
        {
            if (item.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (enemy.GetComponent<Enemy_shields>().enabled == false)
                {
                    enemy.TakeDamage(damage);
                }
                else
                {
                    enemy.GetComponent<Enemy_shields>().takeDameage();
                }

            }
        }
        gameObject.SetActive(false);
        PoolManager.Release(VfxDestory, this.transform.position);
        gameObject.transform.position = Vector3.zero;
        StopCoroutine(nameof(MoveProjectile));

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        explosionRadius = 0.81f;
    }
}
