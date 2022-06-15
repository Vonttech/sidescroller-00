using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGround;

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
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            animator.SetBool("jumping", false);
            animator.SetBool("isGround", isGround);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            animator.SetBool("jumping", true);
            animator.SetBool("isGround", isGround);
        }
    }
}
