using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsHandler : MonoBehaviour
{

    [SerializeField]
    private Transform startPointPlataform;
    [SerializeField]
    private GameObject checkpoint;

    private Checkpoint checkpointScript;

    private float yPlayerRespawnPosition = 3f;


    private void Awake()
    {
        checkpointScript = checkpoint.GetComponent<Checkpoint>();
    }

    public void SetLevelSpawnPointsPosition()
    {
        if (LevelData.levelStartPoint == Vector3.zero && LevelData.checkpointPosition == Vector3.zero)
        {
            LevelData.levelStartPoint = startPointPlataform.transform.position + (Vector3.up * yPlayerRespawnPosition);

            LevelData.checkpointPosition = checkpoint.transform.position + (Vector3.up * yPlayerRespawnPosition);
        }
    }


    public void CheckCheckpointUseLimit()
    {
        if (Checkpoint.timesCheckpointUsed > checkpointScript.CheckpointUseLimit &&
            Checkpoint.isCheckpointActivated)
        {
            
            Checkpoint.isLastRespawnAllowed = true;
       
            Checkpoint.isCheckpointActivated = false;

            checkpoint.SetActive(false);
        }
    }
}
