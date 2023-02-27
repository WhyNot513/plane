using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProject_Aiming : Projectile
{
    private void Awake()
    {
        SetTartget(GameObject.FindGameObjectWithTag("Player"));
    }


    protected override void OnEnable()
    {
        SetTartget(GameObject.FindGameObjectWithTag("Player"));
        if (target == null)
        {
            moveDirection = new Vector2(0, -1);
        }
        StartCoroutine(nameof(MoveDirectionCoroutine));
        base.OnEnable();
    }
    IEnumerator MoveDirectionCoroutine()
    {
        yield return null;
        if (target != null)
        {
            if (target.activeSelf)
            {

                moveDirection = ((target.transform.position - transform.position).normalized);
            }
        }


    }
}
