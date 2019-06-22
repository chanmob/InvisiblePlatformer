using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction dir;

    public float speed;

    public void FixedUpdate()
    {
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(this.gameObject);
    //}

    private void OnBecameInvisible()
    {
        Debug.Log("Hello");
    }
}
