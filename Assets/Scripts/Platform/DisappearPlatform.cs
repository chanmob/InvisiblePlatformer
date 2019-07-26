using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DisappearPlatform : MonoBehaviour
{
    public float delayDisappear;
    public float delayApeear;

    private IEnumerator disappearCoroutine;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGrounded"))
        {
            disappearCoroutine = Disappear(collision.transform.parent.gameObject);
            StartCoroutine(disappearCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(disappearCoroutine != null)
        {
            StopCoroutine(disappearCoroutine);
            sprite.color = new Color(1, 1, 1, 1);
            disappearCoroutine = null;
        }
    }

    private IEnumerator Disappear(GameObject _player)
    {
        float alpha = 1;
        
        while(alpha >= 0)
        {
            alpha -= (Time.deltaTime / delayDisappear);
            sprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        
        Invoke("Appear", delayApeear);
        _player.GetComponent<Player>().grounded = false;
        this.gameObject.SetActive(false);
    }

    private void Appear()
    {
        sprite.color = new Color(1, 1, 1, 1);
        this.gameObject.SetActive(true);
    }
}
