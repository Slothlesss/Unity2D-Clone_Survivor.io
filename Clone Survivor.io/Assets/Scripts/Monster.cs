using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    [SerializeField] Material flashMaterial;
    [SerializeField] Material normalMaterial;


    [SerializeField] int monsterMaxHealth;
    [SerializeField] float monsterSpeed;
    [SerializeField] int monsterDamage;
    [SerializeField] float TimeBetweenAttack;

    [SerializeField] Animator anim;
    GameObject destination;

    public int monsterCurrentHealth;
    public bool IsActive { get; set; }

    public bool IsAlive 
    { 
        get
        {
            return monsterCurrentHealth > 0;
        }
    }

    float countTBA = 0; //TimeBetweenAttack
    public float countTBD = 0; //TimeBetweenDamage

    private void Update()
    {
        Move();
        UpdateAnimation();
        Attack();
        countTBD += Time.deltaTime;
    }
    public void Spawn(Transform MonsterPool, Vector3 randomPos)
    {
        IsActive = true;
        randomPos.x += randomPos.x <= 0 ? -1f : 1f;
        randomPos.y += randomPos.y <= 0 ? -1f : 1f;
        //destination = GameObject.Find("Player");
        destination = PlayerController.Instance.gameObject;
        transform.position = randomPos + destination.transform.position;
        monsterCurrentHealth = monsterMaxHealth;
        transform.SetParent(MonsterPool);
        sr.material = normalMaterial;
    }
    void Release()
    {
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
    }
    private void Move()
    {
        if(IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, monsterSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {
        countTBA += Time.deltaTime;
        if (Vector3.Distance(destination.transform.position, transform.position) < 0.1f && countTBA >= TimeBetweenAttack)
        {
            PlayerController.Instance.TakeDamage(monsterDamage);
            countTBA = 0;
        }
    }
    public void IncreaseStrength(int health, int damage)
    {
        monsterMaxHealth += damage;
        monsterDamage += damage;
    }
    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            monsterCurrentHealth -= damage;

            StartCoroutine(TakeDamageAffect());
            if (!IsAlive)
            {
                CreateItem();
                Release();
            }
        }
    }

    public void TakeDamagePerSecond(int damage, float timer)
    {
        if (IsActive && countTBD >= 0)
        {
            monsterCurrentHealth -= damage;
            countTBD = -timer;
            StartCoroutine(TakeDamageAffect());
            if (!IsAlive)
            {
                CreateItem();
                Release();
            }
        }
    }

    IEnumerator TakeDamageAffect()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        sr.material = normalMaterial;
        yield return new WaitForSeconds(0.1f);
    }

    void CreateItem()
    {
        Item goldObject = GameManager.Instance.Pool.GetObject("GoldObject").GetComponent<Item>();
        Item expObject = GameManager.Instance.Pool.GetObject("ExpObject").GetComponent<Item>();
        goldObject.Spawn(transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
        expObject.Spawn(transform.position);
    }

    void UpdateAnimation()
    {
        if (transform.position.x > destination.transform.position.x) //Move left
        {
            sr.flipX = false;
        }
        else if (transform.position.x < destination.transform.position.x) //Move right
        {
            sr.flipX = true;
        }

    }
}
