using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addblone : Dropsthing
{
    // Start is called before the first frame update
    [SerializeField] float Health;
    protected override void DestoryGameObject()
    {
        base.DestoryGameObject();
        addBlond();
    }
    void addBlond()
    {
        tartget.GetComponent<Character>().RestoryHealth(Health);
    }
}
