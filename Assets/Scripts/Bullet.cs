using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    public float speed;

    private void Start()
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // кут в радіанах > кут в градусах
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.isTrigger)
        //{
        //    if (collision.gameObject.CompareTag("Enemy"))
        //    {
        //        Destroy(collision.gameObject);
        //    }
        //}
        //if (!collision.gameObject.CompareTag("Player"))
        //{
        //    Destroy(this.gameObject);
        //}
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
