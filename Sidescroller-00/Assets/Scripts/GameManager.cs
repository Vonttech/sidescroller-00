using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpoint;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private Player playerScript;

    [SerializeField]
    private TextMeshProUGUI pointsTextField;

    public static int fruitPoints = 0;

    // Update is called once per frame
    void Update()
    {
        RestartFromCheckpoint();
        CountPoints();
    }

    private void RestartFromCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerScript.IsAlive && Checkpoint.checkpointActivated)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     
            Player.respawned = true;
        }
    }

    private void CountPoints()
    {
        pointsTextField.text = fruitPoints.ToString();
    }
}
