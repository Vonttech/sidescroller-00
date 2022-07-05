using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int timesCheckpointUsed = 0;

    public static Vector3 levelStartPoint;

    public static bool isLevelReseted = false;

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

    private void Start()
    {
        playerLifePointsCount = playerScript.LifePoints;


        Trophy.isPlayerBeatLevel = false;

        PlayerData.playerInitialLifePoints = playerScript.LifePoints;

        levelStartPoint = startPointPlataform.transform.position + (Vector3.up * yPlayerRespawnPosition); ;

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
        playerGameObject.transform.position = levelStartPoint;
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
            LastCheckpointPlayerRespaw();

            Checkpoint.isCheckpointActivated = false;

            checkpoint.SetActive(false);
        }
    }

    private void LastCheckpointPlayerRespaw()
    {
        Debug.Log("Last checkpoint position"); //O ÚLTIMO CHECKPOINT LEVA PARA O INÍCIO DO JOGO
        playerGameObject.transform.position = checkpoint.transform.localPosition + (Vector3.up * 2f);
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
        isLevelReseted = true;

        PlayerData.playerFruitPoints = 0;

        Checkpoint.isCheckpointActivated = false;

        GameManager.timesCheckpointUsed = 0;

    }
}
