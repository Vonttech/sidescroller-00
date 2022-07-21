using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioManagerData audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator DelayVolumeReturn()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.UnPause();
    }
    /// <summary>
    /// Play one shot of the audioclip
    /// </summary>
    /// <param name="audioClip">audio clip to be played</param>
    public void ShotSound(AudioClip audioClip)
    { 
        //audioSource.Pause();
        audioSource.PlayOneShot(audioClip);
        //StartCoroutine(DelayVolumeReturn());
    }
    /// <summary>
    /// Plays button click sound
    /// </summary>
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(audioData.buttonClickSound);
    }
}
