using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriend : MonoBehaviour
{
    public Player player;
    public float speed;
    private Vector3 target; // позиц≥€ б≥л€ гравц€
    private SpriteRenderer rend;
    private Animator animator;
    public bool isGrounded;
   // private bool following;
    private void Start()
    {
        rend = transform.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();
        target = new Vector3(player.transform.position.x - 1, player.transform.position.y + 1, transform.position.z);
      //  StartCoroutine(MoveToTarget(transform, target));
    }
      
    //public IEnumerator MoveToTarget(Transform obj, Vector3 target)
    //{
    //    following = true;
        
    //    while (obj.position != target)
    //    {
    //        obj.position = Vector3.MoveTowards(obj.position, target, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //    following = false;
    //}

    private void FixedUpdate()
    {
        animator.speed = player.animationSpeed * player.index;
    }

    private void Update()
    {
        target = new Vector3(player.transform.position.x - 1, player.transform.position.y + 1, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //if (following == false)
        //{
        //    StartCoroutine(MoveToTarget(transform, target));
        //}

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rend.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rend.flipX = false;
            }
        }
    }
}


