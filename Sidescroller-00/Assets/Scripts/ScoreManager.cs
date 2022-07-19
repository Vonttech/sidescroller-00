using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text itemsCollected;

    [SerializeField]
    private Text playerRank;

    [SerializeField]
    private Text lifePointsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        playerRank.text = PlayerData.PlayerLevelRank();
        lifePointsRemaining.text = PlayerData.playerLifePoints.ToString();
        itemsCollected.text = $"{PlayerData.playerFruitPoints}/{LevelData.totalFruitsInLevel}";
    }

    public void NextLevel()
    {
        SceneLoadHandler.NextLevel();
        SceneLoadHandler.ResetLevelData();
    }
}
