using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void NextLevel()
    {
        int sceneToLoad = currentSceneID++;

        bool isSceneToLoadValid = SceneManager.GetSceneAt(sceneToLoad).IsValid();
        
        if (isSceneToLoadValid){
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(InitialLevelScene);
    }
}
