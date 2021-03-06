using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
public class GameManager : MonoBehaviour
{

    private bool isGamePaused = false;
    private bool isGameOver = false;
    private int playerLifePointsCount;
    private Animator animator;
    private SceneHandler sceneHandler = new SceneHandler();

    [SerializeField]
    private SceneHandlerData sceneHandlerData;
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private AudioManagerData audioManagerData;
    [SerializeField]
    private GameObject playerGameObject;
    private PlayerController playerController;
    [SerializeField]
    private Text pointsTextField;
    [SerializeField]
    private RawImage[] lifePointsImageGameObject = new RawImage[3];
    [SerializeField]
    private Sprite loseLifePointImage;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject pauseMenuPanel;
    [SerializeField]
    private GameObject trophy;
    [SerializeField]
    private IntroPanelManager introPanelManager;
    [SerializeField]
    private SpawnPointsHandler spawnPointsHandler;

    private void Awake()
    {
        PlayerController.isAllowedToMove = false;
        SceneHandler.currentSceneID = SceneManager.GetActiveScene().buildIndex;
        //spawnPointsHandler.SetLevelSpawnPointsPosition();
    }
    private void Start()
    {
        playerController = playerGameObject.GetComponent<PlayerController>();
        playerLifePointsCount = playerController.LifePoints; //get player lifepoints
        Trophy.isPlayerBeatLevel = false;
        PlayerData.playerInitialLifePoints = playerController.LifePoints;
        spawnPointsHandler.CheckCheckpointUseLimit();
        animator = GetComponent<Animator>();
        CountTotalFruitsInLevel();
        SpawnPlayerFromStartPoint();
    }
    void Update()
    {
        introPanelManager.DisplayLevelIntroPanel();
        AllowPlayerMovement();
        RestartFromCheckpoint();
        CountPoints();
        CheckPlayerLifePoints();
        PlayerBeatLevel();
        ChangePauseGameState();
    }

    IEnumerator CountToRespawn()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Checkpoint.timesCheckpointUsed++;
    }
    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1f);
        GameOver();
    }
    IEnumerator DelayResumeGame()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);
    }

    private void SpawnPlayerFromStartPoint()
    {
        if (!Checkpoint.isCheckpointActivated && !Checkpoint.isLastRespawnAllowed)
        {
            playerGameObject.transform.position = LevelData.levelStartPoint;
        }
        else
        {
            RestartFromCheckpoint();
        }
    }
    private void RestartFromCheckpoint()
    {
        if (!playerController.IsAlive &&
            Checkpoint.isCheckpointActivated)
        {
            StartCoroutine("CountToRespawn");
        }
    }
    private void CountPoints()
    {
        pointsTextField.text = PlayerData.playerFruitPoints.ToString();
    }
    private void CheckPlayerLifePoints()
    {
        if(playerLifePointsCount > playerController.LifePoints)
        {
            //create method to change life points in life bar
            int lastLifePointIndex = playerLifePointsCount - 1;
            lifePointsImageGameObject[lastLifePointIndex].texture = loseLifePointImage.texture;
            playerLifePointsCount--;
        }
        else if(playerController.LifePoints == 0)
        {
            StartCoroutine("DelayGameOver");
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
    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        audioManager.ShotSound(audioManagerData.pauseMenuSound);
    }

    /// <summary>
    /// Calls a static method to close the application from SceneLoadHandler class
    /// </summary>
    public void CallExitGame()
    {
        audioManager.PlayButtonClickSound();
        StartCoroutine(sceneHandler.CloseApp());
    }
    public void CallStartGame()
    {
        sceneHandler.StartGame();
    }

    public void NextLevelTransition()
    {
       animator.SetTrigger("fadeIn");
    }

    public void ResumeGame()
    {
        audioManager.PlayButtonClickSound();
        StartCoroutine("DelayResumeGame");
    }
    public void CallResetLevel()
    {
        audioManager.ShotSound(audioManagerData.buttonClickSound);
        //SceneLoadHandler.ResetLevelData();
    }

    public void ChangePauseGameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else if (isGamePaused)
            {
                ResumeGame();
            }
        }
    }
   

}
