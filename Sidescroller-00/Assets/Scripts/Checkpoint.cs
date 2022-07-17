using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static bool isCheckpointActivated = false;
    public static bool isPlayerTouchedCheckpoint = false;
    public static int timesCheckpointUsed = 0;
    public static bool isLastRespawnAllowed = false;

    private Animator animator;
    private AudioSource audioSource;
    private Collider2D collider;
    private int checkpointUseLimit = 4;
    public int CheckpointUseLimit { get { return checkpointUseLimit; } }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        
         if (isCheckpointActivated)
        {
            animator.SetTrigger("playerHitCheckpoint");
            animator.SetBool("checkpointActivated", isCheckpointActivated);
        }
        else
        {
            collider.enabled = true;
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCheckpointActivated)
        {
            collider.enabled = false;
            audioSource.Play();
            isPlayerTouchedCheckpoint = true;
            isCheckpointActivated = true;
            PlayerData.playerLifePointsSaved = PlayerData.playerLifePoints;
            animator.SetTrigger("playerHitCheckpoint");
            animator.SetBool("checkpointActivated", isCheckpointActivated);       
        }
    }
}
