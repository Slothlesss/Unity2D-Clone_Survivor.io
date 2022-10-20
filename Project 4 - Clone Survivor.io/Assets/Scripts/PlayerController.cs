using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Character")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float currentHealth;
    [SerializeField] Slider healthSlider;



    Vector2 movement;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public bool CanMove { get; set; }

    private void Start()
    {
        this.healthSlider.maxValue = playerHealth;
        this.healthSlider.value = playerHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {

        UpdateMovement();
        UpdateAnimation();
    }

    void UpdateMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement.normalized * playerSpeed * Time.fixedDeltaTime);
        currentHealth = healthSlider.value;
    }

    void UpdateAnimation()
    {
        if(movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            sr.flipX = movement.x < 0.01 ? true : false;
        }
        animator.SetFloat("Speed", movement.normalized.sqrMagnitude);
    }





    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        Color c1 = sr.color;
        Color c2 = sr.color;

        c1.a = 0;
        sr.color = c1;
        yield return new WaitForSeconds(.1f);

        c2.a = 1;
        sr.color = c2;
        yield return new WaitForSeconds(.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gold"))
        {
            GameManager.Instance.GetGold();
            GameManager.Instance.Pool.ReleaseObject(collision.gameObject);
        }
        if (collision.CompareTag("Exp"))
        {
            GameManager.Instance.GetExp();
            GameManager.Instance.Pool.ReleaseObject(collision.gameObject);
        }
    }
}
