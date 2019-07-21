using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterActivePlatform : MonoBehaviour
{
    public float disappearTime;

    public GameObject[] disappearObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            Invoke("Disappear", disappearTime);
        }
    }

    private void Disappear()
    {
        for(int i = 0; i < disappearObject.Length; i++)
        {
            disappearObject[i].SetActive(false);
        }
    }
}
