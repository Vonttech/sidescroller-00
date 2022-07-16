using UnityEngine;

public class MovingSawRail : MonoBehaviour
{
    [SerializeField]
    private Transform topLimit;
    [SerializeField]
    private Transform bottomLimit;
    [SerializeField]
    private float moveSpeed = 6.75f;

    private float moveDistance = 0.1f;
    
    private float topLimitDistance;
    private float bottomLimitDistance;
    private float maxLimitDistance = 0.1f;

    [SerializeField]
    private bool moveUp = true;


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (moveUp) 
        {
            topLimitDistance = Vector3.Distance(transform.position, topLimit.position);

            if (topLimitDistance >= maxLimitDistance)
            {

                transform.localPosition += new Vector3(0, moveDistance * Time.deltaTime * moveSpeed, 0);
            }
            else
            {
                moveUp = false;
            }
        }
        else
        {
            bottomLimitDistance = Vector3.Distance(transform.position, bottomLimit.position);
            
            if (bottomLimitDistance >= maxLimitDistance)
            {
                transform.localPosition -= new Vector3(0, moveDistance * Time.deltaTime * moveSpeed, 0);
            }
            else
            {
                moveUp = true;
            }
        }
    }
}
