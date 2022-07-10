using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    public static int currentSceneID;

    private int initialLevelScene = 3;
    public int InitialLevelScene 
    { 
        get { return initialLevelScene; } 
        private set {}
    }


    private bool isGamePaused = false;
    public bool IsGamePaused { get { return isGamePaused; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            currentSceneID = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(this);
        }
    }

    public static void NextLevel()
    {

        int nextSceneToLoadIndex = currentSceneID + 1;

        try
        {
            Scene nextSceneToLoad = SceneManager.GetSceneAt(nextSceneToLoadIndex);

            SceneManager.LoadScene(nextSceneToLoad.name);

        }catch
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(InitialLevelScene);
    }

    public void ChangePauseGameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {

            PauseGame();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {

            ResumeGame();

        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
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
