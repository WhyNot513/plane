using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judeIsOUT : MonoBehaviour
{
    public GameObject sonProject;
    EnemyProjectile enemyProjectile;
    float dir;
    public bool relase;
    public bool Opend;
    private void OnEnable()
    {
        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyProjectile.moveDirection = Random.Range(0, 2) == 1 ? new Vector2(-0.5f, 0.5f) : new Vector2(0.5f, 0.5f);
        Opend = false;
    }
    private void Update()
    {
        if (transform.position.x > MyCamera.maxX || transform.position.x < MyCamera.minX)
        {
            dir = transform.position.x > MyCamera.maxX ? 270 : 0;
            Opend = true;
            this.gameObject.SetActive(false);

        }
    }
    private void OnDisable()
    {

        if (relase)
        {
            if (!Opend) return;
            float rotation = dir;
            for (int i = 0; i < 5; i++)
            {
                rotation += 360 / 36;
                PoolManager.Release(sonProject, transform.position, Quaternion.AngleAxis(rotation, Vector3.forward));

            }
        }
        relase = true;

    }
}
