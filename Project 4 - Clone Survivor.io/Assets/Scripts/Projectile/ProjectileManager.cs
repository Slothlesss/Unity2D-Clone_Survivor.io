using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [Header("Normal Projectile")]
    public int normalProjectileDamage;
    public float normalProjectileSpeed;
    public float normalProjectileExistTime;
    public float normalProjectileRange;
    public int normalProjectileLevel;
    public int normalProjectileNumber;

    [Header("Rocket Projectile")]
    public int rocketProjectileDamage;
    public float rocketProjectileSpeed;
    public float rocketProjectileExistTime;
    public float rocketProjectileRange;
    public int rocketProjectileLevel;
    public int rocketProjectileNumber;
    public bool rocketCanDamage = false; // avoid damage several times at one hit

    [Header("DartProjectile")]
    public int dartProjectileDamage;
    public float dartProjectileInnerSpeed;
    public float dartProjectileSpeed;
    public float dartProjectileExistTime;

    public float dartProjectileRadius;
    public float dartProjectileWideRange;
    public int dartProjectileLevel;
    public int dartProjectileNumber;




    public UpgradeProjectile[] NormalProjectileUpgrades { get; protected set; }

    private void Start()
    {
        NormalProjectileUpgrades = new UpgradeProjectile[]
        {
                new UpgradeProjectile(5,-0.2f,10),
                new UpgradeProjectile(5,-0.2f,10),
                new UpgradeProjectile(5,-0.2f,10)
        };
    }

    public void UpgradeNormalProjectile()
    {
        normalProjectileDamage += NormalProjectileUpgrades[normalProjectileLevel].Damage;
        normalProjectileLevel++;
    }
    public void UpgradeRocketProjectile()
    {
        rocketProjectileDamage += NormalProjectileUpgrades[normalProjectileLevel].Damage;
        normalProjectileLevel++;
    }

    public void UpgradeDartProjectile()
    {
        dartProjectileNumber += 1;
    }

}
