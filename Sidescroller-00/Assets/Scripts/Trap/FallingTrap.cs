using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField]
    private float timeToFall = 2f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeToFall);
        gameObject.GetComponent<TargetJoint2D>().enabled = false;
    }
}
