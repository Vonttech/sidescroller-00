using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemsCollected;

    [SerializeField]
    private TextMeshProUGUI playerRank;

    [SerializeField]
    private TextMeshProUGUI lifePointsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        playerRank.text = LoadPlayerData.PlayerLevelRank();
        lifePointsRemaining.text = LoadPlayerData.playerLifePoints.ToString();
        itemsCollected.text = $"{LoadPlayerData.playerFruitPoints}/{LevelData.totalFruitsInLevel}";
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("SampleScene");

        GameManager.timesCheckpointUsed = 0;

        LoadPlayerData.playerFruitPoints = 0;
    }
}
