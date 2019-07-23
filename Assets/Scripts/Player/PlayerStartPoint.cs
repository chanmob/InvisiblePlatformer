using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = this.transform.position;
    }
}
