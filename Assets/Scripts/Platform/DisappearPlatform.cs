using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DisappearPlatform : MonoBehaviour
{
    public float delayDisappear;
    public float delayApeear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayDisappear);

        Invoke("Appear", delayApeear);
        this.gameObject.SetActive(false);
    }

    private void Appear()
    {
        this.gameObject.SetActive(true);
    }
}
