using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }

        set { isAlive = value; }
    }

    private Vector3 playerRespawPos;
    public Vector3 PlayerRespawPos
    {
        get { return playerRespawPos; }
        set { playerRespawPos = value; }
    }

    public static bool respawned = false;

    private Rigidbody2D playerRigidBody;

    public static Dictionary<string ,GameObject> itemsCollected = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        if (respawned)
        {
            transform.position = GameObject.Find("Checkpoint").transform.localPosition + (Vector3.up * 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            isAlive = false;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
   
}
