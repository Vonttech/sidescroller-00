using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    
    private Animator animator;
    private bool isGround;
    private bool isDoublej;
    private bool isHitTaken;
    private bool isPlayerOnGround;
    
    private int jumpCount = 0;

    [SerializeField] float moveSpeed;
    [SerializeField] float thurst = 10;
    [SerializeField] float hitThurst = 8;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

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
        float inputHorizontalValue = Input.GetAxis("Horizontal");

        float positonXValue = inputHorizontal * moveSpeed * Time.deltaTime;
   
        CheckMovementConditions(inputHorizontalValue, positionXValue, isHitTaken);
     
    }
    
    private void CheckMovementConditions(float inputMovimentValue, float positionXValue, bool isHitTaken)
    {
        int yRotationDegrees;
        
        if (!isHitTaken) {
            transform.position += new Vector3(positionXValue, 0, 0);
        }
        
        if(inputHorizontalValue > 0 && !isHitTaken)
        {
            animator.SetBool("moving", true);

            yRotationDegrees = 0;
                
            transform.eulerAngles = new Vector3(0, yRotationDegrees, 0);
        }
        else if(inputHorizontalValue < 0 && !isHitTaken)
        {
            animator.SetBool("moving", true);

            yRotationDegrees = 180;
            
            transform.eulerAngles = new Vector3(0, yRotationDegrees, 0);
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
            rigidBody.AddForce(transform.up * thurst, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGround && !isDoublej && !isHitTaken)
        {
            rigidBody.AddForce(transform.up * thurst, ForceMode2D.Impulse);

            isDoublej = true;

            animator.SetBool("doubleJump", true);
        }
   
    }


    private void CheckJumpState()
    {
        if(rigidBody.velocity.y > 0 && !isHitTaken)
        {
            animator.SetBool("jumping", true);

            animator.SetBool("isFalling", false);
        }
        else if(rigidBody.velocity.y < 0 && !isHitTaken && !isGround)
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
        bool isPLayerHittenByTrap = collision.gameObject.CompareTag("Trap");
        
        isPlayerOnGround = collision.gameObject.CompareTag("Ground");
        
        CheckPlayerOnGround(bool isPlayerOnGround);
        
        CheckTrapHitsPlayer(bool isPlayerHittenByTrap);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isPlayerOnGround = collision.gameObject.CompareTag("Ground");
        
        CheckPlayerOnGround(bool isPlayerOnGround);
    }
    
    private void CheckPlayerOnGround(bool isPlayerOnGround)
    {
        if (isPlayerOnGround)
        {
            isGround = true;
         
            animator.SetBool("isGround", isGround);

            animator.SetBool("doubleJump", false);

            isDoublej = false;

            isHitTaken = false;

        }
        else
        {
            isGround = false;

            animator.SetBool("isGround", isGround);

            animator.SetBool("moving", false);
        }
    }
    
    private void CheckTrapHitsPlayer(bool isPlayerHittenByTrap)
    {
            animator.SetBool("isGround", false);

            animator.SetTrigger("hit");

            isHitTaken = true;

            GetComponent<Player>().LifePoints--;

            rigidBody.AddForce((transform.up + (-transform.right)) * hitThurst, ForceMode2D.Impulse);
    }
}
