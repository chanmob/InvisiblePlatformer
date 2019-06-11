using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundObject : MonoBehaviour
{
    private BoxCollider2D box2d;

    public GameObject target;

    void Start()
    {
        box2d = target.GetComponent<BoxCollider2D>();
        transform.position = box2d.bounds.min;
    }

    private void FixedUpdate()
    {
        if(transform.position.x <= box2d.bounds.max.x && transform.position.y >= box2d.bounds.max.y)
        {
            transform.Translate(Vector2.right * Time.deltaTime);
        }

        else if(transform.position.y >= box2d.bounds.min.y && transform.position.x >= box2d.bounds.max.x)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }

        else if(transform.position.x >= box2d.bounds.min.x && transform.position.y <= box2d.bounds.min.y)
        {
            transform.Translate(Vector2.left * Time.deltaTime);
        }

        else if(transform.position.y <= box2d.bounds.max.y && transform.position.x <= box2d.bounds.max.x)
        {
            transform.Translate(Vector2.up * Time.deltaTime);
        }
    }
}
