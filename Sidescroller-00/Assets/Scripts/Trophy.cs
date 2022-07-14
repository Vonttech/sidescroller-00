using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static bool isPlayerBeatLevel;

    private Animator trophyAnimator;

    private void Start()
    {
        trophyAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trophyAnimator.SetTrigger("WinAnimation");

            GetComponent<AudioSource>().Play();
        }
    }

    private void PlayerBeatLevel()
    {
        isPlayerBeatLevel = true;
    }

}
