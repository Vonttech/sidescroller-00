using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGround;
    private bool isDoublej;

    private int jumpCount = 0;

    [SerializeField] float speed;
    [SerializeField] float thurst;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        CheckJumpState();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");

        float posX = inputH * speed * Time.deltaTime;
        
        transform.position += new Vector3(posX, 0, 0);
       


        if(inputH > 0)
        {
            animator.SetBool("moving", true);

            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(inputH < 0)
        {
            animator.SetBool("moving", true);

            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            animator.SetBool("moving", false);
        }
        
    }

    private void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {

            rb.AddForce(transform.up * thurst, ForceMode2D.Impulse);

        }else if (Input.GetKeyDown(KeyCode.Space) && !isGround && !isDoublej)
        {
            rb.AddForce(transform.up * thurst, ForceMode2D.Impulse);

            isDoublej = true;

            animator.SetBool("doubleJump", true);
        }
   
    }


    private void CheckJumpState()
    {
        if(rb.velocity.y > 0)
        {
            animator.SetBool("jumping", true);

            animator.SetBool("isFalling", false);
        }
        else if(rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);

            animator.SetBool("jumping", false);
        }
        else
        {
            animator.SetBool("jumping", false);

            animator.SetBool("isFalling", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
         
            animator.SetBool("isGround", isGround);

            animator.SetBool("doubleJump", false);

            isDoublej = false;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;

            animator.SetBool("isGround", isGround);

            animator.SetBool("moving", false);
        }
    }
}
