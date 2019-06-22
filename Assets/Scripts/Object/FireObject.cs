using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    public Direction direction;

    public float speed;
    public float startDelay;
    public float delayTime;

    public BulletObject bulletPrefab;

    private IEnumerator Start()
    {
        if(startDelay > 0f)
        {
            yield return new WaitForSeconds(startDelay);
        }
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.speed = speed;
            switch (direction)
            {
                case Direction.Up:
                    bullet.dir = BulletObject.Direction.Up;
                    break;
                case Direction.Down:
                    bullet.dir = BulletObject.Direction.Down;
                    break;
                case Direction.Right:
                    bullet.dir = BulletObject.Direction.Right;
                    break;
                case Direction.Left:
                    bullet.dir = BulletObject.Direction.Left;
                    break;
            }
        }
    }
}
