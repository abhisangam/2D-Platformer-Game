using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleKey : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectKey();
            animator.SetTrigger("KeyCollected");
            Destroy(gameObject, 2.0f);
        }
    }
}