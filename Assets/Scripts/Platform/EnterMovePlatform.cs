using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMovePlatform : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction dir;

    private bool move;
    public bool oneWay;

    public float speed;

    public int maxSpace;

    private Vector2 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void FixedUpdate()
    {
        if (!move)
        {
            switch (dir)
            {
                case Direction.Up:
                    transform.Translate(Vector2.up * -speed * Time.deltaTime);

                    if (transform.position.y <= pos.y)
                        transform.position = pos;
                    break;
                case Direction.Down:
                    transform.Translate(Vector2.down * -speed * Time.deltaTime);

                    if (transform.position.y >= pos.y)
                        transform.position = pos;
                    break;
                case Direction.Left:
                    transform.Translate(Vector2.left * -speed * Time.deltaTime);

                    if (transform.position.x >= pos.x)
                        transform.position = pos;
                    break;
                case Direction.Right:
                    transform.Translate(Vector2.right * -speed * Time.deltaTime);

                    if (transform.position.x <= pos.x)
                        transform.position = pos;
                    break;
            }
        }
        
        else
        {
            switch (dir)
            {
                case Direction.Up:
                    transform.Translate(Vector2.up * speed * Time.deltaTime);

                    if (transform.position.y >= pos.y + maxSpace.GetTileSize())
                        transform.position = new Vector2(pos.x, pos.y + maxSpace.GetTileSize());
                    break;
                case Direction.Down:
                    transform.Translate(Vector2.down * speed * Time.deltaTime);

                    if (transform.position.y <= pos.y - maxSpace.GetTileSize())
                        transform.position = new Vector2(pos.x, pos.y - maxSpace.GetTileSize());
                    break;
                case Direction.Left:
                    transform.Translate(Vector2.left * speed * Time.deltaTime);

                    if (transform.position.x <= pos.x - maxSpace.GetTileSize())
                        transform.position = new Vector2(pos.x - maxSpace.GetTileSize(), pos.y);
                    break;
                case Direction.Right:
                    transform.Translate(Vector2.right * speed * Time.deltaTime);

                    if (transform.position.x >= pos.x + maxSpace.GetTileSize())
                        transform.position = new Vector2(pos.x + maxSpace.GetTileSize(), pos.y);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(this.transform);
            move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            collision.transform.parent.SetParent(null);
            if (!oneWay)
            {
                move = false;
            }
        }
    }
}
