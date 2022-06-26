using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToggler : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceAllowed = 1f;

    private float distance;

    [SerializeField]
    private float speed = 2.25f;

    [SerializeField]
    private float moveUnits = 0.09f;

    private bool isActive = false;

    [SerializeField]
    private Vector2 trapActivePosition;

    private Transform target;

    private Collider2D trapCollider;

    //start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        trapCollider = GetComponent<Collider2D>();
    }

    //update is called once per frame
    void Update()
    {
        CheckTrapStatus();
    }

    private bool ActiveTrap(float distance)
    {
        if (!isActive)
        {
            if (distance <= maxDistanceAllowed)
            {
                isActive = true;
                return isActive;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        } 
    }

    private void CheckTrapStatus()
    {

        distance = Vector3.Distance(target.position, transform.position);

        isActive = ActiveTrap(distance);

        trapActivePosition.x = transform.position.x;

        if (isActive)
        {
            if(transform.position.y < trapActivePosition.y)
            {
                transform.position += Vector3.up * moveUnits * speed * Time.deltaTime;
            }

            trapCollider.enabled = true;

        }
    }
}
