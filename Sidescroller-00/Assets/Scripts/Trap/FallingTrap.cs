using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField]
    private float timeToFall = 2f;

    private Collider2D trapCollider;

    private TargetJoint2D trapTargetJoint;

    private void Start()
    {
        trapCollider = GetComponent<Collider2D>();

        trapTargetJoint = GetComponent<TargetJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeToFall);
        
        trapTargetJoint.enabled = false;

        trapCollider.enabled = false;


    }
}
