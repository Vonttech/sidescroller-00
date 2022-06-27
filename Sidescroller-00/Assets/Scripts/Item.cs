using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        if (Player.itemsCollected.ContainsKey(gameObject.name))
        {
            gameObject.SetActive(false);
        }
        else
        {
            animator = GetComponent<Animator>();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.fruitPoints++;
            
            animator.SetBool("collected", true);
   
            Player.itemsCollected.Add(gameObject.name, gameObject);

            StartCoroutine(DisableItem());
        }
    }

    IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
