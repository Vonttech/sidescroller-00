using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemsCollected;

    [SerializeField]
    private TextMeshProUGUI playerRank;

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
        SceneManager.LoadScene("SampleScene");

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
