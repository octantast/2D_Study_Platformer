using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0) // ÿךשמ דנאגוצü ןאהא÷ (גוכמס³ע³ < 0)
           // {
                Destroy(transform.parent.gameObject);
            //}
        }
    }
}


