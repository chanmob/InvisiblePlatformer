using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DoubleJumpObject : MonoBehaviour
{
    public float delayVisible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().grounded = true;
            Invoke("VisibleObject", delayVisible);
            gameObject.SetActive(false);
        }
    }

    private void VisibleObject()
    {
        gameObject.SetActive(true);
    }
}
