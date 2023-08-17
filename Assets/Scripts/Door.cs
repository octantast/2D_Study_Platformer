using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool playerSeen;
    private Animator animator;
    public GameObject blackSquare;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("playerSeen", playerSeen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
               playerSeen = true;
        }
        blackSquare.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerSeen = false;
        }
        blackSquare.SetActive(true);
    }
}
