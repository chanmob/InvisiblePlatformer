using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int hp = 5;

    public float speed = 1000f;
    public float jumpForce = 8.5f;
    
    public bool grounded;
    private bool leftBtnPress;
    private bool rightBtnPress;
    private bool jumpBtnPress;
    private bool playerLookRight;
    private bool invincibility;
    public bool isDead;

    private Rigidbody2D rb2d;
    private Vector3 originalScale;
    private BoxCollider2D box2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        playerLookRight = false;
        originalScale = transform.localScale;
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
                transform.localScale = originalScale;
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            //rb2d.AddForce(Vector2.left * speed * Time.deltaTime);
        }

        else if (leftBtnPress == false && rightBtnPress == true)
        {
            if (playerLookRight == false)
            {
                playerLookRight = true;
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            //rb2d.AddForce(Vector2.right * speed * Time.deltaTime);
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
            rb2d.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
    }

    public void JumpButtonRelease()
    {
        if(rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }
        jumpBtnPress = false;
    }
}