using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuddleControllerScript : Unit
{
    public float maxSpeed = 10f;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 7f;

    bool doubleJump = false;
    public Text coinText;

    private bool gameEnd = false;
    public Text endText;
    public GameObject door;
    public GameObject doorOpened;
    public GameObject doorClosed;
    private GameMaster gm;

    public int coin;
    public int key;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        coin = 0;
        SetCoinText();
        endText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", grounded);

        if (grounded)
            doubleJump = false;

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 & facingRight)
            Flip();
    }

    void Update()
    {
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.transform.gameObject.SetActive(false);
            coin = coin + 1;
            gm.coins = coin;
            gm.collCoins.Add(other.gameObject.name);
            SetCoinText();
        }
        if (other.gameObject.CompareTag("Key"))
        {
            other.transform.gameObject.SetActive(false);
            key += 1;
            gm.keys = key;
            gm.collKeys.Add(other.gameObject.name);
            door.GetComponent<SpriteRenderer>().sprite = doorOpened.GetComponent<SpriteRenderer>().sprite;
        }
        if (other.gameObject.CompareTag("Door") && key > 0)
        {
            endText.text = "You won!";
            gameEnd = true;
            anim.SetFloat("Speed", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            endText.text = "You lost! Press \"C\" to restart";
            gameEnd = true;
            anim.SetFloat("Speed", 0);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 5.0f);
        }
    }

    public void SetCoinText()
    {
        coinText.text = "Count: " + coin.ToString();
    }
}