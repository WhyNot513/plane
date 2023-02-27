using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectileOverdrive : PlayerProjectile
{
    [SerializeField] ProjectileGuidanceSystems ProjectileGuidanceSystems;
    [SerializeField] float Grade3_damage;
    GameObject Grade3vfx;
    protected override void OnEnable()
    {
        Grade = UpdateManager.Instance.Grade_Track;
        if (Grade >= 1) //等级1
        {
            damage = UpdateDameage;
            DefaultDamage = UpdateDameage;
        }

        base.OnEnable();
        transform.rotation = Quaternion.identity;
        SetTartget(EnemyManager.Instance.enemies, GameManager.Instance.Player.transform);
        if (target == null)
        {
            base.OnEnable();
        }
        else
        {
            StartCoroutine(ProjectileGuidanceSystems.HomingCoroutine(target));
        }

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy character))
        {

            if (this.gameObject.activeSelf)
            {
                Vector3 location = this.transform.position;
                character.TakeDamage(damage);
                if (character.currentHealth <= 0f)
                {
                    isKill = true;

                }
                if (Grade >= 3)
                {
                    if (EnemyManager.Instance != null)
                    {
                        if (character.DistanceEnmey(EnemyManager.Instance.enemies) != null)
                        {
                            Grade3vfx = PoolManager.Release(UpdateManager.Instance.Grade_Track3Vfx);
                            character.DistanceEnmey(EnemyManager.Instance.enemies).GetComponent<Character>().TakeDamage(Grade3_damage);
                            Grade3vfx.GetComponent<lighting>().GetAngle(character.gameObject, character.DistanceEnmey(EnemyManager.Instance.enemies));
                            //Debug.Log(character.DistanceEnmey(EnemyManager.Instance.enemies).name);
                        }
                    }
                }

                Vector3 closestPoint = collision.collider.ClosestPoint(location);
                this.gameObject.SetActive(false);
                PoolManager.Release(PorjectVfx, closestPoint);
            }
            //播放命中特效预制体
        }
        //PlayerEnergy.Instance.Obtain(PlayerEnergy.PRECRNT);
    }

}
