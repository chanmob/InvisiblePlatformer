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
            var player = collision.GetComponent<Player>();
            collision.GetComponent<Player>().grounded = true;
            if(player.rb2d.velocity.y < 0)
            {
                player.ForceVelocity(new Vector2(player.rb2d.velocity.x, 0));
            }
            Invoke("VisibleObject", delayVisible);
            gameObject.SetActive(false);
        }
    }

    private void VisibleObject()
    {
        gameObject.SetActive(true);
    }
}
