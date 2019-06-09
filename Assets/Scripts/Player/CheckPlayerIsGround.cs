﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerIsGround : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //player.grounded = false;
    }
}
