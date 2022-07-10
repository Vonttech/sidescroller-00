using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text itemsCollected;

    [SerializeField]
    private Text playerRank;

    // Start is called before the first frame update
    void Start()
    {
        playerRank.text = PlayerData.PlayerLevelRank();
        itemsCollected.text = $"{PlayerData.playerFruitPoints}/{LevelData.totalFruitsInLevel}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneLoadHandler.currentSceneID);

        GameManager.ResetLevelData();

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
