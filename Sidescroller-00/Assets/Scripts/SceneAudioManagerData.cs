using UnityEngine;
[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/SceneAudioManagerData", order = 1)]
public class SceneAudioManagerData : ScriptableObject
{
    public AudioClip sceneBackgroundMusic;
    public AudioClip buttonClickSound;
}
