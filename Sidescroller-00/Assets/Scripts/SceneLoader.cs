using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    public static int currentSceneID;

    private int initialLevelScene = 3;
    public int InitialLevelScene 
    { 
        get { return initialLevelScene; } 
        private set {}
    }


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

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
