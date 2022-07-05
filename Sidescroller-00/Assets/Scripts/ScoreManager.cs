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
        playerRank.text = PlayerData.PlayerLevelRank();
        lifePointsRemaining.text = PlayerData.playerLifePoints.ToString();
        itemsCollected.text = $"{PlayerData.playerFruitPoints}/{LevelData.totalFruitsInLevel}";
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("SampleScene");

        GameManager.ResetLevelData();
    }
}
