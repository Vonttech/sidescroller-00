using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static bool isPlayerBeatLevel;

    private Animator trophyAnimator;
    private bool shouldStopAudio = false;
    private void Start()
    {
        trophyAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !shouldStopAudio)
        {
            shouldStopAudio = true;
            trophyAnimator.SetTrigger("WinAnimation");

            GetComponent<AudioSource>().Play();
        }
    }

    private void callPlayerBeatLevel()
    {
        StartCoroutine(PlayerBeatLevel());
    }
    IEnumerator PlayerBeatLevel()
    {
        yield return new WaitForSeconds(3f);
        isPlayerBeatLevel = true;
    }
}
