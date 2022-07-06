using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    public static Dictionary<string ,GameObject> itemsCollected = new Dictionary<string, GameObject>();

    private Animator animator;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

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

            PlayerData.playerLifePointsBeforeInstaDeath = lifePoints;

            lifePoints = 0;

            animator.SetTrigger("death");

            playerRigidBody.AddForce(transform.up * 45f, ForceMode2D.Impulse);

            StartCoroutine(DisablePlayerSprite());
        }
    }
   
    IEnumerator DisablePlayerSprite()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }


    private void CheckPlayerStatus()
    {
        if(lifePoints <= 0)
        {
            isAlive = false;

            animator.SetTrigger("death");

            StartCoroutine(DisablePlayerSprite());
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
            lifePoints = PlayerData.playerLifePointsBeforeInstaDeath;
           
            transform.position = LevelData.checkpointPosition;
        }
        else if (Checkpoint.isLastRespawnAllowed)
        {
            transform.position = LevelData.checkpointPosition;

            Checkpoint.isLastRespawnAllowed = false;
        }
    }
}
