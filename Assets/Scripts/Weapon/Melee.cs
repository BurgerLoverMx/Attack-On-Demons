using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField]
    private float attackRate = 1f, attackSpeed = 1f;

    private float attackRateTime = 0;

    private MeleeAttack meleeAttackScript;

    void Start()
    {
        meleeAttackScript = GetComponentInChildren<MeleeAttack>();
    }

    void Update()
    {
        if (attackRateTime >= 0)
        {
            attackRateTime -= Time.deltaTime;
        }
    }

    public override void Attack()
    {
        if (attackRateTime > 0)
        {
            return;
        }

        animator.speed = 1 / attackSpeed;
        animator.SetTrigger("Attack");

        meleeAttackScript.damage = damage;

        attackRateTime = attackRate;
    }
}
