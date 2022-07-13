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
            GetComponent<Animator>().SetTrigger("fadeIn");
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
        GetComponent<Animator>().SetTrigger("fadeOut");
        //SceneManager.LoadScene(InitialLevelScene);
    }

    public void DoNothing()
    {
        SceneManager.LoadScene(InitialLevelScene);
    }

   
}
