using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] float leftPushRange;
    [SerializeField] float rightPushRange;
    [SerializeField] float velocityThreshold;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }
    private void Push()
    {
        if (transform.rotation.z > 0
        && transform.rotation.z < rightPushRange
        && (rigidbody2D.angularVelocity > 0)
        && rigidbody2D.angularVelocity < velocityThreshold)
        {
            rigidbody2D.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
       && transform.rotation.z > leftPushRange
       && (rigidbody2D.angularVelocity < 0)
       && rigidbody2D.angularVelocity > velocityThreshold * -1)
        {
            rigidbody2D.angularVelocity = velocityThreshold * -1;
        }

    }
}
