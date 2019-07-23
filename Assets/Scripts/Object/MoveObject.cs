using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public int space;

    public float speed;
    
    private float EarlyPos;
    private float pos;

    public bool wall;
    public bool reverse;
    public bool upperAndLower;
    public bool isPlatform;
    public bool opposition;

    void Start()
    {
        var ealryPosition = GetComponent<Transform>().position;

        if (upperAndLower)
            EarlyPos = ealryPosition.y;
        else
            EarlyPos = ealryPosition.x;

        if (reverse)
        {
            EarlyPos = EarlyPos - space.GetTileSize();
        }
    }

    private void FixedUpdate()
    {
        if (upperAndLower)
        {
            pos = transform.position.y;
        }
        else
        {
            pos = transform.position.x;
        }

        if(!wall)
        {
            if (pos >= EarlyPos + space.GetTileSize())
            {
                opposition = true;

                if (!isPlatform)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (pos <= EarlyPos)
            {
                opposition = false;

                if (!isPlatform)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }


        if (upperAndLower)
        {
            if(opposition)
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            else
                transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            if (opposition)
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            else
                transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (wall)
        //{
        //    opposition = !opposition;

        //    if (!isPlatform && opposition)
        //    {
        //        this.transform.localScale = new Vector3(1, 1, 1);
        //    }
        //    else if (!isPlatform && !opposition)
        //    {
        //        this.transform.localScale = new Vector3(-1, 1, 1);
        //    }
        //}

        if (collision.gameObject.CompareTag("PlayerGrounded") && isPlatform)
        {
            collision.transform.parent.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded") && isPlatform)
        {
            collision.transform.parent.SetParent(null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (wall && collision.gameObject.CompareTag("Ground"))
        {
            opposition = !opposition;

            if (!isPlatform && opposition)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (!isPlatform && !opposition)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
