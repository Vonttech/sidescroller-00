using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SceneHandlerData", menuName = "ScriptableObjects/SceneHandlerData", order = 1)]
public class SceneHandlerData : ScriptableObject
{
    public int InitialSceneId;
    public Sprite ImageToFadeScenesTransitions;
}
