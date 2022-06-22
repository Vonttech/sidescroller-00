using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToggler : MonoBehaviour
{
    [SerializeField]
    private float activeTrap = 6;

    private bool isActive = false;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(player.transform.position, transform.position));

        if (Vector3.Distance(transform.position, player.transform.position) < activeTrap)
        {
            isActive = true; 
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
            Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            //transform.position += ((player.transform.position - transform.position) * Time.deltaTime * 0.55f);

            transform.position += new Vector3(0, 0.10f, 0) * Time.deltaTime * 0.2f;
        }
        else
        {
            isActive = false;
        }
    }
}
