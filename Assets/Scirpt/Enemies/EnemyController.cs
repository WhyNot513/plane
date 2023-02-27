using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("-----Move-----")]
    [SerializeField] protected float PaddingX;
    [SerializeField] float PaddingY;
    [SerializeField] float SpwanPaddingY;
    public float MoveSpeed = 1f;
    public float defaultSpeed;
    [SerializeField] int MoveType = 0;

    [Header("-----Fire-----")]
    [SerializeField] protected float minFireInterval;//最小开伙速度
    [SerializeField] protected float maxFireInterval;//最大开伙速度
    [SerializeField] protected GameObject[] projectiles;
    [SerializeField] protected Transform muzzle;
    [SerializeField] int SwitchFireSype;
    public static bool OnFire;
    protected Vector3 targetPosition;
    int MoveCount;//移动的次数

    protected Coroutine fireCoroutine;//开火携程

    private void Awake()
    {
        defaultSpeed = MoveSpeed;
    }
    protected virtual void OnEnable()
    {
        MoveSpeed = defaultSpeed;
        StartCoroutine(nameof(RangMoveEnemt_Cor));
        MoveCount = 0;
        if (projectiles.Length >= 0)
            fireCoroutine = StartCoroutine(nameof(RandomlyFireCoroutine));
    }
    IEnumerator RangMoveEnemt_Cor()
    {
        //yield return new WaitForSeconds(1);
        float time = 0;
        transform.position = MyCamera.RangSpawnEnemyPos(PaddingX, SpwanPaddingY);
        targetPosition = SwithMoveType(MoveType);
        Vector3 control = new Vector3((targetPosition.x - transform.position.x) / 2, (targetPosition.y - transform.position.y) / 2 + 0.5f, 0);
        Debug.Log($" transform.position{transform.position}------targetPosition{targetPosition}==============control{control}--------{this.gameObject.name}");
        while (gameObject.activeSelf)
        {
            //    Debug.LogWarning($" transform.position{transform.position}------targetPosition{targetPosition}--------{this.gameObject.name}");
            //    Debug.LogWarning($"距离{Vector3.Distance(targetPosition, transform.position)}==========Mathf.Epsilon{Mathf.Epsilon}--------{this.gameObject.name}");
            if (Vector3.Distance(targetPosition, transform.position) > Mathf.Epsilon)
            {
                //Vector3.Distance(targetPosition, transform.position) > Mathf.Epsilon
                transform.position = (Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime));
                //time += Time.deltaTime * MoveSpeed;
                //transform.Translate(MoveSpeed * new Vector2(0, -1) * Time.deltaTime);
                //  transform.Translate(MoveSpeed * new Vector2(-1, 0) * Time.deltaTime * Mathf.Sin(time));
                // transform.position = BezierCurve.QuadraticPoint(transform.position, Vector3.zero, targetPosition, MoveSpeed * Time.deltaTime);
                //  Debug.Log($" transform.position{transform.position}------targetPosition{targetPosition}--------{this.gameObject.name}");
                yield return null;
            }
            else
            {

                MoveCount++;
                targetPosition = SwithMoveType(MoveType);
                control = new Vector3((targetPosition.x - transform.position.x) / 2, (targetPosition.y - transform.position.y) / 2 + 0.5f, 0);
                Debug.Log($"{control}*************{this.gameObject.name}");

                yield return null;
            }
            yield return null;
        }
    }
    private void Update()
    {
        if (fireCoroutine != null)
        {
            if (transform.position.x > MyCamera.maxX || transform.position.x < MyCamera.minX)
            {
                Debug.Log("出界了");
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
        }
        else
        {
            Debug.Log("准备了");
            if (transform.position.x <= MyCamera.maxX && transform.position.x >= MyCamera.minX)
            {
                Debug.Log("开火了");
                fireCoroutine = StartCoroutine(nameof(RandomlyFireCoroutine));
            }
        }
    }

    Vector3 SwithMoveType(int MoveType)
    {
        Vector3 targetPosition = new Vector3();
        switch (MoveType)
        {
            case 0:
                targetPosition = MyCamera.RangUpHalfPos(PaddingX, PaddingY);

                break;
            case 1:
                targetPosition = MyCamera.RaneEnemeyHalfPos(PaddingX, PaddingY);
                break;
            case 2:
                if (MoveCount == 2)
                {
                    //  this.gameObject.SetActive(false);
                }
                if (MoveCount == 0)
                {
                    targetPosition = MyCamera.RangUpHalfPos(PaddingX, PaddingY);
                }
                else
                {
                    targetPosition = MyCamera.RaneEnemeyGoOutPos(PaddingX, PaddingY);
                }

                break;
        }
        return targetPosition;
    }
    protected virtual IEnumerator RandomlyFireCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));
            if (GameManager.GameState == GameState.GameOver) yield break;
            foreach (var item in projectiles)
            {
                PoolManager.Release(item, muzzle.position);
            }
        }
    }

}
