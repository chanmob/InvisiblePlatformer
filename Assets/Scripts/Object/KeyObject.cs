using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public GameObject[] keyTileObjects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);

            for(int i = 0; i < keyTileObjects.Length; i++)
            {
                keyTileObjects[i].SetActive(false);
            }
        }
    }
}
