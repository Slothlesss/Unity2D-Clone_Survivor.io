using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile: MonoBehaviour
{
    public LayerMask enemyMask;

    public float counter;
    Monster target;
    public Monster Target
    {
        get { return target; }
    }
    protected Queue<Monster> monsters = new Queue<Monster>();

    void Update()
    {
        MoveToTarget();
        Release();
    }
    public virtual void Spawn(Monster target)
    {
        this.target = target;
    }


    public abstract void MoveToTarget();

    public abstract void Damage();
    
    public virtual void Release()
    {
        counter += Time.deltaTime;
        if (counter >= ProjectileManager.Instance.normalProjectileExistTime)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
            counter = 0;
        }
    }
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            Damage();
        }
    }*/


    //Rocket projectile

    /* transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime* projectileSpeed);

           //Rotate projectile
           Vector2 dir = target.transform.position - transform.position;
   float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
   transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
   */
}
