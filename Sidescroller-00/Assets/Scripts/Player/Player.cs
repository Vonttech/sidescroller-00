using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static Dictionary<string, GameObject> itemsCollected = new Dictionary<string, GameObject>();
    private bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }

        set { isAlive = value; }
    }
    private Vector3 playerRespawPos;
    public Vector3 PlayerRespawPos
    {
        get { return playerRespawPos; }
        set { playerRespawPos = value; }
    }
    private int lifePoints = 3;
    public int LifePoints
    {
        get { return lifePoints; }
        set { LifePointsSetTreatment(value); }
    }
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    private AudioSource audioSource;
    private Collider2D m_collider;
    private bool shouldStopCheckPlayerStatus = false;

    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip spawnSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnSound);
        m_collider = GetComponent<Collider2D>();
        m_collider.enabled = true;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerStatus();
        PlayerData.playerLifePoints = lifePoints;
    }

    private void LifePointsSetTreatment(int value)
    {
        if(value <= 0)
        {
            lifePoints = 0;
        }
        else
        {
            lifePoints = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            isAlive = false;
            lifePoints = 0;
            animator.SetTrigger("death");
            playerRigidBody.AddForce(transform.up * 45f, ForceMode2D.Impulse);
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
        if (lifePoints <= 0 && !shouldStopCheckPlayerStatus)
        {
            shouldStopCheckPlayerStatus = true;
            m_collider.enabled = false;
            isAlive = false;
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
            lifePoints = PlayerData.playerLifePointsSaved;
           
            transform.position = LevelData.checkpointPosition;
        }
        else if (Checkpoint.isLastRespawnAllowed)
        {
            transform.position = LevelData.checkpointPosition;
            Checkpoint.isLastRespawnAllowed = false;
        }
    }
}
