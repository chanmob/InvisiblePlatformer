using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObject : MonoBehaviour
{
    public float jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y <= -0.9f && collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.grounded = false;
            player.DelayJump(0.1f);
            player.ForceVelocity(new Vector2(player.rb2d.velocity.x, 0));
            player.AddForceToPlayer(new Vector2(player.rb2d.velocity.x, jumpForce));
        }
    }
}
