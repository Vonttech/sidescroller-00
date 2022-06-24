using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBox : MonoBehaviour
{

    [SerializeField]
    private GameObject innerTrap;

    private Transform targetTransform;

    private Animator animator;

    [SerializeField]
    private float maxDistanceAllowed;

    private float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetDistance();
    }

    private void CheckTargetDistance()
    {
        targetDistance = Vector3.Distance(targetTransform.position, transform.position);
        
        if(targetDistance <= maxDistanceAllowed)
        {
            animator.SetTrigger("destroy");

            StartCoroutine(DisableBox());

        }
    }


    IEnumerator DisableBox()
    {
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);

        Instantiate(innerTrap, transform.position, innerTrap.transform.rotation);

        innerTrap.transform.position = transform.position;

        innerTrap.SetActive(true);
    }
}
