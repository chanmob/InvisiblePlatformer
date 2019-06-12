using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObject : MonoBehaviour
{
    public float jumpForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            var player = collision.gameObject.GetComponentInParent<Player>();
            player.ForceVelocity(new Vector2(player.rb2d.velocity.x, 0));
            player.AddForceToPlayer(new Vector2(player.rb2d.velocity.x, jumpForce));
        }
    }
}
