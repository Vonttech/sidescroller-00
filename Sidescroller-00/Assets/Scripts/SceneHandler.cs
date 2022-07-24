using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneHandler
{
    public static int currentSceneID;
    private float secondsToDelay = 1.3f;
    private int initialLevelSceneID = 3; //Pode ser inserido em um scriptableObject
    public int InitialLevelSceneID 
    { 
        get { return initialLevelSceneID; } 
        private set {}
    }
    private bool shouldStopFade = false;

    public void NextLevel()
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
    public void ResetLevelData()
    {
        Player.itemsCollected.Clear();
        PlayerData.playerFruitPoints = 0;
        Checkpoint.isCheckpointActivated = false;
        Checkpoint.timesCheckpointUsed = 0;
        Checkpoint.isLastRespawnAllowed = false;
    }

    /// <summary>
    /// Closes the application after 1.3 seconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator CloseApp()
    {
        yield return new WaitForSecondsRealtime(secondsToDelay);
        ShutDownApp();
    }

    private void ShutDownApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void CheckIfSceneLoaded()
    {
        if (SceneManager.GetActiveScene().isLoaded && !shouldStopFade)
        {
            shouldStopFade = true;
        }
    }

    public void StartGame()
    {
        LoadScene(initialLevelSceneID);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }






}
