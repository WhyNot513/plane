using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] public Vector2 moveDirection;
    [SerializeField] protected float DestoryTime = 1f;//子弹销毁的时间
    public float DefaultDamage;
    public float damage;//伤害 
    protected WaitForSeconds WaitDestoryTime;
    protected GameObject target;
    public GameObject PorjectVfx;
    protected Coroutine Destory;
    Coroutine Move;
    void Awake()
    {

    }



    protected virtual void OnEnable()
    {

        WaitDestoryTime = new WaitForSeconds(DestoryTime);

        Move = StartCoroutine(nameof(MoveProjectile));

        Destory = StartCoroutine(nameof(DestoryBullet));
        gameObject.transform.position = Vector3.zero;
    }
    protected virtual void OnDisable()
    {

        StopCoroutine(Move);
        StopCoroutine(Destory);
        damage = DefaultDamage;

    }
    protected IEnumerator MoveProjectile()
    {

        while (gameObject.activeSelf)
        {
            OnMove();

            yield return null;
        }

    }
    public virtual void OnMove() => transform.Translate(moveSpeed * moveDirection * Time.deltaTime);



    protected virtual IEnumerator DestoryBullet()
    {
        yield return WaitDestoryTime;

        gameObject.SetActive(false);

        gameObject.transform.position = Vector3.zero;


    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {

            if (this.gameObject.activeSelf)
            {
                if (character.GetComponent<Shields>())
                {
                    if (character.GetComponent<Shields>().enabled == false)
                    {
                        Vector3 location = this.transform.position;
                        character.TakeDamage(damage);
                        Vector3 closestPoint = collision.collider.ClosestPoint(location);
                        this.gameObject.SetActive(false);
                        PoolManager.Release(PorjectVfx, closestPoint);
                    }
                }

            }


            //播放命中特效预制体
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Shields>(out Shields character))
        {

            character.takeDameage();

            this.gameObject.SetActive(false);


            //播放命中特效预制体
        }
    }
    protected void SetTartget(GameObject target) => this.target = target;

    protected void SetTartget(List<GameObject> targetList, Transform startPos)
    {
        float distance = 1000;
        int index = -1;
        for (int i = 0; i < targetList.Count; i++)
        {

            if (distance > (targetList[i].transform.localPosition - startPos.position).magnitude)
            {
                index = i;

                distance = (targetList[i].transform.localPosition - startPos.position).magnitude;
            }

        }
        this.target = index != -1 ? targetList[index] : null;
    }


}
