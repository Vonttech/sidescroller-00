using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private Animator animator;

    public static bool isCheckpointActivated = false;
    
    public static int timesCheckpointUsed = 0;

    public static bool isLastRespawnAllowed = false;

    private int checkpointUseLimit = 4;
    public int CheckpointUseLimit { get { return checkpointUseLimit; } }

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
         if (isCheckpointActivated)
        {
            animator.SetTrigger("playerHitCheckpoint");
            animator.SetBool("checkpointActivated", isCheckpointActivated);
        }    

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCheckpointActivated)
        {
            isCheckpointActivated = true;
            PlayerData.playerLifePointsSaved = PlayerData.playerLifePoints;
            animator.SetTrigger("playerHitCheckpoint");
            animator.SetBool("checkpointActivated", isCheckpointActivated);
            
        }
    }
}
