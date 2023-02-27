using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    bool IsUpdate;

    public int Grade;
    public bool isKill;
    public float UpdateDameage;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        DefaultDamage = damage;
        damage += GameManager.Instance.player_damage;

        this.transform.rotation = Quaternion.identity;

        isKill = false;
        if (MusrKillManager.Instance.IsAddDamage && !IsUpdate)
        {

            damage *= MusrKillManager.Instance.DamagePrecent;
            IsUpdate = true;
        }
        else if (!MusrKillManager.Instance.IsAddDamage && IsUpdate)
        {
            if (DefaultDamage != damage)
            {
                damage /= MusrKillManager.Instance.DamagePrecent;
            }

        }

        base.OnEnable();
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
                Vector3 closestPoint = collision.collider.ClosestPoint(location);
                this.gameObject.SetActive(false);
                PoolManager.Release(PorjectVfx, closestPoint);
            }
            //播放命中特效预制体
        }
        // PlayerEnergy.Instance.Obtain(PlayerEnergy.PRECRNT);
    }
    protected virtual void Update()
    {
        OnMove();


    }

    public override void OnMove()
    {

        transform.Translate(moveSpeed * moveDirection * Time.unscaledDeltaTime);
    }

    protected override void OnDisable()
    {

        base.OnDisable();
    }
}
