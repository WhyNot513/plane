using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    float continuousFireDuration = 1.5f;
    WaitForSeconds waitForContinuousFireInterval;
    WaitForSeconds waitForFireInterval;
    public List<GameObject> magazine;
    [SerializeField] Transform playerDectionTransfrom;
    [SerializeField] Vector2 PlayerDetectionsize;
    [SerializeField] LayerMask playerLayermask;
    [SerializeField] int Launch_type;

    Transform playerTransform;
    protected void Awake()
    {
        waitForContinuousFireInterval = new WaitForSeconds(minFireInterval);
        waitForFireInterval = new WaitForSeconds(maxFireInterval);
        magazine = new List<GameObject>(projectiles.Length);

    }

    protected override void OnEnable()
    {
        //  playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//用于boss追击敌人
        base.OnEnable();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerDectionTransfrom.position, PlayerDetectionsize);
    }
    void loadMagazine() //boos敌人随机开火
    {

        if (Physics2D.OverlapBox(playerDectionTransfrom.position, PlayerDetectionsize, 0f, playerLayermask))
        {
            Launch_type = 0;
        }
        else
        {
            Launch_type = Random.Range(2, 3);
        }
    }
    IEnumerator ContinuousFireCoroutinw()
    {
        loadMagazine();

        float continuousFireTimer = 0;


        int rotation;
        //yield return waitForContinuousFireInterval;
        while (continuousFireTimer < continuousFireDuration)
        {

            switch (Launch_type)
            {
                case 0:
                    PoolManager.Release(projectiles[Launch_type], muzzle.position);
                    break;
                case 1:
                    PoolManager.Release(projectiles[Launch_type], muzzle.position);
                    break;
                case 2:
                    rotation = 0;
                    for (int i = 0; i < 36; i++)
                    {
                        rotation += 360 / 36;
                        PoolManager.Release(projectiles[Launch_type], muzzle.position, Quaternion.AngleAxis(rotation, Vector3.forward));
                    }
                    //   PoolManager.Release(projectiles[Launch_type], muzzle.position);
                    break;

            }




            continuousFireTimer += minFireInterval;
            yield return waitForContinuousFireInterval;
        }
    }
    protected override IEnumerator RandomlyFireCoroutine()
    {
        while (isActiveAndEnabled)
        {
            yield return waitForFireInterval;
            yield return StartCoroutine(nameof(ContinuousFireCoroutinw));
            if (GameManager.GameState == GameState.GameOver) yield break;
        }
    }
    IEnumerator ChasingPlayerCoruoutine() //boss追击敌人
    {
        while (isActiveAndEnabled)
        {
            targetPosition.x = MyCamera.maxX - PaddingX;
            targetPosition.y = playerTransform.position.y;
            yield return null;
        }
    }

}
