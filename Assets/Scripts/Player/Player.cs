using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{  
    public float speed = 10;
    public float jumpForce = 625;

    public bool grounded;
    private bool leftBtnPress;
    private bool rightBtnPress;
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

        rb2d.gravityScale = 3;
        speed = 10;
        jumpForce = 800;
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
        
        if (rb2d.velocity.sqrMagnitude > 210)
        {
            rb2d.velocity = rb2d.velocity.normalized * 15;
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
        if (grounded)
        {
            if (rb2d.gravityScale < 0)
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
            this.gameObject.SetActive(false);
            Invoke("Die", 1f);
        }

        else if(collision.CompareTag("Clear") && !isDead)
        {
            Debug.Log("End Game");
            isDead = true;
            rb2d.velocity = new Vector2(0, 0);
            rb2d.gravityScale = 0;
            gameObject.layer = 9;
            StartCoroutine(EndCoroutine(collision.transform.position));
            GameManager.instance.GameEnd();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Die") && !isDead)
        {
            Debug.Log("Hi");
            isDead = true;
            DieMarkManager.instance.DieMarkOnOff(true);
            DieMarkManager.instance.CreateDieMark(this.transform.position);
            this.gameObject.SetActive(false);
            Invoke("Die", 1f);
        }
    }

    private void Die()
    {
        DieMarkManager.instance.DieMarkOnOff(false);
        SceneLoad.instance.LoadedSceneLoad();
    }

    public IEnumerator EndCoroutine(Vector3 _clearPosition)
    {
        float diff = float.MaxValue;

        while(diff >= 0.005f)
        {
            diff = (_clearPosition - transform.position).sqrMagnitude;
            
            transform.position = Vector2.Lerp(transform.position, _clearPosition, 1f * Time.deltaTime);

            yield return null;
        }

        transform.position = _clearPosition;   
    }
}