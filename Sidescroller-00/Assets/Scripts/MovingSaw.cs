using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField]
    private Vector2 limit;

    [SerializeField]
    private float speed;

    private Vector3 rightLimit;
    private Vector3 leftLimit;

    bool toRight = true;

    // Start is called before the first frame update
    void Start()
    {
      rightLimit = new Vector3(transform.position.x + limit.x, transform.position.y + limit.y, 0);
      leftLimit = new Vector3(transform.position.x - limit.x, transform.position.y - limit.y, 0);
      Debug.Log(rightLimit);
      Debug.Log(leftLimit);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Debug.Log(toRight);
        if (toRight)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            if(transform.position.x > rightLimit.x)
            {
                toRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * Time.deltaTime * speed;
            if (transform.position.x < leftLimit.x)
            {
                toRight = true;
            }
        }
    }
}
