using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Animator animator;

    private Collider2D itemCollider;

    private float timeToDisable = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

        if (Player.itemsCollected.ContainsKey(gameObject.name))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            animator = GetComponent<Animator>();

            itemCollider = GetComponent<Collider2D>();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            itemCollider.enabled = false;
            
            animator.SetBool("collected", true);
   
            Player.itemsCollected.Add(gameObject.name, gameObject);

            PlayerData.playerFruitPoints++;

            StartCoroutine(DisableItem());
        }
    }

    IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
}
