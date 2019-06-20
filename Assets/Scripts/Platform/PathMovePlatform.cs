using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PathMovePlatform : MonoBehaviour
{
    public List<GameObject> wayPoints = new List<GameObject>();

    private Vector2 direction = Vector2.zero;

    private int wayPointIndex = 0;

    public float speed;

    public bool move;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var manager = GetComponentInParent<PathMoveManager>();

            manager.StartMove();
        }
    }
}
