using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    public static bool isCheckpointActivated = false;

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
            Player.isReacheadCheckPoint = true;
            isCheckpointActivated = true;
        }
    }
}
