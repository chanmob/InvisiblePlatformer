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

    private void OnEnable()
    {
        DirectionSetting();
    }

    public void DirectionSetting()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        transform.eulerAngles = new Vector3(0, 0, 0);

        switch (dir)
        {
            case Direction.Up:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case Direction.Down:
                transform.eulerAngles = new Vector3(0, 0, 90);
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                break;
            case Direction.Left:
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                break;
            case Direction.Right:
                break;
        }
    }

    public void FixedUpdate()
    {
        switch (dir)
        {
            case Direction.Up:
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
                break;
            case Direction.Down:
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
                break;
            case Direction.Right:
                transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            ObjectPoolManager.instance.WaitBullet(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("불렛 삭제");

        ObjectPoolManager.instance.WaitBullet(this.gameObject);
    }
}
