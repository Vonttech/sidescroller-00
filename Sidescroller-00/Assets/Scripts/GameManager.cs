using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform startPointPlataform;

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

    [SerializeField]
    private GameObject trophy;

    private int playerLifePointsCount;

    private bool isGameOver = false;

    private float yPlayerRespawnPosition = 3f;


    private void Awake()
    {
        SetLevelRespawnPointsPosition();
    }

    private void Start()
    {
        playerLifePointsCount = playerScript.LifePoints;

        Trophy.isPlayerBeatLevel = false;

        PlayerData.playerInitialLifePoints = playerScript.LifePoints;

        CountTotalFruitsInLevel();

        CheckCheckpointUseLimit();

        RespawnPlayerFromStartPoint();
    }

    // Update is called once per frame
    void Update()
    {
        RestartFromCheckpoint();
        CountPoints();
        CheckPlayerLifePoints();
        PlayerBeatLevel();
    }

    private void RespawnPlayerFromStartPoint()
    {
        playerGameObject.transform.position = LevelData.levelStartPoint;
    }

   private void SetLevelRespawnPointsPosition()
    {
        LevelData.levelStartPoint = startPointPlataform.transform.position + (Vector3.up * yPlayerRespawnPosition);

        LevelData.checkpointPosition = checkpoint.transform.position + (Vector3.up * yPlayerRespawnPosition);
    }

    private void RestartFromCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            !playerScript.IsAlive &&
            Checkpoint.isCheckpointActivated)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Checkpoint.timesCheckpointUsed++;

        }
    }

    private void CheckCheckpointUseLimit()
    {
        if(Checkpoint.timesCheckpointUsed > checkpointScript.CheckpointUseLimit &&
            Checkpoint.isCheckpointActivated)
        {

            Checkpoint.isLastRespawnAllowed = true;

            Checkpoint.isCheckpointActivated = false;

            checkpoint.SetActive(false);
        }
    }

    private void CountPoints()
    {
        pointsTextField.text = PlayerData.playerFruitPoints.ToString();
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

    private void PlayerBeatLevel()
    {
        if (Trophy.isPlayerBeatLevel)
        {
            SceneManager.LoadScene("ScoreScene");
        }
    }

    private void CountTotalFruitsInLevel()
    {
        if( LevelData.totalFruitsInLevel == 0)
        {
            LevelData.totalFruitsInLevel = GameObject.FindGameObjectsWithTag("Fruit").Length;
        }
    }

    public static void ResetLevelData()
    {
        Player.itemsCollected.Clear();

        PlayerData.playerFruitPoints = 0;

        Checkpoint.isCheckpointActivated = false;

        Checkpoint.timesCheckpointUsed = 0;

        Checkpoint.isLastRespawnAllowed = false;

    }
}
