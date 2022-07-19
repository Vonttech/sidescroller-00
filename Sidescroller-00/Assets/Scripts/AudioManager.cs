using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Play one shot of the audioclip
    /// </summary>
    /// <param name="audioClip">audio clip to be played</param>
    public void ShotSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
