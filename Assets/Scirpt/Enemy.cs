using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] int deadEnergyBouns = 3;
    [SerializeField] GameObject DieVfx;
    [SerializeField] float Damage;
    public override void Die()
    {
        PlayerEnergy.Instance.Obtain(deadEnergyBouns);
        EnemyManager.Instance.RemoveFromlist(gameObject);
        PoolManager.Release(DieVfx, transform.position);

        base.Die();
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {

            Vector3 location = this.transform.position;
            player.TakeDamage(Damage);
            Vector3 closestPoint = collision.collider.ClosestPoint(location);
            this.gameObject.SetActive(false);
            EnemyManager.Instance.RemoveFromlist(gameObject);
            //  PoolManager.Release(PorjectVfx, closestPoint);
            //播放命中特效预制体
        }
    }
    public GameObject DistanceEnmey(List<GameObject> targetList)
    {
        float distance = 1000;
        int index = -1;
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].transform.localPosition != this.gameObject.transform.position)
            {
                if (distance > (targetList[i].transform.localPosition - this.gameObject.transform.position).magnitude)
                {
                    index = i;

                    distance = (targetList[i].transform.localPosition - this.gameObject.transform.position).magnitude;

                }
            }


        }
        return index != -1 ? targetList[index] : null;
    }
}
