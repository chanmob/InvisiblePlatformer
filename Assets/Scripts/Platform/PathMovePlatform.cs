using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
public class PathMovePlatform : MonoBehaviour
{
    public List<GameObject> wayPoints = new List<GameObject>();
    private List<PathMovePlatform> pathPlatformLists = new List<PathMovePlatform>();

    private Vector2 direction = Vector2.zero;

    private int wayPointIndex = 0;

    public float speed;

    public bool move;
    public bool started;

    private void Start()
    {
        pathPlatformLists = GameObject.FindObjectsOfType<PathMovePlatform>().ToList();
        wayPoints = GameObject.FindGameObjectsWithTag("Path").ToList();
    }

    private void FixedUpdate()
    {
        if (!move)
            return;

        direction = wayPoints[wayPointIndex].transform.position - transform.position;

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, wayPoints[wayPointIndex].transform.position) <= 0.05f)
        {
            transform.position = wayPoints[wayPointIndex].transform.position;
            wayPointIndex++;

            if(wayPointIndex >= wayPoints.Count)
            {
                move = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(this.transform);

            if (!started)
            {
                foreach (var p in pathPlatformLists)
                {
                    if (p.started == false)
                    {
                        p.started = true;
                        p.move = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for (int i = 1; i < wayPoints.Count; i++)
        {
            Gizmos.DrawLine(wayPoints[i - 1].transform.position, wayPoints[i].transform.position);
        }
    }
}
