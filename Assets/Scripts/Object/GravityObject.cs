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

            var checks = collision.GetComponentsInChildren<CheckPlayerIsGround>(true);
            if(Physics2D.gravity.y > 0)
            {
                checks[0].gameObject.SetActive(false);
                checks[1].gameObject.SetActive(true);
            }
            else
            {
                checks[0].gameObject.SetActive(true);
                checks[1].gameObject.SetActive(false);
            }
        }
    }
}
