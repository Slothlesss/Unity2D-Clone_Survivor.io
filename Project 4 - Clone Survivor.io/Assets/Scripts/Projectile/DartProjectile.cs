using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartProjectile : Projectile
{
    public override void Spawn(Monster target)
    {
        transform.SetParent(PlayerController.Instance.transform);

        counter = 0;
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1f, 1f), false));
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool isRemove)
    {
        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        transform.localScale = to;
        if(isRemove)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
    public override void MoveToTarget() //MOve around
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * ProjectileManager.Instance.dartProjectileInnerSpeed);
        transform.RotateAround(PlayerController.Instance.transform.position, Vector3.forward , ProjectileManager.Instance.dartProjectileSpeed * Time.deltaTime);
    }
    public override void Damage()
    {
            /*float range = ProjectileManager.Instance.dartProjectileRadius;

            var hitInfo = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

            foreach (var hitCollider in hitInfo)
            {
                var enemy = hitCollider.GetComponent<Monster>();
                if (enemy)
                {
                    Debug.Log(enemy.name);
                    enemy.TakeDamagePerSecond(ProjectileManager.Instance.dartProjectileDamage, 0.5f);
                }
            }*/
    }
    public override void Release()
    {
        counter += Time.deltaTime;
        if (counter >= ProjectileManager.Instance.dartProjectileExistTime)
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ProjectileManager.Instance.dartProjectileRadius);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamagePerSecond(ProjectileManager.Instance.dartProjectileDamage, 0.2f); 
        }
    }
}
