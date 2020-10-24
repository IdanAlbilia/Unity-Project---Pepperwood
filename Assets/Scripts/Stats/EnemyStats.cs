using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    CharacterAnimator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<CharacterAnimator>();
    }


    public override void Die()
    {
        base.Die();
        // add ragdoll effect
        animator.Die(this, false);

     
    }
}
