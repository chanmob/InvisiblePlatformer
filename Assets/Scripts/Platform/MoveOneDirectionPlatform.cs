﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOneDirectionPlatform : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction direction;

    public int space;

    public float speed;

    private Vector3 pos;

    private void Start()
    {
        pos = GetComponent<Transform>().position;
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case Direction.Up:
                transform.Translate(Vector2.up * speed * Time.deltaTime);

                if(transform.position.y >= pos.y + space.GetTileSize())
                {
                    if (this.transform.childCount != 0)
                    {
                        transform.GetChild(0).SetParent(null);
                    }

                    transform.position = pos;
                }
                break;
            case Direction.Down:
                transform.Translate(Vector2.down * speed * Time.deltaTime);

                if (transform.position.y <= pos.y - space.GetTileSize())
                {
                    if (this.transform.childCount != 0)
                    {
                        transform.GetChild(0).SetParent(null);
                    }

                    transform.position = pos;
                }
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * speed * Time.deltaTime);

                if (transform.position.x <= pos.x - space.GetTileSize())
                {
                    if (this.transform.childCount != 0)
                    {
                        transform.GetChild(0).parent.SetParent(null);
                    }

                    transform.position = pos;
                }
                break;
            case Direction.Right:
                transform.Translate(Vector2.right * speed * Time.deltaTime);

                if (transform.position.x >= pos.x + space.GetTileSize())
                {
                    if (this.transform.childCount != 0)
                    {
                        transform.GetChild(0).SetParent(null);
                    }

                    transform.position = pos;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(null);
        }
    }
}
