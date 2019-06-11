using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector2.right * speed * Time.deltaTime);
    }
}
