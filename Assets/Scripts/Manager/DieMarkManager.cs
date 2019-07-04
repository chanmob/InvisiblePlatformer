﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieMarkManager : Singleton<DieMarkManager>
{
    public GameObject dieMark;

    private List<GameObject> markLists = new List<GameObject>();

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateDieMark(Vector2 pos)
    {
        var newDieMark = Instantiate(dieMark, pos, Quaternion.identity);
        newDieMark.transform.SetParent(this.transform);
        markLists.Add(newDieMark);
    }

    public void DieMarkOnOff(bool _on)
    {
        for(int i = 0; i < markLists.Count; i++)
        {
            markLists[i].transform.localScale = new Vector2(0.8f, 0.8f);
            markLists[i].SetActive(_on);
        }
    }

    public void ClearDieMark()
    {
        markLists.Clear();
    }
}