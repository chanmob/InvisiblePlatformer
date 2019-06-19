using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, -Physics2D.gravity.y);

            var lc = collision.transform.localScale;
            collision.transform.localScale = new Vector3(lc.x, -lc.y, lc.z);
        }
    }
}
