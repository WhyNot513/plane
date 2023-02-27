using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerOverdrive : MonoBehaviour
{
    public static UnityAction on = delegate { };
    public static UnityAction off = delegate { };

    private void Awake()
    {
        on += On;
        off += Off;

    }
    private void OnDestroy()
    {
        on -= On;
        off -= Off;
    }
    private void On()
    {
        //修改粒子特效
    }
    private void Off()
    {

    }
}
