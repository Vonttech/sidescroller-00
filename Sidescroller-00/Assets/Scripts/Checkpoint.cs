using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    public static bool isCheckpointActivated = false;

    private int checkpointUseLimit = 4;
    public int CheckpointUseLimit { get { return checkpointUseLimit; } }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("playerHitCheckpoint");
            animator.SetBool("checkpointActivated", true);
            isCheckpointActivated = true;
        }
    }
}
