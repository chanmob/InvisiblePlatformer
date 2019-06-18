using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public int space;
    public float size;

    public float speed;
    
    private float EarlyPos;
    private float pos;

    public bool reverse;
    public bool upperAndLower;
    public bool isPlatform;
    private bool opposition;

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


        if (pos >= EarlyPos + space.GetTileSize())
        {
            opposition = true;
        }
        else if(pos <= EarlyPos)
        {
            opposition = false;
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
}
