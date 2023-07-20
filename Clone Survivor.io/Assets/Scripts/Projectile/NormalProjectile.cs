using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : Projectile
{
    public override void Spawn(Monster target)
    {
        base.Spawn(target);
        counter = 0;
        transform.Rotate(0, 0, Random.Range(0, 360));
    }
    public override void MoveToTarget()
    {
        if (Target != null && Target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * ProjectileManager.Instance.normalProjectileSpeed);
        }
        else if(Target == null)
        {
            transform.Translate(Time.deltaTime * ProjectileManager.Instance.normalProjectileSpeed, 0,0 );
        }
        else if(!Target.IsActive) //dan dang bay xong quai chet ==> bay thang; chua lam dc
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
    public override void Damage() //Thu dung raycast
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster)
        {
            monster.TakeDamage(ProjectileManager.Instance.normalProjectileDamage);
            GameManager.Instance.Pool.ReleaseObject(gameObject);
            counter = 0;
        }

    }
}
