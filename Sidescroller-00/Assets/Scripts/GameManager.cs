using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpoint;

    [SerializeField]
    private Checkpoint checkpointScript;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private Player playerScript;

    [SerializeField]
    private TextMeshProUGUI pointsTextField;

    [SerializeField]
    private RawImage[] lifePointsImageGameObject = new RawImage[3];

    [SerializeField]
    private Sprite loseLifePointImage;

    [SerializeField]
    private GameObject gameOverPanel;

    private int playerLifePointsCount;

    private bool isGameOver = false;

    public static int fruitPoints = 0;

    public static int timesCheckpointUsed = 0;

    private void Start()
    {
        playerLifePointsCount = playerScript.LifePoints;

        LoadPlayerData.playerInitialLifePoints = playerScript.LifePoints;

        CountTotalFruitsInLevel();

        CheckCheckpointUseLimit();
    }

    // Update is called once per frame
    void Update()
    {
        RestartFromCheckpoint();
        CountPoints();
        CheckPlayerLifePoints();
    }

    private void RestartFromCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            !playerScript.IsAlive &&
            Checkpoint.isCheckpointActivated)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            timesCheckpointUsed++;

        }
    }

    private void CheckCheckpointUseLimit()
    {
        if(timesCheckpointUsed > checkpointScript.CheckpointUseLimit &&
            Checkpoint.isCheckpointActivated)
        {
            RespawnPlayerLastTime();

            Checkpoint.isCheckpointActivated = false;

            checkpoint.SetActive(false);
        }
    }

    private void RespawnPlayerLastTime()
    {
        playerGameObject.transform.position = checkpoint.transform.localPosition + (Vector3.up * 2f);
    }

    private void CountPoints()
    {
        pointsTextField.text = fruitPoints.ToString();
    }

    private void CheckPlayerLifePoints()
    {
        if(playerLifePointsCount > playerScript.LifePoints)
        {
            int lastLifePointIndex = playerLifePointsCount - 1;

            lifePointsImageGameObject[lastLifePointIndex].texture = loseLifePointImage.texture;

            playerLifePointsCount--;
        }
        else if(playerScript.LifePoints == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (Checkpoint.isCheckpointActivated)
        {
            isGameOver = true;

            gameOverPanel.SetActive(isGameOver);

        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void CountTotalFruitsInLevel()
    {
        if( LevelData.totalFruitsInLevel == 0)
        {
            LevelData.totalFruitsInLevel = GameObject.FindGameObjectsWithTag("Fruit").Length;
        }
    }
}
