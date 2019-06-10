using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AppearNextPlatform : MonoBehaviour
{
    public GameObject nextPlatform;

    public float delayDisapper;

    public bool first = false;

    private void OnEnable()
    {
        if(!first)
            StartCoroutine(NextPlatformCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y <= -0.8f)
        {
            if (nextPlatform != null)
                nextPlatform.SetActive(true);
        }
    }

    private IEnumerator NextPlatformCoroutine()
    {
        yield return new WaitForSeconds(delayDisapper);

        gameObject.SetActive(false);
    }
}
