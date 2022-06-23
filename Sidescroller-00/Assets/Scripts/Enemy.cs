using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float maxDistanceAllowed = 2.75f;
    [SerializeField]
    private float moveSpeed = 2.75f;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
   private void Move()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if(distance < maxDistanceAllowed)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
