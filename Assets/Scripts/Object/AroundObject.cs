using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundObject : MonoBehaviour
{
    public enum StartPoint
    {
        LeftTop,
        LeftBot,
        RightTop,
        RightBot
    }

    public StartPoint startPoint;

    public bool reverse;

    private float angleZ;

    private BoxCollider2D box2d;
    private Bounds bounds;

    public GameObject target;

    void Start()
    {
        if(reverse)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector2(-scale.x, scale.y);
        }

        box2d = target.GetComponent<BoxCollider2D>();
        bounds = box2d.bounds;

        switch (startPoint)
        {
            case StartPoint.LeftBot:
                transform.position = box2d.bounds.min;
                break;

            case StartPoint.LeftTop:
                transform.position = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
                break;

            case StartPoint.RightBot:
                transform.position = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
                break;

            case StartPoint.RightTop:
                transform.position = box2d.bounds.max;
                break;
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            gameObject.SetActive(false);
        }

        if (reverse)
        {
            if (transform.position.x <= bounds.max.x && transform.position.y >= bounds.max.y)
            {
                angleZ = 0;
                transform.Translate(Vector2.right * Time.deltaTime, Space.World);
            }

            else if (transform.position.x >= bounds.min.x && transform.position.y <= bounds.min.y)
            {
                angleZ = -180;
                transform.Translate(Vector2.left * Time.deltaTime, Space.World);
            }

            else if (transform.position.y >= bounds.min.y && transform.position.x >= bounds.max.x)
            {
                angleZ = -90;
                transform.Translate(Vector2.down * Time.deltaTime, Space.World);
            }

            else if (transform.position.y <= bounds.max.y && transform.position.x <= bounds.max.x)
            {
                angleZ = -270;
                transform.Translate(Vector2.up * Time.deltaTime, Space.World);
            }
        }

        else
        {
            if (transform.position.x <= bounds.max.x && transform.position.y <= bounds.min.y)
            {
                angleZ = 180;
                transform.Translate(Vector2.right * Time.deltaTime, Space.World);
            }

            else if (transform.position.x >= bounds.min.x && transform.position.y >= bounds.max.y)
            {
                angleZ = 0;
                transform.Translate(Vector2.left * Time.deltaTime, Space.World);
            }

            else if (transform.position.y >= bounds.min.y && transform.position.x <= bounds.min.x)
            {
                angleZ = 90;
                transform.Translate(Vector2.down * Time.deltaTime, Space.World);
            }

            else if (transform.position.y <= bounds.max.y && transform.position.x >= bounds.min.x)
            {
                angleZ = 270;
                transform.Translate(Vector2.up * Time.deltaTime, Space.World);
            }
        }

        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, angleZ, Time.deltaTime * 10));
    }
}
