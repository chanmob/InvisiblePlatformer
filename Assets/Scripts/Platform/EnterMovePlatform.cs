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

    public float speed;
 
    void FixedUpdate()
    {
        if (!move)
            return;

        switch (dir)
        {
            case Direction.Up:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
            case Direction.Down:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case Direction.Right:
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerGrounded"))
        {
            move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerGrounded"))
        {
            move = false;
        }
    }
}
