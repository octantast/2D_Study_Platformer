using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public CameraController cameraMain;
    public GameObject bulletPrefab;
    public GameObject bullets;
    public GameObject win;

    public float health;
    public float healthMax;
    private float healthVisual;
    public float healthVisualSpeed;
    public Image healthbar;

    public float coins;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Animator animator;

    public float force; // сила стрибка
    public float speed; // швидкість руху
    public float index; // для сповільнення руху і анімацій
    public float animationSpeed; // стандартна швидкість анімації
    private Vector3 startPosition; // початкова позиція гравця
    public float addForceMax; // максимум ривка
    public float forceTimer;
    private bool isGrounded;
    private float addForce;
    public TMP_Text coinsText;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rend = transform.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();
        startPosition = transform.position;
        health = healthMax;
        healthVisual = health;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3((Input.GetAxis("Horizontal") * speed * index) + addForce, rb.velocity.y, 0);
        animator.speed = animationSpeed * index;
        coinsText.text = coins.ToString() + "$";
        if (health != healthVisual)
        {
            healthVisual = Mathf.Lerp(healthVisual, health, healthVisualSpeed);
            healthbar.fillAmount = healthVisual / healthMax;
        }
    }

    IEnumerator addForceTimer()
    {
        yield return new WaitForSeconds(forceTimer);
        addForce = 0;
    }

    void Update()
    {
        // рух
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                cameraMain.biasX = 1;
                rend.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                cameraMain.biasX = -1;
                rend.flipX = false;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // підкрадання
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            index = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || isGrounded == false)
        {
            index = 1;
        }

        // стрибок
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        }

        // ривки
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            rend.flipX = false;
            addForce = -1 * addForceMax;
            StartCoroutine(addForceTimer());
            rb.AddForce(transform.up * force * 0.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            rend.flipX = true;
            addForce = addForceMax;
            StartCoroutine(addForceTimer());
            rb.AddForce(transform.up * force * 0.5f, ForceMode2D.Impulse);
        }

        //
        if(Input.GetMouseButtonDown(0))
        {
           GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bullets.transform);
            newBullet.GetComponent<Bullet>().targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("VelocityM", rb.velocity.magnitude);
    }

    public void Damage(float count)
    {
        health -= count;
        
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Batut"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * force * Random.Range(1.6f, 2.1f), ForceMode2D.Impulse);
        }
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.CompareTag("Water"))
        {
            if (win.activeSelf == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
