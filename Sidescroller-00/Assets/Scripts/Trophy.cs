using System.Collections;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static bool isPlayerBeatLevel;
    private Animator animator;
    private AudioSource audioSource;
    private Collider2D collider;
    
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider.enabled = false;
            audioSource.Play();
            animator.SetTrigger("WinAnimation");
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
