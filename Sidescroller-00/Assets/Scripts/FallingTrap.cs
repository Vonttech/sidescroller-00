using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField]
    private float timeToFall = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        gameObject.GetComponent<TargetJoint2D>().enabled = false;
    }
}
