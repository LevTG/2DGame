using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D rb;
    float myWidth;


    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
       // myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check to see if there's ground in front of us before moving forward
        Vector2 lineCastPos = transform.position - transform.right * myWidth;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        //If theres no ground, turn around
        if (!isGrounded)
            Flip();

        //Always move forward
        Vector2 vel = rb.velocity;
        vel.x = - transform.localScale.x * speed;
        rb.velocity = vel;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
