using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private SceneAudioManagerData sceneAudioManagerData;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sceneAudioManagerData.sceneBackgroundMusic;
        audioSource.PlayDelayed(1f);
    }
}
