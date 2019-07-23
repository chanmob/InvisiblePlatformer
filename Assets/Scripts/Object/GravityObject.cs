using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.rb2d.gravityScale = player.rb2d.gravityScale * -1;
            
            var lc = collision.transform.localScale;
            collision.transform.localScale = new Vector3(lc.x, -lc.y, lc.z);
        }
    }
}
