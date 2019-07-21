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
    public bool notBlink;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box2d;

    private Transform[] childObject;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box2d = GetComponent<BoxCollider2D>();

        childObject = transform.GetcomponentsRealChildren<Transform>();

        spriteRenderer.enabled = active;
        box2d.enabled = active;

        if (childObject.Length >= 1)
        {
            for (int i = 0; i < childObject.Length; i++)
            {
                childObject[i].gameObject.SetActive(active);
            }
        }

        if (delayTime > 0)
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

            if (childObject.Length >= 1)
            {
                for (int i = 0; i < childObject.Length; i++)
                {
                    childObject[i].gameObject.SetActive(active);
                }
            }

            if (notBlink)
            {
                yield break;
            }
        }
    }
}
