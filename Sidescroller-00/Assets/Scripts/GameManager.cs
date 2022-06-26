using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpoint;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RestartFromCheckpoint();
    }

    private void RestartFromCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerScript.IsAlive && Checkpoint.checkpointActivated)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Player.respawned = true;
        }
    }
}
