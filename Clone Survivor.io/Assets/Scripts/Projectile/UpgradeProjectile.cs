using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeProjectile
{
    public int Damage { get; private set; }
    public float AttackCD { get; private set; }
    public int Amount { get; private set; }

    public UpgradeProjectile(int damage, float attackCD, int amount)
    {
        this.Damage = damage;
        this.AttackCD = attackCD;
        this.Amount = amount;
    }




}
