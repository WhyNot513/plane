using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller1 : MonoBehaviour
{
    protected Vector3 targetPosition;
    [SerializeField] protected float PaddingX;
    [SerializeField] protected float PaddingY;
    [SerializeField] protected float SpwanPaddingY;
    [SerializeField] protected Transform firePos;
    protected virtual void OnEnable()
    {
        transform.position = MyCamera.RangSpawnEnemyPos(PaddingX, SpwanPaddingY);
        StartCoroutine(nameof(OnMove));

    }
    public float MoveSpeed;


    protected virtual IEnumerator OnMove()
    {


        while (gameObject.activeSelf)
        {
            if (Vector3.Distance(targetPosition, transform.position) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(2f);
                targetPosition = SwithMoveType();
                yield return null;
            }
            yield return null;
        }
    }
    protected virtual Vector3 SwithMoveType()
    {
        return targetPosition;
    }


}
