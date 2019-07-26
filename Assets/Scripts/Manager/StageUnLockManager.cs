using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUnLockManager : MonoBehaviour
{
    public int maxLevel { private set; get; }
    public int curLevel { private set; get; }

    private void Awake()
    {
        curLevel = SaveAndLoad.instance.LoadIntData("CurLevel");

        for(int i = 0; i < curLevel; i++)
        {

        }
    }
}
