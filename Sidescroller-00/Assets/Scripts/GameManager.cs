using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    /*
     CRIAR CLASSE PARA O OBJETO QUE IRÁ GERENCIAR OS PONTOS DE RESPAWN DO LEVEL 
     */

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
    private GameObject pauseMenuPanel;

    [SerializeField]
    private GameObject trophy;

    [SerializeField]
    private SceneHandler sceneHandler;

    private bool isGameOver = false;

    private float yPlayerRespawnPosition = 3f;

    [SerializeField]
    private IntroPanelManager introPanelManager;    
  
    private void Awake()
    {
        PlayerController.isAllowedToMove = false;

        SceneHandler.currentSceneID = SceneManager.GetActiveScene().buildIndex;

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
        introPanelManager.DisplayLevelIntroPanel();

        AllowPlayerMovement();

        RestartFromCheckpoint();
        
        CountPoints();
        
        CheckPlayerLifePoints();
        
        PlayerBeatLevel();

        sceneHandler.ChangePauseGameState();
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
        if (Input.anyKeyDown && !playerScript.IsAlive &&
            Checkpoint.isCheckpointActivated)
        {
            StartCoroutine("CountToRespawn");
        }
    }

    IEnumerator CountToRespawn()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Checkpoint.timesCheckpointUsed++;
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

    private void AllowPlayerMovement()
    {
        if (!introPanelManager.IsLevelIntroPanelRunning)
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
