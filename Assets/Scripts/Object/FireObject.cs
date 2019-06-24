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

    private IEnumerator Start()
    {
        if(startDelay > 0f)
        {
            yield return new WaitForSeconds(startDelay);
        }

        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            var bullet = ObjectPoolManager.instance.GetBullet();
            bullet.transform.position = transform.position;
            var bo = bullet.GetComponent<BulletObject>();


            bo.speed = speed;
            switch (direction)
            {
                case Direction.Up:
                    bo.dir = BulletObject.Direction.Up;
                    break;
                case Direction.Down:
                    bo.dir = BulletObject.Direction.Down;
                    break;
                case Direction.Right:
                    bo.dir = BulletObject.Direction.Right;
                    break;
                case Direction.Left:
                    bo.dir = BulletObject.Direction.Left;
                    break;
            }
        }
    }
}
