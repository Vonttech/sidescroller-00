using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
public class GameManager : MonoBehaviour
{
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
    private SceneLoadHandler sceneHandler;

    [SerializeField]
    private IntroPanelManager introPanelManager;

    [SerializeField]
    private SpawnPointsHandler spawnPointsHandler;

    private bool isGamePaused = false;
    private bool isGameOver = false;


    private void Awake()
    {
        PlayerController.isAllowedToMove = false;

        SceneLoadHandler.currentSceneID = SceneManager.GetActiveScene().buildIndex;

        spawnPointsHandler.SetLevelSpawnPointsPosition();
    }

    private void Start()
    {
        playerLifePointsCount = playerScript.LifePoints;

        Trophy.isPlayerBeatLevel = false;

        PlayerData.playerInitialLifePoints = playerScript.LifePoints;

        spawnPointsHandler.CheckCheckpointUseLimit();

        CountTotalFruitsInLevel();

        SpawnPlayerFromStartPoint();
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

        ChangePauseGameState();
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
        if (!playerScript.IsAlive &&
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
    private void CountPoints()
    {
        pointsTextField.text = PlayerData.playerFruitPoints.ToString();
    }
    private void CheckPlayerLifePoints()
    {
        if(playerLifePointsCount > playerScript.LifePoints)
        {
            //create method to change life points in life bar
            int lastLifePointIndex = playerLifePointsCount - 1;

            lifePointsImageGameObject[lastLifePointIndex].texture = loseLifePointImage.texture;

            playerLifePointsCount--;
        }
        else if(playerScript.LifePoints == 0)
        {
            StartCoroutine(DelayGameOver());
        }
    }
    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1f);
        GameOver();
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

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        pauseMenuPanel.GetComponent<AudioSource>().Play();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;   
        pauseMenuPanel.SetActive(false);

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
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
