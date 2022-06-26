using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Transform spikesTransform;

    [SerializeField]
    private float maxDistanceAllowed = 5f;

    private float distance;

    private float maxYMoveUnits = 3.13f;

    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private float spikeMoveSpeed = 20f;

    [SerializeField]
    private float movePerUnit = 0.09f;

    [SerializeField]
    private float spikeMovePerUnit = 0.01f;

    private float spikeXFinalPos = -0.75f;

    private float xFinalPos = 0f;

    private bool activeTrap = false;

    private bool moveTrap = false;

    private bool disableTrap = false;

    private void Update()
    {
        CheckTargetDistance();
        ActiveTrap();
        Move();
        DisableTrap();
    }

    private void CheckTargetDistance()
    {
        if (!activeTrap && !disableTrap)
        {
            distance = Vector3.Distance(targetTransform.position, transform.position);
            
            if (distance <= maxDistanceAllowed)
            {
                activeTrap = true;
            }
        }
    }

    private void ActiveTrap()
    {
        if (activeTrap)
        {
            if (transform.localPosition.y < maxYMoveUnits)
            {
                transform.position += Vector3.up * movePerUnit * moveSpeed * Time.deltaTime;
            }
            else
            {
                moveTrap = true;
                activeTrap = false;
            }
        }
    }

    private void Move()
    {
        if (moveTrap)
        {
            if (transform.localPosition.x >= xFinalPos)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime * movePerUnit;
                if (spikesTransform.localPosition.x >= spikeXFinalPos)
                {
                    spikesTransform.position -= Vector3.right * spikeMoveSpeed * Time.deltaTime * spikeMovePerUnit;
                }
            }
            else
            {
                moveTrap = false;
                disableTrap = true;
            }
        }
    }

    private void DisableTrap()
    {
        if(disableTrap && transform.localPosition.y >= 0)
        {   
            transform.localPosition += Vector3.down * movePerUnit * moveSpeed * Time.deltaTime;
        }
    }
}
