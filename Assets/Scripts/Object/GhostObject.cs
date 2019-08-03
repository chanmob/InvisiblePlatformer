using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    private Player player;

    public bool lookRight;
    private bool move;

    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();  
    }

    private void FixedUpdate()
    {
        if (player.playerLookRight && !lookRight)
        {
            move = false;
        }

        else if(!player.playerLookRight && lookRight)
        {
            move = false;
        }

        else
        {
            move = true;
        }

        if (player.transform.position.x > transform.position.x)
        {
            lookRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            lookRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (move)
        {
            Vector2 dir = player.transform.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }
}
