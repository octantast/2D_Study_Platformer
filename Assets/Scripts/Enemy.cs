using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageHp;
    private Animator animator;
    private SpriteRenderer rend;
    public Transform[] points;
    private float speed;
    public float speedIdle;
    public float speedAttack;

    private int currentPoint;
    private Rigidbody2D rb;
    private Vector3 target;
    private Transform player;

    private bool culdown;
    public float culdownTime;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        rend = transform.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (!player)
        {
            speed = speedIdle;
            culdown = false;
            if (transform.position == points[currentPoint].position)
            {
                currentPoint += 1;
                if (currentPoint == points.Length)
                {
                    currentPoint = 0;
                }
            }
            target = points[currentPoint].position;
        }
        else
        {
            if (culdown)
            {
                speed = speedIdle;
                target = player.position + Vector3.up * 1.8f;
            }
            else
            {
                speed = speedAttack;
                target = player.position;
            }
        }

        if (transform.position.x < points[currentPoint].position.x)
        {
            rend.flipX = true;
        }
        else if (transform.position.x > points[currentPoint].position.x)
        {
            rend.flipX = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        rb.velocity = Vector3.zero;
        animator.SetBool("attack", speed == speedAttack);
    }

    IEnumerator CulldownTimer()
    {
        yield return new WaitForSeconds(culdownTime);
        culdown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(damageHp);
               culdown = true;
            StartCoroutine(CulldownTimer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }
}
