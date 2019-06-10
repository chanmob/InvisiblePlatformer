using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed;
    public float distance;

    private float EarlyPos;
    private float pos;

    public bool upperAndLower;
    private bool opposition;

    void Start()
    {
        var ealryPosition = GetComponent<Transform>().position;

        if (upperAndLower)
            EarlyPos = ealryPosition.y;
        else
            EarlyPos = ealryPosition.x;
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


        if(pos >= EarlyPos + distance)
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
}
