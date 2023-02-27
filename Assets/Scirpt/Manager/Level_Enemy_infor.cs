using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_Enemy_infor", order = 1)]
public class Level_Enemy_infor : ScriptableObject
{

    public List<string> EnemyList = new List<string>();
    private void Awake()
    {
        Debug.Log("1111");

    }
}
