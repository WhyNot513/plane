using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMananger : UnitySingleton<LoadMananger>
{
    public LoadData loadData;
    public List<string> AcountMessage = new List<string>();
}
