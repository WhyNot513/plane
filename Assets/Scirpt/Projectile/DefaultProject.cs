using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProject : PlayerProjectile
{
    public List<GameObject> Enemey = new List<GameObject>();
    [SerializeField] int scattered_count;
    float defaultSpeed;//默认飞行速度
    private void Awake()
    {
        defaultSpeed = moveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    protected override void OnEnable()
    {

        moveSpeed = defaultSpeed;
        Grade = UpdateManager.Instance.Grade_Default;
        if (Grade >= 1)
        {
            damage = UpdateDameage;
        }
        base.OnEnable();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy character))
        {


            if (this.gameObject.activeSelf)
            {
                if (Enemey.Count == 0) //避免子弹等级三的时候造成二次伤害
                {
                    Enemey.Add(collision.gameObject);
                }
                else
                {
                    for (int i = 0; i < Enemey.Count; i++)
                    {
                        if (Enemey[i] != collision.gameObject)
                        {
                            Enemey.Add(collision.gameObject);
                        }
                        else
                        {
                            return;

                        }
                    }
                }


                Vector3 location = this.transform.position;
                character.TakeDamage(damage);
                if (character.currentHealth <= 0f)
                {
                    isKill = true;
                    if (Grade >= 3) moveSpeed = 0;


                }
                Vector3 closestPoint = collision.collider.ClosestPoint(location);


                if (!isKill || Grade <= 2)
                {
                    gameObject.SetActive(false);
                }
                PoolManager.Release(PorjectVfx, closestPoint);
            }
            //播放命中特效预制体
        }
        //  PlayerEnergy.Instance.Obtain(PlayerEnergy.PRECRNT);
        if (isKill && Grade >= 2)
        {
            int rotation = 0;
            for (int i = 0; i < scattered_count; i++)
            {
                rotation += 360 / scattered_count;
                PoolManager.Release(UpdateManager.Instance.player.Bullet_list[1], this.transform.localPosition, Quaternion.AngleAxis(rotation, Vector3.forward));
            }

        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Enemey.Clear();
    }
    // Update is called once per frame

}
