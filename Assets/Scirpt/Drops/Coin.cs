using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Dropsthing
{
    protected override void DestoryGameObject()
    {
        base.DestoryGameObject();
        GameManager.Instance.coin++;
    }
}
