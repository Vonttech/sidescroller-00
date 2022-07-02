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

    [SerializeField]
    private RawImage[] lifePointsImageGameObject = new RawImage[3];

    [SerializeField]
    private Sprite loseLifePointImage;

    private int playerLifePointsCount;

    public static int fruitPoints = 0;

    private void Start()
    {
        playerLifePointsCount = playerScript.LifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        RestartFromCheckpoint();
        CountPoints();
        CheckPlayerLifePoints();
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

    private void CheckPlayerLifePoints()
    {
        if(playerLifePointsCount > playerScript.LifePoints)
        {
            int lastLifePointIndex = playerLifePointsCount - 1;

            lifePointsImageGameObject[lastLifePointIndex].texture = loseLifePointImage.texture;

            playerLifePointsCount--;
        }
    }
}
