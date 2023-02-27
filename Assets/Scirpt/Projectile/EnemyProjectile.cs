using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    [SerializeField] int MoveType = 0;

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    protected override void OnEnable()
    {
        float z = moveDirection.x == -0.5f ? 45f : -45f;
        transform.rotation = new Quaternion(180f, 0, z, 0);
        base.OnEnable();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override void OnMove()
    {
        switch (MoveType)
        {
            case 0:
                base.OnMove();
                break;
            case 1:
                time += Time.deltaTime * moveSpeed;
                transform.Translate(moveSpeed * moveDirection * Time.deltaTime);
                // transform.rotation = Quaternion.AngleAxis(90 * Mathf.Sin(time), Vector3.forward);
                transform.Translate(moveSpeed * new Vector2(-1, 0) * 0.5f * Time.deltaTime * Mathf.Sin(time));
                break;
            default:
                break;
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
