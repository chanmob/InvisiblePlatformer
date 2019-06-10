using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class LaunchObject : MonoBehaviour
{
    public Vector2 launchDir;

    public float force;

    private Rigidbody2D rb2d;

    private BoxCollider2D box2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
    }

    public void Launch()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.AddForce(launchDir * force);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        box2d.isTrigger = false;
    }
}
