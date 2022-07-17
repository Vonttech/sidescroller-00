using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip checkPointAudio;
    [SerializeField]
    private AudioClip trophyAudio;
    [SerializeField]
    private AudioClip sceneBackgroundMusic;
    private bool shouldStopPlayingAudio;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PlayBackgroundMusic()
    {
        m_AudioSource.Play();
    }
}
