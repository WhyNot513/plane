using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGuidanceSystems : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] float minBallisticAngle = 50f; //最小弹道角度
    [SerializeField] float maxBallisticAngle = 75f;//最大弹道角度
    Vector3 targetDirection;
    public IEnumerator HomingCoroutine(GameObject target)
    {
        while (gameObject.activeSelf)
        {
            if (target.activeSelf)
            {

                targetDirection = target.transform.position - transform.position;
                var angle = Mathf.Atan2(-targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                // transform.rotation *= Quaternion.Euler(0, 0, Random.Range(minBallisticAngle, maxBallisticAngle));
                projectile.OnMove();

            }
            else
            {
                projectile.OnMove();
            }
            yield return null;
        }
    }
}
