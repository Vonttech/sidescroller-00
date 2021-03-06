using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isAllowedToMove;

    private Player player = new Player();
    
    private Collider2D m_collider;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip doubleJumpAudio;
    private bool isHitTaken;
    private bool isPlayerOnGround;
    private bool isRunning;
    private bool isJumping;
    private bool isDoubleJumping;
    private int jumpCount = 0;
    private int jumpsLimit = 3;
    [SerializeField] 
    private float moveSpeed;
    [SerializeField] 
    private float thurst = 10;
    [SerializeField] 
    private float hitThurst = 8;
    [SerializeField] 
    AudioClip hitAudio;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip spawnSound;
    private Vector3 playerRespawPos;
    public Vector3 PlayerRespawPos
    {
        get { return playerRespawPos; }
        set { playerRespawPos = value; }
    }
    private bool shouldStopCheckPlayerStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isHitTaken = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnSound);
        m_collider = GetComponent<Collider2D>();
        m_collider.enabled = true;
        PlayerData.playerLifePoints = player.LifePoints;

    }
    // Update is called once per frame
    void Update()
    {
        if (isAllowedToMove)
        {
            Move();
            Jump();
            CheckJumpState();
            GetAnimatorBoolValues();
        }

    }
    private void GetAnimatorBoolValues()
    {
        isPlayerOnGround = animator.GetBool("isPlayerOnGround");
        isRunning = animator.GetBool("isRunning");
        isJumping = animator.GetBool("isJumping");
        isDoubleJumping = animator.GetBool("isDoubleJumping");
    }
    private void Move()
    {
        float inputHorizontalValue = Input.GetAxis("Horizontal");
        float positionXValue = inputHorizontalValue * moveSpeed * Time.deltaTime;
        CheckRunningConditions(inputHorizontalValue, positionXValue);
    }
    private void CheckRunningConditions(float inputMovimentValue, float positionXValue)
    {
        int yRotationDegrees;
        if (!isHitTaken) {
            transform.position += new Vector3(positionXValue, 0, 0);
            if (isPlayerOnGround)
            {
                bool isPlayerMoving = CheckIsPlayerRunning(inputMovimentValue);
                animator.SetBool("isRunning", isPlayerMoving);
            }
            if(inputMovimentValue > 0)
            {
                yRotationDegrees = 0;
                transform.eulerAngles = new Vector3(0, yRotationDegrees, 0);
            }
            else if(inputMovimentValue < 0)
            {
                yRotationDegrees = 180;
                transform.eulerAngles = new Vector3(0, yRotationDegrees, 0);
            }
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && 
            isPlayerOnGround && 
            !isHitTaken)
        {
            isPlayerOnGround = false;
            audioSource.Play();
            animator.SetBool("isPlayerOnGround", isPlayerOnGround);
            rigidBody.AddForce(transform.up * thurst, ForceMode2D.Impulse);
            jumpCount++;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && 
            !isPlayerOnGround && 
            !isDoubleJumping && 
            !isHitTaken && 
            jumpCount < jumpsLimit)
        {
            rigidBody.AddForce(transform.up * thurst, ForceMode2D.Impulse);
            audioSource.PlayOneShot(doubleJumpAudio);
            isDoubleJumping = true;
            animator.SetBool("isDoubleJumping", isDoubleJumping);
            jumpCount = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayerHittenByTrap = collision.gameObject.CompareTag("Trap");
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isHitTaken = false;
            isJumping = false;
            isDoubleJumping = false;
            animator.SetBool("isPlayerOnGround", true);
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isDoubleJumping", isDoubleJumping);
        }
        CheckTrapHitsPlayer(isPlayerHittenByTrap);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        bool isPlayerOffTheGround = collision.gameObject.CompareTag("Ground");
        if (isPlayerOffTheGround)
        {
            isPlayerOnGround = false;
            isRunning = false;
            animator.SetBool("isPlayerOnGround", isPlayerOnGround);
            animator.SetBool("isRunning", isRunning);
        }
    }
    private void CheckTrapHitsPlayer(bool isPlayerHittenByTrap)
    {
        if (isPlayerHittenByTrap)
        {
            animator.SetBool("isPlayerOnGround", false);
            animator.SetTrigger("hit");
            isHitTaken = true;
            audioSource.PlayOneShot(hitAudio);
            rigidBody.AddForce((transform.up + (-transform.right)) * hitThurst, ForceMode2D.Impulse);
            GetComponent<Player>().LifePoints--;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
    private void CheckJumpState()
    {
        if (rigidBody.velocity.y > 0.1 && 
            !isHitTaken &&
            !isPlayerOnGround)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if (rigidBody.velocity.y < 0 && 
            !isHitTaken &&
            !isPlayerOnGround)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
    private bool CheckIsPlayerRunning( float playerMovementValue )
    {
        if (playerMovementValue > 0 || playerMovementValue < 0)
        {
            isRunning = true;
            return isRunning;
        }
        else
        {
            isRunning = false;
            return isRunning;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            player.IsAlive = false;
            player.LifePoints = 0;
            animator.SetTrigger("death");
            rigidBody.AddForce(transform.up * 45f, ForceMode2D.Impulse);
            StartCoroutine(DisablePlayer());
        }
    }

    IEnumerator DisablePlayer()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
    private void CheckPlayerStatus()
    {
        if (player.LifePoints <= 0 && !shouldStopCheckPlayerStatus)
        {
            shouldStopCheckPlayerStatus = true;
            m_collider.enabled = false;
            player.IsAlive = false;
            audioSource.PlayOneShot(deathSound);
            animator.SetTrigger("death");
            StartCoroutine(DisablePlayer());
        }
    }
    private void SpawnPlayer()
    {
        if (!Checkpoint.isCheckpointActivated && !Checkpoint.isLastRespawnAllowed)
        {
            transform.position = LevelData.levelStartPoint;
        }
        else if (Checkpoint.isCheckpointActivated && !Checkpoint.isLastRespawnAllowed)
        {
            player.LifePoints = PlayerData.playerLifePointsSaved;

            transform.position = LevelData.checkpointPosition;
        }
        else if (Checkpoint.isLastRespawnAllowed)
        {
            transform.position = LevelData.checkpointPosition;
            Checkpoint.isLastRespawnAllowed = false;
        }
    }
}
