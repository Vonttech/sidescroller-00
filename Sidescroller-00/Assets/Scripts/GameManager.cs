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
    private Text pointsTextField;

    [SerializeField]
    private RawImage[] lifePointsImageGameObject = new RawImage[3];
    [SerializeField]
    private Sprite loseLifePointImage;
    private int playerLifePointsCount;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject trophy;

    private bool isGameOver = false;

    private float yPlayerRespawnPosition = 3f;


    [SerializeField]
    private RectTransform levelIntroPanel;
    private bool isLevelIntroPanelRunning;
    private bool isHideLevelIntroPanel = false;
    private float counterToHideLevelIntroPanel;
    private float timerLimitToHideLevelIntroPanel = 3f;
    private float yLevelIntroPanelTopLimit = 700f;
    private float yLevelIntroPanelBottomLimit = 300f;
    private float levelIntroPanelSpeedIn = 360f;
    private float levelIntroPanelSpeedOut = 760f;


    private void Awake()
    {
        isLevelIntroPanelRunning = true;

        PlayerController.isAllowedToMove = false;

        counterToHideLevelIntroPanel = 0;

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

        DisplayLevelIntroPanel();
        
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

    private void DisplayLevelIntroPanel() 
    {
        if (levelIntroPanel.transform.localPosition.y >= yLevelIntroPanelBottomLimit && !isHideLevelIntroPanel)
        {
            levelIntroPanel.transform.localPosition -= Vector3.up * levelIntroPanelSpeedIn * Time.deltaTime;
        }
        else
        {
            CountToHideLevelIntroPanel();
        }
    }
    private void CountToHideLevelIntroPanel()
    {
        if(counterToHideLevelIntroPanel <= timerLimitToHideLevelIntroPanel) 
        {
            counterToHideLevelIntroPanel += Time.deltaTime;

        }else if(counterToHideLevelIntroPanel >= timerLimitToHideLevelIntroPanel)
        {
            isHideLevelIntroPanel = true;
            HideLevelIntroPanel();
        }
    }
    private void HideLevelIntroPanel()
    {
        if( levelIntroPanel.transform.localPosition.y <= yLevelIntroPanelTopLimit &&
            isHideLevelIntroPanel)
        {
            levelIntroPanel.transform.localPosition += Vector3.up * levelIntroPanelSpeedOut * Time.deltaTime;
        }
        else
        {
            isLevelIntroPanelRunning = false;
            AllowPlayerMovement();
        }
    }

    private void AllowPlayerMovement()
    {
        if (!isLevelIntroPanelRunning)
        {
            PlayerController.isAllowedToMove = true;
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
