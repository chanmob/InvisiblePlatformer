using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{  
    public float speed = 10;
    public float jumpForce = 625;

    public bool grounded;
    private bool leftBtnPress;
    private bool rightBtnPress;
    private bool jumpBtnPress;
    public bool playerLookRight;
    private bool invincibility;
    public bool isDead;

    [HideInInspector]
    public Rigidbody2D rb2d;
    private Vector3 originalScale;
    private BoxCollider2D box2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        playerLookRight = false;
    }

    void Update()
    {
        if (isDead == true)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftButtonPress();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LeftButtonRelease();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightButtonPress();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RightButtonRelease();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButtonPress();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpButtonRelease();
        }
    }

    void FixedUpdate()
    {
        if (isDead == true)
            return;

        if (leftBtnPress == true && rightBtnPress == false)
        {
            if (playerLookRight == true)
            {
                playerLookRight = false;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            //rb2d.AddForce(Vector2.left * speed * Time.deltaTime);
        }

        else if (leftBtnPress == false && rightBtnPress == true)
        {
            if (playerLookRight == false)
            {
                playerLookRight = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
            //rb2d.AddForce(Vector2.right * speed * Time.deltaTime);
        }

        if (rb2d.velocity.sqrMagnitude > 150)
        {
            rb2d.velocity = rb2d.velocity.normalized * 12.5f;
        }
    }

    public void LeftButtonPress()
    {
        leftBtnPress = true;
    }

    public void LeftButtonRelease()
    {
        leftBtnPress = false;
    }

    public void RightButtonPress()
    {
        rightBtnPress = true;
    }

    public void RightButtonRelease()
    {
        rightBtnPress = false;
    }

    public void JumpButtonPress()
    {
        jumpBtnPress = true;

        if (grounded)
        {
            if(Physics2D.gravity.y > 0)
            {
                AddForceToPlayer(new Vector2(0, -jumpForce));
            }
            else
            {
                AddForceToPlayer(new Vector2(0, jumpForce));
            }
            grounded = false;
        }
    }

    public void JumpButtonRelease()
    {
        if(rb2d.velocity.y > 0)
        {
            ForceVelocity(new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f));
        }

        jumpBtnPress = false;
    }

    public void AddForceToPlayer(Vector2 _force)
    {
        rb2d.AddForce(_force);
    }

    public void ForceVelocity(Vector2 _velocity)
    {
        rb2d.velocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Die") && !isDead)
        {
            Debug.Log("Hi");
            isDead = true;
            DieMarkManager.instance.DieMarkOnOff(true);
            DieMarkManager.instance.CreateDieMark(this.transform.position);
            Invoke("Die", 1f);
        }
    }

    private void Die()
    {
        DieMarkManager.instance.DieMarkOnOff(false);
        SceneLoad.instance.LoadedSceneLoad();
    }
}