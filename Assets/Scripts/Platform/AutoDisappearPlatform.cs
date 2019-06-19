using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class AutoDisappearPlatform : MonoBehaviour
{
    public float delayTime;
    public float blinkTime;

    public bool active;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box2d;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box2d = GetComponent<BoxCollider2D>();

        spriteRenderer.enabled = active;
        box2d.enabled = active;

        if(delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);
        }

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkTime);

            active = !active;
            spriteRenderer.enabled = active;
            box2d.enabled = active;
        }
    }
}
