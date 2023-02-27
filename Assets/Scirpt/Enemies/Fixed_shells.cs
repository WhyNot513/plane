using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed_shells : Projectile
{

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        WaitDestoryTime = new WaitForSeconds(DestoryTime);

        Destory = StartCoroutine(nameof(DestoryBullet));

    }
    protected override void OnDisable()
    {

        StopCoroutine(Destory);

    }

}
