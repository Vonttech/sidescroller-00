using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoadHandler : MonoBehaviour
{
    public static SceneLoadHandler Instance { get; private set; }

    public static int currentSceneID;

    private int initialLevelScene = 3;
    public int InitialLevelScene 
    { 
        get { return initialLevelScene; } 
        private set {}
    }

    [SerializeField]
    private Image fadeImage;

    private bool shouldStopFade = false;

    private Animator animator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            animator = GetComponent<Animator>();
            currentSceneID = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(this);
        }

    }

    private void LateUpdate()
    {
        CheckIfSceneLoaded();
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
        animator.SetTrigger("fadeOut");
        //SceneManager.LoadScene(InitialLevelScene);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(InitialLevelScene);
    }


    private void CheckIfSceneLoaded()
    {
        if (SceneManager.GetActiveScene().isLoaded && !shouldStopFade)
        {
            animator.SetTrigger("fadeIn");
            shouldStopFade = true;
        }
    }
    public static void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
