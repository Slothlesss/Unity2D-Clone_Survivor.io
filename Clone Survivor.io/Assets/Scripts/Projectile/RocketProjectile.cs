using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    public GameObject ExplosionEffect;

    public override void Spawn(Monster target)
    {
        base.Spawn(target);
        counter = 0;
        transform.Rotate(0, 0, Random.Range(0, 360));
    }
    public override void MoveToTarget()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * ProjectileManager.Instance.rocketProjectileSpeed);
            //if(transform.position == Target.transform.position)
            if(Vector3.Distance(transform.position, Target.transform.position) < 0.05f)
            {
                Damage();
            }
            //Rotate projectile
            Vector2 dir = Target.transform.position - transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) - Mathf.PI/4) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
    public override void Damage()
    {
        if (ProjectileManager.Instance.rocketCanDamage)
        {
            float range = ProjectileManager.Instance.rocketProjectileRange;

            var hitInfo = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

            foreach (var hitCollider in hitInfo)
            {
                var enemy = hitCollider.GetComponent<Monster>();
                if (enemy)
                {
                    enemy.TakeDamage(ProjectileManager.Instance.rocketProjectileDamage);
                }
            }
            GameObject explosionEffect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(explosionEffect, 0.1f);
            GameManager.Instance.Pool.ReleaseObject(gameObject);
            ProjectileManager.Instance.rocketCanDamage = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ProjectileManager.Instance.rocketProjectileRange);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }
}
