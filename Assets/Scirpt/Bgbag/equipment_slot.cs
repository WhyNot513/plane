using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment_slot : MonoBehaviour
{
    public int Grade; //等级
    public float addattribute;//添加是属性值

    public void UpdateGrade() //升级
    {
        Grade++;
        addattribute++;
    }



}
