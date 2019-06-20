using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMoveManager : MonoBehaviour
{
    public List<PathMovePlatform> pathPlatformLists = new List<PathMovePlatform>();

    public List<GameObject> pathLists = new List<GameObject>();

    private bool started = false;

    private void Start()
    {
        pathLists = GameObject.FindGameObjectsWithTag("Path").ToList();
        pathPlatformLists = GetComponentsInChildren<PathMovePlatform>().ToList();

        for(int i = 0; i < pathPlatformLists.Count; i++)
        {
            pathPlatformLists[i].wayPoints = pathLists;
        }
    }

    public void StartMove()
    {
        if (started)
            return;

        started = true;
        for(int i = 0; i < pathPlatformLists.Count; i++)
        {
            pathPlatformLists[i].move = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for (int i = 1; i < pathLists.Count; i++)
        {
            Gizmos.DrawLine(pathLists[i - 1].transform.position, pathLists[i].transform.position);
        }
    }
}
