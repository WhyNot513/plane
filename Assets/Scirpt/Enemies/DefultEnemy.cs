using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefultEnemy : Enemycontroller1
{
    [SerializeField] Vector2 moveDirection;
    [SerializeField] float TimeFire;
    List<Vector3> pos = new List<Vector3>();

    protected override void OnEnable()
    {
        pos.Clear();
        base.OnEnable();
    }
    protected override IEnumerator OnMove()
    {

        while (Mathf.Abs(transform.localPosition.y) > MyCamera.maxY / 2)
        {
            this.transform.Translate(MoveSpeed * moveDirection * Time.unscaledDeltaTime); ;
            yield return null;
        }
        yield return StartCoroutine(nameof(OnFire));

        //if (Mathf.Abs(transform.localPosition.y) > MyCamera.maxY + offest)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    protected override Vector3 SwithMoveType()
    {
        targetPosition = MyCamera.RaneEnemeyGoOutPos(PaddingX, PaddingY);
        return targetPosition;
    }
    public GameObject b;

    IEnumerator OnFire()
    {

        GameObject a = PoolManager.Release(GameManager.Instance.EnemeyProjecte[2], firePos.position);
        while (a.activeSelf)
        {

            pos.Add(a.transform.localPosition);
            yield return new WaitForSeconds(0.07f);
        }
        Debug.LogWarning(pos.Count);
        for (int i = 0; i < pos.Count; i++)
        {

            GameObject c = PoolManager.Release(GameManager.Instance.EnemeyProjecte[1]);
            c.transform.localPosition = pos[i] + new Vector3(Random.Range(0.5f, -0.5f), Random.Range(0f, -0.5f), 0);

        }
        yield return null;
        while (Mathf.Abs(transform.localPosition.y) < MyCamera.maxY)
        {
            this.transform.Translate(MoveSpeed * -moveDirection * Time.unscaledDeltaTime); ;
            yield return null;
        }
        this.gameObject.SetActive(false);
        EnemyManager.Instance.RemoveFromlist(this.gameObject);

        yield return null;

    }

}
