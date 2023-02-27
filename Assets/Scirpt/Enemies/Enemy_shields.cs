using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shields : Shields
{
    // Start is called before the first frame update
    void Start()
    {

    }

    protected override void OpenShields()
    {

        base.OpenShields();

    }
    protected override void CloseShields()
    {
        this.enabled = false;
        base.CloseShields();
    }
}
