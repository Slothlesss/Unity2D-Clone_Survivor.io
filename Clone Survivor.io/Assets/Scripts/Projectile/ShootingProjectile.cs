using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingProjectile : MonoBehaviour
{
    [SerializeField] Transform player;
    [Header("Timer Projectile")]
    [SerializeField] List<string> projectileName;
    [SerializeField] float normalProjectileCD;
    [SerializeField] float rocketProjectileCD;
    [SerializeField] float dartProjectileCD;
    [SerializeField] Slider attackCoolDownSlider;


    float normalTimer;
    float rocketTimer;
    float dartTimer;

    List<Monster> monstersInRange = new List<Monster>();

    Monster target;

    private void Start()
    {
        this.attackCoolDownSlider.maxValue = normalProjectileCD;
        this.attackCoolDownSlider.value = normalTimer;

        
    }

    
    private void Update()
    {
        transform.position = player.position;
        CountTimer();
        AimShoot();
        ShootDartProjectile();
    }
    void CountTimer()
    {
        normalTimer += Time.deltaTime;
        rocketTimer += Time.deltaTime;
        dartTimer += Time.deltaTime;
        this.attackCoolDownSlider.value = normalTimer;
    }    
    void AimShoot() // sua lai ham nay`, chi can check monster nao gan nhat
    {
        float closestDistance = Mathf.Infinity;
        if(monstersInRange.Count > 0)
        {
            foreach(Monster monster in monstersInRange)
            {
                float curDis = Vector3.Distance(player.position, monster.transform.position);
                if (curDis < closestDistance )
                {
                    closestDistance = curDis;
                    target = monster;
                }
            }
            if (target != null && target.IsActive)
            {
                ShootNormalProjectile();
                ShootRocketProjectile();
            }
        }

        /*if (target == null && monsters.Count > 0 && monsters.Peek().IsActive)
        {
            target = monsters.Dequeue();
        }
        if (target != null && target.IsActive)
        {
            ShootNormalProjectile();
            ShootRocketProjectile();
        }
        if (target != null && !target.IsAlive || target != null && !target.IsActive)
        {
            target = null;
        }
        */
    }
    void ShootNormalProjectile()
    {
        if (normalTimer >= normalProjectileCD)
        {
            StartCoroutine(SpawnNormalProjectile());
            normalTimer = 0;
        }
    }

    IEnumerator SpawnNormalProjectile()
    {
        for (int i = 0; i < ProjectileManager.Instance.normalProjectileNumber; i++)
        {
            Projectile projectile = GameManager.Instance.Pool.GetObject(projectileName[0]).GetComponent<NormalProjectile>();
            if (target != null)
            {
                projectile.Spawn(target);
            }
            projectile.transform.position = transform.position;
            yield return new WaitForSeconds(0.15f
                );
        }

    }
    

    void ShootRocketProjectile()
    {
        if (rocketTimer >= rocketProjectileCD)
        {
            for (int i = 0; i < ProjectileManager.Instance.rocketProjectileNumber; i++)
            {
                Projectile projectile = GameManager.Instance.Pool.GetObject(projectileName[1]).GetComponent<RocketProjectile>();
                if (target != null)
                {
                    projectile.Spawn(target);
                }
                projectile.transform.position = transform.position;
                rocketTimer = 0;
                ProjectileManager.Instance.rocketCanDamage = true;
            }
        }
    }

    void ShootDartProjectile()
    {
        if (dartTimer >= dartProjectileCD)
        {
            float radius = ProjectileManager.Instance.dartProjectileWideRange;
            int numberDarts = ProjectileManager.Instance.dartProjectileNumber;
            for (int i = 0; i < numberDarts; i++)
            {
                GameObject dart1 = GameManager.Instance.Pool.GetObject("DartProjectile");
                float a = (float)i / numberDarts;
                float b = a * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(b);
                float yScaled = Mathf.Sin(b);

                float x = xScaled * radius;
                float y = yScaled * radius;
                dart1.GetComponent<DartProjectile>().Spawn(target);
                dart1.transform.position = PlayerController.Instance.transform.position + new Vector3(x, y, 0f);
            }
            dartTimer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            //monsters.Enqueue(other.GetComponent<Monster>());
            monstersInRange.Add(other.GetComponent<Monster>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            //target = null;
            monstersInRange.Remove(other.GetComponent<Monster>());
        }
    }
}
